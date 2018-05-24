using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;

namespace RM.Common.DotNetCode
{
    public class WxHelper
    {
        /// <summary>
        /// 发送核销
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="openid"></param>
        /// <param name="ticketname"></param>
        /// <param name="num"></param>
        public static string SendTemplateMsg(string hotelcode, string openid, string ticketname, string num)
        {
            try
            {
                var paramData = new
               {
                   first = new
                   {
                       value = "你好，优惠券核销成功",
                       color = "#173177",
                   },
                   keyword1 = new
                   {
                       value = ticketname,
                       color = "#173177",
                   },
                   keyword2 = new
                   {
                       value = num,
                       color = "#173177",
                   },
                   keyword3 = new
                   {
                       value = DateTime.Now.ToString("yyyy-MM-dd"),
                       color = "#173177",
                   },
                   reamrk = new
                   {
                       value = "",
                       color = "#173177",
                   },
               };
                var postJson = JsonConvert.SerializeObject(paramData);
                var postData = "hotelcode=" + hotelcode + "&openid=" + openid + "&param=" + postJson + "&templateName=consume";
                var result = HttpHepler.SendPost(Config.SendTemplateUrl, postData);

                return result;
            }
            catch (Exception ex)
            {
                return "false";
 
            }
        }
    }
}
