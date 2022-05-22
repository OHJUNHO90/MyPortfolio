using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public class TheSamePlace : ConditionTesting
    {
        public TheSamePlace(iDecorator decorator = null) : base(decorator)
        {

        }

        public override bool CheckLogic(params object[] datas)
        {
            return !datas[0].Equals(datas[1]);
        }
    }
}
