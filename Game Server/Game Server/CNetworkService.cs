using System;
using System.Net.Sockets;

public class CNetworkService
{
	private const int MAX_CONNECTIONS = 100;

	CListener clinet_listener;
	SocketAsyncEventArgsPool receive_event_args_pool;
	SocketAsyncEventArgsPool send_event_args_pool;
	BufferManager buffer_manager;

	public delegate void SessionHandler(CUserToken token);
	public SessionHandler session_created_callback { get; set; }


	public CNetworkService()
	{
		receive_event_args_pool = new SocketAsyncEventArgsPool(MAX_CONNECTIONS);
		send_event_args_pool    = new SocketAsyncEventArgsPool(MAX_CONNECTIONS);

		for (int i = 0; i < MAX_CONNECTIONS; i++)
		{
			CUserToken token = new CUserToken();
			SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
			arg.Completed += new EventHandler<SocketAsyncEventArgs>(Receive_Completed);
			arg.UserToken = token;

			this.receive_event_args_pool.Push(arg);
		}

		for (int i = 0; i < MAX_CONNECTIONS; i++)
		{
			CUserToken token = new CUserToken();
			SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
			arg.Completed += new EventHandler<SocketAsyncEventArgs>(Send_Completed);
			arg.UserToken = token;

			this.send_event_args_pool.Push(arg);
		}


	}

    public void Listen(string host, int port, int backlog)
	{
		CListener listener = new CListener();
		listener.callback_on_newClient += OnNewClient;
		listener.Start(host, port, backlog);
	}

	private void Receive_Completed(object sender, SocketAsyncEventArgs e)
    {
        throw new NotImplementedException();
    }
	private void Send_Completed(object sender, SocketAsyncEventArgs e)
	{
		throw new NotImplementedException();
	}

	private void OnNewClient(Socket client_socket, object token)
    {
        throw new NotImplementedException();
    }
}
