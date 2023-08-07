using System.Web.Http;
using Newtonsoft.Json.Linq;
using IMS.COMMON.REST.Message;

namespace IMS.API.SERVER.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WM_WCS_003Controller : BaseController
    {
        // GET: api/WM_WCS_003
        //public string Get()
        //{
        //    return "value";
        //}

        // POST: api/WM_WCS_003
        public JObject Post([FromBody] JObject value)
        {
            return base.POST<SP_IF_WMS_R_003_PRDT_EXCPT_PA_PRC>(value);
        }
    }
}
