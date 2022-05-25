using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public struct Const<T>
    {
        public T Value { get; private set; }

        public Const(T value) : this()
        {
            this.Value = value;
        }
    }

    class MessageResolver
    {
		public readonly short HEADERSIZE = 2;

		// 메시지 사이즈.
		int messageSize;
		// 읽어와야 할 목표 위치.
		int positionToRead;
		// 남은 사이즈.
		int remainBytes;
		// 현재 진행중인 버퍼의 인덱스를 가리키는 변수.
		int currentPosition;
		// 진행중인 버퍼.
		byte[] messageBuffer = new byte[1024];

		public delegate void OnCompleted(Const<byte[]> buffer);
        public void ReadBytes(byte[] buffer, int offset, int transffered, OnCompleted callback)
        {
			// 읽어야 할 바이트 길이.
			remainBytes = transffered;

			// 원본 버퍼의 포지션값.
			// 패킷이 여러개 뭉쳐 올 경우 원본 버퍼의 포지션은 계속 앞으로 가야 하는데 그 처리를 위한 변수이다.
			int src_position = offset;

			// 남은 데이터가 있다면 계속 반복한다.
			while (remainBytes > 0)
			{
				bool completed = false;
				//Console.WriteLine("remainBytes" + remainBytes);
				// 헤더만큼 못읽은 경우 헤더를 먼저 읽는다.
				if (currentPosition < HEADERSIZE)
				{
					// 목표 지점 설정(헤더 위치까지 도달하도록 설정).
					positionToRead = HEADERSIZE;

					completed = ReadUntil(buffer, ref src_position, offset, transffered);
					if (!completed)
					{
						// 아직 다 못읽었으므로 다음 receive를 기다린다.
						return;
					}

					// 헤더 하나를 온전히 읽어왔으므로 메시지 사이즈를 구한다.
					messageSize = GetBodySize();

					// 다음 목표 지점(헤더 + 메시지 사이즈).
					positionToRead = messageSize + HEADERSIZE;
				}

				completed = ReadUntil(buffer, ref src_position, offset, transffered);

				if (completed)
				{
					callback(new Const<byte[]>(messageBuffer));
					ClearBuffer();
				}
			}
		}

		bool ReadUntil(byte[] buffer, ref int src_position, int offset, int transffered)
		{
			if (this.currentPosition >= offset + transffered)
			{
				// 들어온 데이터 만큼 다 읽은 상태이므로 더이상 읽을 데이터가 없다.
				return false;
			}

			// 읽어와야 할 바이트.
			// 데이터가 분리되어 올 경우 이전에 읽어놓은 값을 빼줘서 부족한 만큼 읽어올 수 있도록 계산해 준다.
			int copy_size = positionToRead - currentPosition;

			// 앗! 남은 데이터가 더 적다면 가능한 만큼만 복사한다.
			if (remainBytes < copy_size)
			{
				copy_size = remainBytes;
			}

			// 버퍼에 복사.
			Array.Copy(buffer, src_position, messageBuffer, currentPosition, copy_size);


			// 원본 버퍼 포지션 이동.
			src_position += copy_size;

			// 타겟 버퍼 포지션도 이동.
			currentPosition += copy_size;

			// 남은 바이트 수.
			remainBytes -= copy_size;

			// 목표지점에 도달 못했으면 false
			if (currentPosition < this.positionToRead)
			{
				return false;
			}

			return true;
		}

		int GetBodySize()
		{
			// 헤더 타입의 바이트만큼을 읽어와 메시지 사이즈를 리턴한다.
			Type type = HEADERSIZE.GetType();
			if (type.Equals(typeof(Int16)))
			{
				return BitConverter.ToInt16(messageBuffer, 0);
			}

			return BitConverter.ToInt32(messageBuffer, 0);
		}

		void ClearBuffer()
		{
			Array.Clear(messageBuffer, 0, messageBuffer.Length);

			currentPosition = 0;
			messageSize = 0;
		}
	}
}
