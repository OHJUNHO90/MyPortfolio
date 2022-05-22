using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public interface iDecorator
    {
        bool Testing(params object[] datas);
    }
}
