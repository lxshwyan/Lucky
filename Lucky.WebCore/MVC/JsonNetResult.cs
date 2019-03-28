using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Serialization;


namespace Lucky.WebCore.MVC
{
    public class JsonNetResult : System.Web.Mvc.JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            var jsonSerializerSetting = new JsonSerializerSettings();
             //首字母小写
            jsonSerializerSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //年月日
            jsonSerializerSetting.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            var json = JsonConvert.SerializeObject(Data, Formatting.None, jsonSerializerSetting);

            response.Write(json);

        }
    }
}
