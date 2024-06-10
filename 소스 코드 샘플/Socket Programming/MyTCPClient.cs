

public class MyTCPClient : MySocket
{
	TCPClient socket = null;
	
	public override void Run()
	{
		socket = new TCPClient(ip, int.Parse(port), headerLength);
		socket.ConnectRecv();
		thread = new Thread(new ThreadStart(ThreadRun));
		thread.Start();
	}

	private void ThreadRun()
	{
		socket.Dispatch();
	}

	public override void DisconnectThread()
	{
		socket.DisconnectThread();
	}

	public override void Send(byte[] msg)
	{
		if (socket.IsConnected)
		{
			socket.Send(msg, msg.Length);
		}
	}
	
	public override MsgData Receive()
	{
		byte[] buffer = new byte[1];
		int recvSize = socket.Receive(ref buffer);

		if (recvSize > 0)
		{
			int index = 0;
			int offset = 0;

			for (int i = 0; i < buffer.Length; i++)
			{
				if (buffer[i].Equals(0x00))
				{
					MsgData msg = new MsgData(headerLength, (index - headerLength));
					byte[] array = new byte[index];
					Array.Copy(buffer, offset, array, 0, index);

					msg.SetHeader(array, headerInfo);
					bool unpromisedMessage = msg.VerifyReceivedMsgLength(index);

					if(unpromisedMessage){
					//bool unpromisedMessage = msg.SetMsgData(array, headerInfo, index);
					/*만약 수신된 일련의 데이터가 완성된 Packet이 아닌 상황을 위한 대기 로직 */
					// -- 수신된 패킷 길이 확인
					// -- 하나의 패킷을 나눠서 보내지 않겠다는 사전 협의를 기반으로 만들어진 로직

						Send("메세지 실패 내용 전달", "실패한 메세지 아이디");
						break;
					}

					msg.SetBody(array);


					receiveMsg.Add(msg);
					index = -1;
					offset = i + 1;
				}
				index++;
			}
		}

		if (0 < receiveMsg.Count)
		{
			MsgData msg = receiveMsg[0];
			receiveMsg.RemoveAt(0);
			return msg;
		}

		return null;
	}
	
	

	public override void Disconnect()
	{
		socket.Disconnect();
	}
}