using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business
{
    internal class TaskArgs : EventArgs
    { 
        public MC_TSK_STAT dataSet { set; get; }
        public Action<string> callback { set; get; }
        public Action<TaskArgs> postProcessing { set; get; }
        public EventHandler task { set; get; }
    }

    internal class TaskDispatcher
    {
        public void BeginningOfTask(TaskArgs args)
        {
            TaskRun(args);
        }


        async void TaskRun(TaskArgs args)
        {
            await Task.Run(() => {
                args.task.Invoke(this, args);
            });
        }
    }
}
