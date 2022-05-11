
public class UDPClient
{
	string ip;
	int port;

	private UdpClient udpClient;
	private bool isUdpClntConnecting { get; set; }

	public UDPClient(String ip, int port)
	{
		this.ip = ip;
		this.port = port;
	}

	public void Start()
	{
		udpClient = new UdpClient(ip, port);
		isUdpClntConnecting = true;
	}

	public void Close()
	{
		if (isUdpClntConnecting)
		{
			udpClient.Close();
			isUdpClntConnecting = false;
		}
	}

	public void Send(byte[] data)
	{
		udpClient.Send(data, data.Length);
	}
}