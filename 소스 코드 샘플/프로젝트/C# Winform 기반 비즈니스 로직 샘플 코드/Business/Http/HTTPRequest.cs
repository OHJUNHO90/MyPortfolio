using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business
{
    internal class HTTPRequest
    {
        MC_TSK_STAT rowInfo = new MC_TSK_STAT();
        public HTTPRequest(MC_TSK_STAT data) 
        {
            rowInfo = data;
        }

        public async Task<string> PostAsync()
        {
            return await PostAsync(rowInfo.IP);
        }

        async Task<string> PostAsync(string requestUrl)
        {
            DBConnector dBConnector = new DBConnector();

            string result = string.Empty;
            dBConnector.GetDataFromProcInClobFormat($"SP_{rowInfo.TASK_NO}_PRC", ref result);

            StringContent jsonContent = new StringContent(result, Encoding.UTF8, "application/json");

            using (var response = await new HttpClient().PostAsync(requestUrl, jsonContent))
            {
                return await new HttpPostprocessing().Postprocessing(response, rowInfo.TASK_NO);
            }
        }
    }
}
