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
        public Socket socket { get; set; }
        public SocketAsyncEventArgs receiveEventArgs { get; private set; }
        public SocketAsyncEventArgs sendEventArgs { get; private set; }
    }
}
