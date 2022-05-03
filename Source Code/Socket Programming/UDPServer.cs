
public class UDPServer
{
	private Socket socket = null;
	private Thread thread = null;
	private bool IsDisconnected = true;

	// 송신 버퍼.
	private PacketQueue sendQueue;
	// 수신 버퍼.
	private PacketQueue recvQueue;
	// 송수신용 패킷의 최대 크기.
	private const int packetSize = 4096;
	private int port;

	public UDPServer(int port)
	{
		this.port = port;
		sendQueue = new PacketQueue();
		recvQueue = new PacketQueue();
	}

	public void StartServer()
	{
		try 
		{
			if (socket == null)
			{
				socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			}
			socket.Bind(new IPEndPoint(IPAddress.Any, port));
		} 
		catch (Exception e) 
		{
			// 에러 처리 로직
		}
	}

	public void StopServer()
	{
		IsDisconnected = true;

		if (thread != null)
		{
			thread.Join();
			thread = null;
		}

		Disconnect();
	}


	public void Disconnect()
	{
		if (socket != null)
		{
			socket.Close();
			socket = null;
		}
	}

	public int Send(byte[] data, int size)
	{
		return sendQueue.Enqueue(data, size);
	}

	public int Receive(ref byte[] buffer)
	{
		return recvQueue.Dequeue(ref buffer);
	}

	public void Dispatch()
	{
		while (!IsDisconnected)
		{
			if (socket != null)
			{
				// 송신 처리.
				DispatchSend();

				// 수신 처리.
				DispatchReceive();
			}

			Thread.Sleep(3);
		}
	}

	void DispatchSend()
	{
		try
		{
			if (socket.Poll(0, SelectMode.SelectWrite))
			{
				byte[] buffer = new byte[packetSize];
				int sendSize = sendQueue.Dequeue(ref buffer);

				while (sendSize > 0)
				{
					socket.Send(buffer, sendSize, SocketFlags.None);
					sendSize = sendQueue.Dequeue(ref buffer);
				}
			}
		}
		catch (Exception e)
		{
			// 에러 처리 코드.
		}
	}

	void DispatchReceive()
	{
		if (socket == null)
		{
			return;
		}

		// 수신 처리.
		try
		{
			while (socket.Poll(0, SelectMode.SelectRead))
			{
				byte[] buffer = new byte[packetSize];

				int recvSize = socket.Receive(buffer, buffer.Length, SocketFlags.None);

				if (recvSize == 0)
				{
					Disconnect();
				}
				else if (recvSize > 0)
				{
					recvQueue.Enqueue(buffer, recvSize);	
				}
			}
		}
		catch (Exception e)
		{
			// 에러 처리 코드 필요
		}
	}
}