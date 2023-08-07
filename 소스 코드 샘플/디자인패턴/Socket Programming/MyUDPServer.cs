
public class MyUDPServer : MySocket
{
	private UDPServer socket = null;

	public override void Run()
	{
		socket = new UDPServer(int.Parse(port));
		socket.StartServer();
		thread = new Thread(new ThreadStart(ThreadRun));
		thread.Start();
	}

	private void ThreadRun()
	{
		socket.Dispatch();
	}

	public override void Send(byte[] msg)
	{
		socket.Send(msg, msg.Length);
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
					msg.SetMsgData(array, headerInfo);
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
		socket.StopServer();
	}
}