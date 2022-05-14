using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class SocketAsyncEventArgsPoolManager
    {
        private static SocketAsyncEventArgsPoolManager instance = null;
        public static SocketAsyncEventArgsPoolManager Instance
        {
            private set { }
            get 
            {
                if (instance == null)
                {
                    instance = new SocketAsyncEventArgsPoolManager();
                }

                return instance;
            }
        }

        // READ, WRITE 
        private const byte ALLOTMENT_COUNT = 2;
        private const short MAX_CONNECTIONS_COUNT = 10000;
        private const short BUFFER_SIZE = 1024;

        SocketAsyncEventArgsPool receiveEventArgsPool;
        SocketAsyncEventArgsPool sendEventArgsPool;
        BufferManager bufferManager;

        public SocketAsyncEventArgsPoolManager()
        {

        }

        public void Initialize()
        {
            //버퍼 할당, 버퍼의 크기는 동시 접속자 수 * 하나의 버퍼 크기 * READ, WRITE
            bufferManager = new BufferManager(MAX_CONNECTIONS_COUNT * BUFFER_SIZE * ALLOTMENT_COUNT, BUFFER_SIZE);
            bufferManager.InitBuffer();

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

        public SocketAsyncEventArgs PopReceiveEventArg()
        {
            return receiveEventArgsPool.Pop();
        }

        public SocketAsyncEventArgs PopSendEventArg()
        {
            return sendEventArgsPool.Pop();
        }

        public void PushReceiveEventArg(SocketAsyncEventArgs args)
        {
            receiveEventArgsPool.Push(args);
        }

        public void PushSendEventArg(SocketAsyncEventArgs args)
        {
            sendEventArgsPool.Push(args);
        }
    }
}
