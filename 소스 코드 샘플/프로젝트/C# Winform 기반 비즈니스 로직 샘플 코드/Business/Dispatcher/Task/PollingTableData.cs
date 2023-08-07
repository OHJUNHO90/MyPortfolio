using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business
{
    internal class PollingTableData : iBusinessTask
    {
        public void Execute(object sender, EventArgs args)
        {
            Console.WriteLine("Start PollingTableData: " + Thread.CurrentThread.ManagedThreadId);
            PollingTheSpecificColumnsWithPostProcessing(args);
        }

        async void PollingTheSpecificColumnsWithPostProcessing(EventArgs args)
        {
            var message = string.Empty;
            try
            {
                while (true)
                {
                    if (0 < new PollingTheSpecificColumns().Polling((args as TaskArgs).dataSet.TASK_NO))
                    {
                        message = await new HTTPRequest((args as TaskArgs).dataSet).PostAsync();
                        (args as TaskArgs).callback.Invoke(message);
                        Logger.logManager.Info(message);
                    }

                    await Task.Delay((args as TaskArgs).dataSet.MILLISECONDS);
                }
            }
            catch (Exception e)
            {
                message = $"Exception: {e}";
            }
            finally
            {
                Logger.logManager.Error(message);
                (args as TaskArgs).callback.Invoke(message);
                (args as TaskArgs).postProcessing((args as TaskArgs));
            }
        }
    }
}
