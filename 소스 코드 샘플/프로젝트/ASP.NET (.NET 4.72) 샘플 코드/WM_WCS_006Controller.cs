using System.Web.Http;
using Newtonsoft.Json.Linq;
using IMS.COMMON.REST.Message;

namespace IMS.API.SERVER.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WM_WCS_006Controller : BaseController
    {
        // GET: api/WM_WCS_006
        //public string Get()
        //{
        //    return "value";
        //}

        // POST: api/WM_WCS_006
        public JObject Post([FromBody] JObject value)
        {
            return base.POST<SP_IF_WMS_R_006_QC_PRC>(value);
        }
    }
}
