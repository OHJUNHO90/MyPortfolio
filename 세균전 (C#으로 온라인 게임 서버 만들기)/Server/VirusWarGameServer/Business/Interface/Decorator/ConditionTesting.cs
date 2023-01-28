using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    public abstract class ConditionTesting : iDecorator
    {
        public abstract bool CheckLogic(params object[] datas);
        protected iDecorator decorator;

        public ConditionTesting(iDecorator decorator)
        {
            this.decorator = decorator;
        }

        public bool Testing(params object[] datas)
        {
            bool IsNotError = true;

            if (decorator != null)
            {
                IsNotError = decorator.Testing(datas);
            }
                
            return IsNotError & CheckLogic(datas);
        }
    }
}
