
public class TCPClient : Client
{
	private Socket socket = null;
	// 송신 버퍼
	private PacketQueue sendQueue;
	// 수신 버퍼
	private PacketQueue recvQueue;
	// 
	PacketParser packetParser;
	
	private bool isConnected = false;
	private const int mtu = 4096;
	private byte[] buffer = new byte[mtu];
	private string ip;
	private int port;
	private bool isStarted = true;

	public TCPClient(string ip, int port, int headerLength)
	{
		this.ip = ip;
		this.port = port;

		sendQueue = new PacketQueue();
		recvQueue = new PacketQueue();
		packetParser = new PacketParser(headerLength);
	}

	public void ConnectRecv()
	{
		socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
		var result = socket.BeginConnect(ip, port, null, null); 

		try
		{
			if (result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(5000), false)) {

				socket.EndConnect(result);
				socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
				IsConnected = true;
			}
			else {
				throw new Exception("Time Out");
			}
		}
		catch (Exception e) 
		{
			packetParser.Reset();

			if (socket != null)
			{
				socket.Close();
			}
			socket = null;
		}
	}

	// 끊기.
	public void Disconnect() {


		try 
		{
			socket.Shutdown(SocketShutdown.Both);
		}
		catch (Exception e) {
			Debug.Log(e.Message, logType.TcpIP);
		}
		finally {

			packetParser.Reset();

			if (socket != null) 
			{
				socket.Close();
				socket = null;
			}

			IsConnected = false;
		}
	}

	// 송신처리.
	public int Send(byte[] data, int size)
	{
		if (sendQueue == null) {
			return 0;
		}

		return sendQueue.Enqueue(data, size); ;
	}

	// 수신처리.
	public int Receive(ref byte[] buffer)
	{
		if (recvQueue == null) 
		{
			return 0;
		}

		int temp = recvQueue.Dequeue(ref buffer);
		return temp;
	}

	public void DisconnectThread()
	{
		isStarted = false;
	}

	public void Dispatch()
	{
		while (isStarted == true)
		{
			if (socket != null)
			{
				DispatchSend();
			}

			if (isConnected == false)
			{
				ConnectRecv();
			}

			Thread.Sleep(3);
		}
	}

	// 스레드 측 송신처리
	void DispatchSend()
	{
		try
		{
			byte[] buffer = new byte[mtu];

			int sendSize = sendQueue.Dequeue(ref buffer);

			if (sendSize > 0)
			{
				socket.BeginSend(buffer, 0, sendSize, SocketFlags.None, new AsyncCallback(SendCallBack), socket);
			}
		}
		catch (Exception e)
		{
			Debug.Log(e.Message, logType.TcpIP);
			Disconnect();
		}
	}

	void SendCallBack(IAsyncResult IAR)
	{
		try
		{
			Socket tempSock = (Socket)IAR.AsyncState;
			int nReadSize = tempSock.EndSend(IAR);
		}
		catch (Exception)
		{
			Disconnect();
		}
	}

	void ReceiveCallBack(IAsyncResult IAR)
	{
		try
		{
			Socket tempSock = (Socket)IAR.AsyncState;
			int readSize = tempSock.EndReceive(IAR);

			if (readSize == 0) {
				new Exception("recv = 0");
			}
			else{
				packetParser.Parse(buffer, readSize, (packetBuffer, packetSize) => InsertQueue(packetBuffer, packetSize));
			}

			socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallBack), socket);
		}
		catch (Exception)
		{
			Disconnect();
		}
	}

	void InsertQueue(byte[] buffer, int size)
	{
		recvQueue.Enqueue(buffer, size); 
	}

	public bool IsConnected
	{
		set
		{
			isConnected = value;
		}
		get
		{
			return isConnected;
		}

	}
}
