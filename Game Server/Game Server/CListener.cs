using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class CListener
{
	SocketAsyncEventArgs accept_args;
	Socket listen_socket;
	AutoResetEvent flow_control_event;

	public delegate void NewClientHandler(Socket client_socket, object token);
	public NewClientHandler callback_on_newClient;


	public CListener()
	{
		this.callback_on_newClient = null;
	}

	public void Start(string host, int port, int backlog)
	{
		this.listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IPAddress address;

		if (host == "0.0.0.0")
		{
			address = IPAddress.Any;
		}
		else
		{
			address = IPAddress.Parse(host);
		}

		IPEndPoint endpoint = new IPEndPoint(address, port);

		try
		{
			this.listen_socket.Bind(endpoint);
			this.listen_socket.Listen(backlog);

			this.accept_args = new SocketAsyncEventArgs();
			this.accept_args.Completed += new EventHandler<SocketAsyncEventArgs>(on_accept_completed);

			//this.listen_socket.AcceptAsync(this.accept_args);
			Thread listen_thread = new Thread(do_listen);
			listen_thread.Start();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
		}
		
	}

    private void do_listen()
    {
		this.flow_control_event = new AutoResetEvent(false);

		while (true)
		{
			this.accept_args.AcceptSocket = null;
			bool pending = true;

			try
			{
				pending = listen_socket.AcceptAsync(this.accept_args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				continue;
			}


			if (pending == false)
			{
				on_accept_completed(null, this.accept_args);
			}

			this.flow_control_event.WaitOne();
		}  
    }

    private void on_accept_completed(object sender, SocketAsyncEventArgs e)
    {
		if (e.SocketError == SocketError.Success)
		{
			Socket client_socket = e.AcceptSocket;

			if (this.callback_on_newClient != null)
			{
				this.callback_on_newClient(client_socket, e.UserToken);
			}
		}
		else
		{
			Console.WriteLine("Failed to accept client.");
		}

		this.flow_control_event.Set();
    }
}
