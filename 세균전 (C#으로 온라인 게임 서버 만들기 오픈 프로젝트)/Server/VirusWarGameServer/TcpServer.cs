using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    // -- 버퍼 셋팅
    // -- 소켓 셋팅
    // -- bind, listen, accept
    // -- 클라이언트 접속
    // 비동기 receive 처리
    // 비동기 send 처리
    // 클라이언트 접속 종료

    // 패킷 수신
    // 패킷 처리
    class TcpServer
    {
        Socket gatekeeper = null;
        AutoResetEvent autoResetEventAccept;
        SocketAsyncEventArgs acceptAsyncEventArgs;

        private const short MAX_CONNECTIONS_COUNT = 10000;
        private const short BUFFER_SIZE = 1024;
        // READ, WRITE 
        private const byte ALLOTMENT_COUNT = 2;
        private const int PORT = 7979;
        private const int BACKLOG = 100;

        SocketAsyncEventArgsPool receiveEventArgsPool;
        SocketAsyncEventArgsPool sendEventArgsPool;
        BufferManager bufferManager;

        public TcpServer()
        {
            InitBufferManager();
            InitEventArgsPool();
        }

        void InitBufferManager()
        {
            //버퍼 할당, 버퍼의 크기는 동시 접속자 수 * 하나의 버퍼 크기 * READ, WRITE
            bufferManager = new BufferManager(MAX_CONNECTIONS_COUNT * BUFFER_SIZE * ALLOTMENT_COUNT, BUFFER_SIZE);
            bufferManager.InitBuffer();
        }

        void InitEventArgsPool()
        {
            AllocateReceiveEventArgsPool();
            AllocateSendEventArgsPool();
        }

        //TO DO: 비슷한 비지니스 로직으로 구현됨, 리팩토링 필요 
        void AllocateReceiveEventArgsPool()
        {
            receiveEventArgsPool = new SocketAsyncEventArgsPool(MAX_CONNECTIONS_COUNT);

            for (int i = 0; i < MAX_CONNECTIONS_COUNT; i++)
            {
                SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                UserToken token = new UserToken();
                arg.Completed += new EventHandler<SocketAsyncEventArgs>(token.OnReceiveCompleted);
                arg.UserToken = token;
                bufferManager.SetBuffer(arg);
                receiveEventArgsPool.Push(arg);
            }
        }
        //TO DO: 비슷한 비지니스 로직으로 구현됨, 리팩토링 필요 
        void AllocateSendEventArgsPool()
        {
            sendEventArgsPool = new SocketAsyncEventArgsPool(MAX_CONNECTIONS_COUNT);
            for (int i = 0; i < MAX_CONNECTIONS_COUNT; i++)
            {
                SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
                UserToken token = new UserToken();
                arg.Completed += new EventHandler<SocketAsyncEventArgs>(token.OnSendCompleted);
                arg.UserToken = token;
                bufferManager.SetBuffer(arg);
                sendEventArgsPool.Push(arg);
            }
        }

        public void Start()
        {
            gatekeeper = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(address, PORT);

            try
            {
                gatekeeper.Bind(endPoint);
                gatekeeper.Listen(BACKLOG);

                acceptAsyncEventArgs = new SocketAsyncEventArgs();
                acceptAsyncEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnAcceptCompleted);

                Thread thread = new Thread(Accept);
                thread.Start();
            }
            catch (Exception e)
            {
                /*TO DO: 서버 소켓 생성 실패시 가용성을 위한 처리가 필요*/
                Console.WriteLine(e.Message);
            }
        } 


        void Accept()
        {
            autoResetEventAccept = new AutoResetEvent(false);

            while (true)
            {
                acceptAsyncEventArgs.AcceptSocket = null;

                bool pending = true;
                try
                {
                    pending = gatekeeper.AcceptAsync(acceptAsyncEventArgs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                // AcceptAsync는 비동기 매소드 이지만 동기적으로 수행이 완료될 경우도 있다
                // 리턴값을 확인하여 직접 실행
                if (!pending)
                {
                    OnAcceptCompleted(null, acceptAsyncEventArgs);
                }

                //AutoResetEvent.Set 이벤트 대기, 스레드 차단
                autoResetEventAccept.WaitOne();
            } 
        }

        void OnAcceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                OnNewClient(e.AcceptSocket, e.UserToken);
            }
            else
            {
                /*TO DO: Accept 실패 처리 필요*/
            }

            // 스레드 차단 해제
            autoResetEventAccept.Set();
        }

        void OnNewClient(Socket client, object token)
        {
            Console.WriteLine(string.Format("[{0}] A client connected. handle {1}", Thread.CurrentThread.ManagedThreadId, client.Handle));

            SocketAsyncEventArgs receiveArgs = receiveEventArgsPool.Pop();
            SocketAsyncEventArgs sendArgs    = sendEventArgsPool.Pop();

            UserToken userToken = receiveArgs.UserToken as UserToken;
            userToken.SetEventArgs(receiveArgs, sendArgs);

            // 생성된 클라이언트 소켓을 보관하여 전송 및 수신에 비동기로 사용.
            userToken.socket = client;
            userToken.BeginReceive();
        }
    }
}
