using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    // -- 버퍼 셋팅
    // 소켓 셋팅
    //// bind, listen, accept
    //// 클라이언트 접속
    //// 클라이언트 접속 종료


    // 패킷 수신
    // 패킷 처리
    class TcpServer
    {
        Socket gatekeeper = null;
        private const short MAX_CONNECTIONS_COUNT = 10000;
        private const short BUFFER_SIZE = 1024;
        // READ, WRITE 
        private const byte ALLOTMENT_COUNT = 2;
        private const int PORT = 7979;
        private const int BACKLOG = 100;

        SocketAsyncEventArgsPool receiveEventArgsPool;
        SocketAsyncEventArgsPool sendEventArgsPool;
        SocketAsyncEventArgs acceptArgs;
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

        void AllocateReceiveEventArgsPool()
        {
            receiveEventArgsPool = new SocketAsyncEventArgsPool(MAX_CONNECTIONS_COUNT);

            for (int i = 0; i < MAX_CONNECTIONS_COUNT; i++)
            {
                receiveEventArgsPool.Push(AllocateSocketAsyncEventArg());
            }
        }

        void AllocateSendEventArgsPool()
        {
            sendEventArgsPool = new SocketAsyncEventArgsPool(MAX_CONNECTIONS_COUNT);
            for (int i = 0; i < MAX_CONNECTIONS_COUNT; i++)
            {
                sendEventArgsPool.Push(AllocateSocketAsyncEventArg());
            }
        }

        SocketAsyncEventArgs AllocateSocketAsyncEventArg()
        {
            SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
            UserToken token = new UserToken();
            arg.Completed  += new EventHandler<SocketAsyncEventArgs>(OnReceiveCompleted);
            arg.UserToken = token;
            bufferManager.SetBuffer(arg);

            return arg;
        }

        void Listen()
        {
            gatekeeper = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(address, PORT);

            try
            {
                gatekeeper.Bind(endPoint);
                gatekeeper.Listen(BACKLOG);

                acceptArgs = new SocketAsyncEventArgs();
                acceptArgs.Completed += new EventHandler<SocketAsyncEventArgs>(OnAceptCompleted);



            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } 

        void OnAceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            
        }


        /*수신 완료 이벤트*/
        void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        { 
            
        }

        /*전송 완료 이벤트*/
        void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {

        }
    }
}
