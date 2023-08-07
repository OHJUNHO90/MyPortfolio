using System;
using IMS.COMMON.Database;
using IMS.COMMON.REST.Message;
using IMS.COMMON.RelatedToProcedures;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using System.ComponentModel;

namespace IMS.API.SERVER.Controllers
{
    public class BaseController : ApiController
    {
        protected JObject POST<T>(JObject value) where T : APIServerReceiveMessage
        {          
            typeof(T).GetCustomAttributes(typeof( DescriptionAttribute ), false).
                                          ForEach(x => Logger.Debug("Messaging", $" {(x as DescriptionAttribute).Description}"));

            string status = StatusOfReplyMessage.SUCCESS;
            string msg    = StatusOfReplyMessage.MESSAGE_OK;

            DBHelper databaseManager = new DBHelper();

            var receiveMessage = JsonConvert.DeserializeObject<T>(value.ToString());

            var PI_USER_ID = new GeneratorOfPropertyInfo("PI_USER_ID", DefinedConstantsRelatedToProcedureCalls.PI_USER_ID);
            string result = string.Empty;

            databaseManager.DMLProcedure( ( value, typeof(T).Name, System.Data.CommandType.StoredProcedure),
                                            PI_USER_ID.Value, ref result );

            if (result.ToLower().Equals("null") == false)
            {
                status = StatusOfReplyMessage.FAILED;
                msg = $"{StatusOfReplyMessage.MESSAGE_FAILED}{result}";
            }

            return new ReplyMsgGenerator().GenerateReplyMsg( receiveMessage.INFO.PATH, 
                                                             receiveMessage.INFO.JOBID, 
                                                             status, msg);
        }
    }
}