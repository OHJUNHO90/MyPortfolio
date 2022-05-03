public class MySocket
{
	public bool isConnected = false;

	public class HeaderInfo
	{
		public string type = string.Empty;
		public int length = 0;
		public int order = 0;
		public string dt_id = string.Empty;
	}
	
	public int headerLength = 0;
	public Dictionary<string, HeaderInfo> headerInfo = new Dictionary<string, HeaderInfo>();
	protected List<MsgData> receiveMsg = new List<MsgData>();
	
	public string hd_id = string.Empty;
	public string id = null;
	public string ip = null;
	public string port = null;

	protected Thread thread = null;
	protected object obj = null;


	public virtual void Send(byte[] msg) { }
	public virtual MsgData Receive() { return null; }

	public virtual void DisconnectThread() { }
	public virtual void Disconnect() { }
	public virtual void Run() { }
}