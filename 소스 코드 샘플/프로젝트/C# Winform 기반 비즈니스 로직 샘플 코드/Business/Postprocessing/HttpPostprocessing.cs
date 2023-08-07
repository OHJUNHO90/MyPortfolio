using IMS.API.CLIENT.Business.Http.Model;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMS.API.CLIENT.Business
{
    public enum ProcType
    {
        S,  // 성공
        N,  // 신규
        R,  // 재처리 필요
        P,  // 프로시저 무시 항목 지정
        E   // 에러
    }

    public class HttpPostprocessing
    {
        public async Task<string> Postprocessing(HttpResponseMessage httpResponseMessage, string tableName)
        {
            Console.WriteLine("Start Postprocessing: " + Thread.CurrentThread.ManagedThreadId);

            var json = await httpResponseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<IFT_SND_WM_WCS_INFO>(json);

            if (response.result.interfaceStatus.Equals(ProcType.S.ToString())) {
                UpdateProcTypeWithReturnAffectedRows(tableName, response.result.interfaceStatus);
            }
            else {
                UpdateProcTypeWithReturnAffectedRows(tableName, ProcType.E.ToString());
            }

            return $"{response.result.jobId}, {response.result.interfaceStatus}, {response.result.interfaceMsg}";
        }

        int UpdateProcTypeWithReturnAffectedRows(string tableName, string procType)
        {
            return new Update().UpdateProcTypeWithReturnAffectedRows(tableName, procType);
        }
    }
}
