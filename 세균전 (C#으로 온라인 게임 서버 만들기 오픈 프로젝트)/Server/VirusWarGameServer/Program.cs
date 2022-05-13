using System;

namespace VirusWarGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
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
