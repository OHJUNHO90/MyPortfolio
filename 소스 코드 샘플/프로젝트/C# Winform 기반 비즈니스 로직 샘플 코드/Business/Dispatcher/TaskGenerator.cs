using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business.Dispatcher
{
    internal class TaskGenerator
    {
        public TaskArgs Generate(MC_TSK_STAT dataSet, 
                                  Action<string> callBack, 
                                  iBusinessTask eventHandler,
                                  Action<TaskArgs> postProcessing)
        {
            var jobArgs = new TaskArgs();
            jobArgs.dataSet = dataSet;
            jobArgs.callback = callBack;
            jobArgs.task = eventHandler.Execute;
            jobArgs.postProcessing = postProcessing;

            return jobArgs;
        }
    }
}
