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
        public int position { get; private set; }
        public short protocol_id { get; private set; }


        public Packet(short protocol_id)
        {
            buffer = new byte[1024];

            this.protocol_id = protocol_id;
            byte[] temp_buffer = BitConverter.GetBytes(protocol_id);
            temp_buffer.CopyTo(this.buffer, HEADERSIZE);
            position = HEADERSIZE + temp_buffer.Length;
        }

        public Packet(byte[] buffer)
        {
            this.buffer = buffer;
        }

        /// <summary>
        /// TO DO: 가변 인자형식으로 변경이 필요 함.
        /// </summary>
        /// <param name="body"></param>
        public void AddBody(byte body)
        {
            byte[] temp_buffer = BitConverter.GetBytes(body);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += sizeof(byte);
        }

        public void AddBody(short body)
        {
            byte[] temp_buffer = BitConverter.GetBytes(body);
            temp_buffer.CopyTo(this.buffer, this.position);
            this.position += sizeof(short);
        }

        public short GetProtocolId()
        {
            return BitConverter.ToInt16(buffer, HEADERSIZE);
        }

        public void RecordHeaderSize()
        {
            byte[] header = BitConverter.GetBytes((short)(position - HEADERSIZE));
            header.CopyTo(buffer, 0);
        }
    }
}
