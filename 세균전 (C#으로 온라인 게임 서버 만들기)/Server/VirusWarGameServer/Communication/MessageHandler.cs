﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public class MessageHandler
    {
        public string serialNumber { get; set; }

        public UserToken owner { private set; get; }
        public Packet packet { set; get; }
        public MessageHandler(UserToken owner)
        {
            this.owner = owner;
        }

        public void OnMessage(Const <byte[]> buffer) 
        {
            byte[] array = new byte[1024];
            Array.Copy(buffer.Value, array, buffer.Value.Length);
            packet = new Packet(array);

            GameServer.Instance.EnqueuePacket(this);
        }
    }
}
