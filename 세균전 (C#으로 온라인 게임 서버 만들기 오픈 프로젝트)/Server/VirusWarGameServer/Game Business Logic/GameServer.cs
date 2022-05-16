using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class GameServer
    {
        private GameServer() { }

        private static GameServer instance = null;
        public static GameServer Instance{
            private set { }
            get
            {
                if (instance == null)
                {
                    instance = new GameServer();
                }

                return instance;
            }

        }

        public void Initialize()
        {

        }

        public void OnNewClient(UserToken token)
        {
            token.ProcessReceive();
        }

    }
}
