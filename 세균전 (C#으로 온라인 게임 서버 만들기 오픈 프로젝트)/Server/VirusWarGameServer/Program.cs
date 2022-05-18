using System;

namespace VirusWarGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Game Server 객체 생성
            GameServer.Instance.Initialize();

            //TCP SERVER.
            TcpServer tcpServer = new TcpServer();
            tcpServer.Start();

            //PACEKT BUFFER.

            //EVENT LOOP.

            /*프로그램 종료를 막는다*/
            while (true)
            {
                string input = Console.ReadLine();
                System.Threading.Thread.Sleep(1000);
            }

        }
    }
}
