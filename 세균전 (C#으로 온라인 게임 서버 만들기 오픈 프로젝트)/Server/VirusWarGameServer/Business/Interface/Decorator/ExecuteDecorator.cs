using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class ExecuteDecorator
    {
        public bool Execute(iDecorator component = null, params object[] datas)
        {
            return component.Testing(datas);
        }
    }
}
