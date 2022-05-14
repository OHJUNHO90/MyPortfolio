﻿using System;
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
        public Socket socket { get; set; }
        public SocketAsyncEventArgs receiveEventArgs { get; private set; }
        public SocketAsyncEventArgs sendEventArgs { get; private set; }

        MessageResolver messageResolver;
        public UserToken()
        {
            messageResolver = new MessageResolver();
        }

        public void SetEventArgs(SocketAsyncEventArgs receiveEventArgs, SocketAsyncEventArgs sendEventArgs)
        {
            this.receiveEventArgs = receiveEventArgs;
            this.sendEventArgs = sendEventArgs;
        }

        public void ProcessReceive()
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
            //    
            Console.WriteLine("ReceiveEventProcessing");
        }

        
        /*전송 완료 이벤트*/
        public void OnSendCompleted(object sender, SocketAsyncEventArgs e)
        {
            Console.WriteLine("OnSendCompleted");
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
