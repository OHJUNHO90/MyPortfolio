using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class Cell
    {
        public byte PlayerIndex { set; get; }
        public short Row { set; get; }
        public short State { set; get; }

    }
}
