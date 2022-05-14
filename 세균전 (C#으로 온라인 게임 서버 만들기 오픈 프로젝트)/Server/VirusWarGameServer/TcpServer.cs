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

        private const int PORT = 7979;
        private const int BACKLOG = 100;

        public TcpServer()
        {
            SocketAsyncEventArgsPoolManager.Instance.Initialize();
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

                Thread thread = new Thread(Listen);
                thread.Start();
            }
            catch (Exception e)
            {
                /*TO DO: 서버 소켓 생성 실패시 가용성을 위한 처리가 필요*/
                Console.WriteLine(e.Message);
            }
        } 


        void Listen()
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
                Console.WriteLine(string.Format("[{0}] A client connected. handle {1}", Thread.CurrentThread.ManagedThreadId, e.AcceptSocket.Handle));

                SocketAsyncEventArgs receiveArgs = SocketAsyncEventArgsPoolManager.Instance.PopReceiveEventArg();
                SocketAsyncEventArgs sendArgs    = SocketAsyncEventArgsPoolManager.Instance.PopSendEventArg();

                UserToken userToken = receiveArgs.UserToken as UserToken;
                userToken.SetEventArgs(receiveArgs, sendArgs);

                // 생성된 클라이언트 소켓을 보관하여 전송 및 수신에 비동기로 사용.
                userToken.socket = e.AcceptSocket;
                userToken.ProcessReceive();
            }
            else
            {
                /*TO DO: Accept 실패 처리 필요*/
            }

            // 스레드 차단 해제
            autoResetEventAccept.Set();
        }
    }
}
