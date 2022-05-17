using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class Packet
    {
        static readonly short HEADERSIZE = 2;

        public byte[] buffer { get; set; }
        Command command { get; set; }

        public int position { get; private set; }
        public short protocol_id { get; private set; }


        public Packet(short protocol_id)
        {
            buffer = new byte[1024];
            RecordHeaderSize();

            this.protocol_id = protocol_id;
            byte[] temp_buffer = BitConverter.GetBytes(protocol_id);
            temp_buffer.CopyTo(this.buffer, HEADERSIZE);
            position = HEADERSIZE + temp_buffer.Length;
        }

        public Packet(byte[] buffer)
        {
            this.buffer = buffer;
        }

        public void AddBody(byte body)
        {
            byte[] temp_buffer = BitConverter.GetBytes(body);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += sizeof(byte);
        }

        public short GetProtocolId()
        {
            return BitConverter.ToInt16(buffer, HEADERSIZE);
        }

        void RecordHeaderSize()
        {
            byte[] header = BitConverter.GetBytes(HEADERSIZE);
            header.CopyTo(buffer, 0);
        }
    }
}
