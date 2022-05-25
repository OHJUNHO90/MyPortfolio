using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirusWarGameServer
{
    class ExecuteDecorator
    {
        public Action OnCompleted { set; get; }
        public Action OnOccurredError { set; get; }
        public bool Execute(iDecorator component = null, params object[] datas)
        {
            bool result = component?.Testing(datas) ?? false;

            if (result)
            {
                OnCompleted?.Invoke();
            }
            else
            {
                OnOccurredError?.Invoke();
            }

            return result;
        }
    }
}
