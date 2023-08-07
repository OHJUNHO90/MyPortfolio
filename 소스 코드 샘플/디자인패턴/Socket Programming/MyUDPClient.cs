
public class MyUDPClient : MySocket
{
	private UDPClient socket = null;

	public override void Run()
	{
		socket = new UDPClient(ip, int.Parse(port));
		socket.StartUdpClntConnect();
	}

	public override void Send(byte[] msg)
	{
		if (socket == null)
		{
			return;
		}

		socket.Send(msg);
	}

	public override void Disconnect()
	{
		if (socket != null)
			socket.EndUdpClntConnect();
	}
}