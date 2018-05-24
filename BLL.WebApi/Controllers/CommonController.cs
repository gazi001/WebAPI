using KS.Common.SDK.AdvancedAPIs;
using KS.Model.Common.CommonRequest;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RM.Common.DotNetHttp;
using System.Net;
using System.IO;
using System.Text;
using BLL.WebApi.Model;
using KS.Ticket.SDK.AdvancedAPIs;

namespace BLL.WebApi.Controllers
{
    public class CommonController : BaseController
    {
        //
        // GET: /Common/

        public ActionResult Index()
        {
            return View();
        }
        private JsonReturn jsonResult = new JsonReturn();
        [HttpPost]
        public JsonResult GetOpenIdForWxOpen(GetOpenIdForWxOpenModel data)
        {
            var url = "https://api.weixin.qq.com/sns/jscode2session?appid=" + data.appid + "&secret=" + data.secret + "&js_code=" + data.code + "&grant_type=authorization_code";
            var result = SendGet(url);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = result;
            return MyJson(result);
        }
        public static string SendGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/json";
            // request.ContentLength = Encoding.UTF8.GetByteCount(PostData);
            // Stream myRequestStream = request.GetRequestStream();
            //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            //myStreamWriter.Write(PostData);
            // myStreamWriter.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
        /// <summary>
        /// 查询是否是会员
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Getbosscard(GetBosscardModel data)
        {
            var result = CommonApi.Getbosscard(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        
        [HttpPost]//小程序注册
        public JsonResult Register(RegisterModel data)
        {
            var db = new MPDBEntities();
            var hotelInfo = db.MPConfigs.Where(x => x.ShopCode == data.hotelcode).FirstOrDefault();
            if (hotelInfo != null)
            {
                var checkopenid = CommonApi.CheckOpenidAndCardByHotelCode(data.openid, data.mobile, hotelInfo.YQTHotelgrouptype, data.hotelcode, data.name, data.source);
                if (checkopenid.returncode == "success")
                {
                    bool YQTGiftId = true;
                    var MPDB = new MPDBEntities();
                    var hotel = MPDB.MPConfigs.Where(x => x.ShopCode == data.hotelcode).FirstOrDefault();
                    var result1 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTGiftId, data.hotelcode);
                    if (result1.returncode == "false")
                    {
                        var isSend = TicketApi.SetNewHyJson(hotel.YQTOperatorId, hotel.YQTOperatorId,data.hotelcode, hotelInfo.YQTGiftId, data.mobile);
                        if (isSend.returncode != "success")
                        {
                            YQTGiftId = false;
                        }
                    }
                    bool YQTRechargeableId = true;
                    var result2 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTRechargeableId, data.hotelcode);
                    if (result2.returncode == "false")
                    {
                        var isSend = TicketApi.SetNewHyJson(hotel.YQTOperatorId, hotel.YQTOperatorId, data.hotelcode, hotelInfo.YQTRechargeableId, data.mobile);
                        if (isSend.returncode != "success")
                        {
                            YQTRechargeableId = false;
                        }
                    }

                    bool YQTregisterId = true;
                    var result3 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTregisterId, data.hotelcode);
                    if (result3.returncode == "false")
                    {
                        var isSend = TicketApi.SetNewHyJson(hotel.YQTOperatorId, hotel.YQTOperatorId, data.hotelcode, hotelInfo.YQTregisterId, data.mobile);
                        if (isSend.returncode != "success")
                        {
                            YQTregisterId = false;
                        }
                    }
                    if (YQTGiftId && YQTregisterId && YQTRechargeableId)
                    {
                        jsonResult.code = ApiCode.成功;
                        jsonResult.msg = "成功";
                    }
                    else
                    {
                        jsonResult.code = ApiCode.发送产品失败;
                        jsonResult.msg = "发送产品失败";
                    }

                }
                else
                {
                    jsonResult.code = ApiCode.注册失败;
                    jsonResult.msg = "发送产品失败";
                }
            }
            else
            {
                jsonResult.code = ApiCode.没有实体数据;
                jsonResult.msg = "没有酒店数据";
            }
            return this.MyJson(jsonResult);
        }

        [HttpPost]
        public JsonResult SendMsg(string mobile, string type, string param, string hotelcode)
        {
            jsonResult.code = ApiCode.成功;
            jsonResult.data = CommonApi.Msg(mobile, type, param, hotelcode, "");
            return this.MyJson(jsonResult);
        }
        [HttpPost]
        public JsonResult GetPrintTicket(string hotelcode)
        {
            var db = new MPDBEntities();
            var data = db.PrintTickets.Where(x => x.hotelcode == hotelcode);
            jsonResult.code = ApiCode.成功;
            jsonResult.data = data;
            return this.MyJson(jsonResult);
        }

    }
}
