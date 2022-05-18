using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    /// <summary>
    /// 연결된 클라이언트와 1:1로 대응되는 객체.
    /// 접속한 유저가 1000명이라면 이 객체도 1000개가 생성 됨.
    /// </summary>
    class UserToken
    {
        MessageHandler messageHandler = null;
        public Socket socket { get; set; }
        public SocketAsyncEventArgs receiveEventArgs { get; private set; }
        public SocketAsyncEventArgs sendEventArgs { get; private set; }

        MessageResolver messageResolver;

        private object sendlockObj;
        Queue<Packet> sendingQueue;

        public UserToken()
        {
            messageResolver = new MessageResolver();
            messageHandler  = new MessageHandler(this);
            sendlockObj     = new object();
            sendingQueue    = new Queue<Packet>();
            SetGuid();
        }

        void SetGuid()
        {
            // TO DO: 아주 낮은 확률로 동일한 GUID가 나왔을때를 대비하는 예외처리가 필요 함.
            // 게임 기능이 어느 정도 완료된 이후 리팩토링이 필요, 구조적 개선이 필요해 보임.
            // USER TOKEN 객체가 시리얼 넘버를 생성, 포함하고 있는게 책임에 부합한지 의문.
            messageHandler.serialNumber = Guid.NewGuid().ToString();
        }


        public void SetEventArgs(SocketAsyncEventArgs receiveEventArgs, SocketAsyncEventArgs sendEventArgs)
        {
            this.receiveEventArgs = receiveEventArgs;
            this.sendEventArgs = sendEventArgs;
        }

        public void OnNewClient()
        {
            bool pending = socket.ReceiveAsync(receiveEventArgs);

            //동기로 완료될 경우 직접 이벤트 처리 함수 호출.
            if (!pending)
            {
                OnReceiveCompleted(null, receiveEventArgs);
            }
        }

        /*수신 완료 이벤트*/
        public void OnReceiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("OnReceiveCompleted");

            //이벤트 처리
            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
            {
                messageResolver.ReadBytes(e.Buffer, e.Offset, e.BytesTransferred, ReceiveEventProcessing);

                //TO DO: 동기 처리 로직 중복 발생, 중복 로직 제거 필요.
                bool pending = socket.ReceiveAsync(e);
                if (!pending)
                {
                    OnReceiveCompleted(null, e);
                }
            }
            else
            {
                Console.WriteLine(string.Format("error {0},  transferred {1}", e.SocketError, e.BytesTransferred));
                CloseSocket();
            }
        }

        void ReceiveEventProcessing(Const<byte[]> array)
        {
            Console.WriteLine("ReceiveEventProcessing");
            messageHandler.OnMessage(array);
        }


        public void SendMessage(Packet packet)
        {
            lock (sendlockObj)
            {
                sendingQueue.Enqueue(packet);
                SendMessage();
            }
        }

        /// <summary>
        /// 락 스코프 영역내에서 호출되는 함수
        /// </summary>
        private void SendMessage()
        {
            // 전송이 아직 완료된 상태가 아니므로 데이터만 가져오고 큐에서 제거하진 않는다.
            Packet msg = this.sendingQueue.Peek();

            // 바디 길이를 헤더에 쓰기
            msg.RecordHeaderSize();

            // 패킷 사이즈 만큼 버퍼 크기를 설정
            this.sendEventArgs.SetBuffer(sendEventArgs.Offset, msg.position);

            // 패킷 내용을 SocketAsyncEventArgs 버퍼에 복사
            Array.Copy(msg.buffer, 0, sendEventArgs.Buffer, this.sendEventArgs.Offset, msg.position);

            // 비동기 전송 시작.
            bool pending = this.socket.SendAsync(sendEventArgs);
            if (!pending)
            {
                OnSendCompleted(null, sendEventArgs);
            }
        }


        /// <summary>
        /// 락 스코프 영역내에서 호출될수도 아닐수도 있는 함수
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*전송 완료 이벤트*/
        public void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine(string.Format("OnSendCompleted : {0}, transferred {1}", e.SocketError, e.BytesTransferred));

            //전송 완료된 패킷을 큐에서 제거
            //TO DO: 중첩 락 코드 수정 필요 
            lock (sendlockObj)
            {
                /*만약 전송 실패가 일어났다면..*/
                //////////////////////////////////////////////////////////////////////////////////////////////////////
                int size = this.sendingQueue.Peek().position;
                if (e.BytesTransferred != size)
                {
                    string error = string.Format("전송 실패: {0},  packet size {1}", e.BytesTransferred, size);
                    Console.WriteLine(error);
                }
                //////////////////////////////////////////////////////////////////////////////////////////////////////

                this.sendingQueue.Dequeue();

                if (sendingQueue.Count > 0)
                {
                    SendMessage();
                }
            }
        }


        public void ClearQueue()
        { 
        
        }

        void CloseSocket()
        {
            SocketAsyncEventArgsPoolManager.Instance.PushReceiveEventArg(receiveEventArgs);
            SocketAsyncEventArgsPoolManager.Instance.PushSendEventArg(sendEventArgs);
        }
    }
}
