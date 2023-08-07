using System;
using System.Threading;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business
{
    internal class ClientConditionReport : iBusinessTask
    {
        private bool isCanceled { set; get; } = true;

        public void Execute(object sender, EventArgs args)
        {
            Report(sender, args);
        }
        
        async void Report(object sender, EventArgs args)
        {
            var message = string.Empty;

            try
            {
                while (isCanceled)
                {
                    new Update().Update_RMK_IN_MC_TSK_STAT();
                    (args as TaskArgs).callback.Invoke($"DB 접속 확인: {DateTime.Now}");
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
