using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class MessageHandler
    {
        public UserToken owner { private set; get; }
        public MessageHandler(UserToken owner)
        {
            this.owner = owner;
        }

        public void OnMessage(Const <byte[]> buffer) 
        {
            
        }

    }
}
