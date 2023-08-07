using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.API.CLIENT.Business.Http.Model
{
    internal class Header
    {
        public string sPath { get; set; }
        public string jobId { get; set; }
        public string interfaceStatus { get; set; }
        public string interfaceMsg { get; set; }
    }

    internal class IFT_SND_WM_WCS_INFO
    {
        public Header result { set; get; }
    }
}
