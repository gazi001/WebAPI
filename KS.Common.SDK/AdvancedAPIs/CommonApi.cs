using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;
using KS.Model.Mall.MallResponse;
using Newtonsoft.Json;
using KS.Model.Common.CommonRequest;
using KS.Model.ApiResultModel;
using RM.Common.DotNetHttp;
using RM.Common.DotNetJson;
using KS.Model;
using KS.Model.Common.CommonResponse;
using ServiceStack.Redis;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;


namespace KS.Common.SDK.AdvancedAPIs
{
    public class CommonApi
    {

        static net.kuaishun.interfacecrs.Service myservice = new net.kuaishun.interfacecrs.Service();
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="type"></param>
        /// <param name="param"></param>
        /// <param name="hotelcode"></param>
        public static void SendMsg(string mobile,string type,string param,string hotelcode)
        {
            net.kuaishun.ks.SendSms SendMsg = new net.kuaishun.ks.SendSms();
            SendMsg.SendSameSms(mobile, type, param, "", hotelcode); 
        }
        public static string Msg(string mobile, string type, string param, string hotelcode,string aaa)
        {
            net.kuaishun.ks.SendSms SendMsg = new net.kuaishun.ks.SendSms();
             return SendMsg.SendSameSms(mobile, type, param, "", hotelcode);
        }
        /// <summary>
        /// 查询是否是会员
        /// </summary>
        public static GetBosscardResult Getbosscard(GetBosscardModel data)
        {
            var result = myservice.Getbosscard(data.openid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetBosscardResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 查询微信订单号
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="trade_no"></param>
        /// <param name="notify"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static QueryTradeNoModel QueryTradeNo(string hotelcode, string trade_no, string notify, string url)
        {
            var obj = new QueryTradeNoModel();
            try
            {
                if (trade_no != "")
                {
                    var postData = "hotelcode=" + hotelcode + "&trade_no=" + trade_no + "&Notify=" + notify;
                    var result = HttpHepler.SendPost(url, postData);
                    //var obj = JsonConvert.DeserializeObject<QueryTradeNoModel>(result)
                    var transaction_id = JsonHelper.GetJsonValue(result, "transaction_id");
                    if (transaction_id != "")
                    {
                        obj.transaction_id = transaction_id;
                        obj.code = StatusCode.成功;
                        obj.msg = "成功";
                    }
                    return obj;
                }
                obj.code = StatusCode.参数不全;
                return obj;
            }
            catch (Exception ex)
            {
                obj.msg = ex.ToString();
                obj.code = StatusCode.程序异常;
                return obj;
            }
        }

        public static CheckOpenidResult CheckOpenidAndCardByHotelCode(string openid, string mobile, string type, string hotelcode, string name, string source)
        {
            var result =myservice.Checkopenidandcardbyhotelcode_new_source(openid, mobile, type, hotelcode, name, source);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<CheckOpenidResult>>(result)[0];
            return obj;
        }
        #region  注册
        public static bool Setmemberlist_newtjcode(string hy_name,
                    string hy_kh,
                    string bosscard,
                    string cardhotel,
                    string usertype,
                    string cardtype,
                    string ratecode,
                    string hy_sjhm,
                    string hy_zjlx,
                    string hy_zjhm,
                    string hy_sex,
                    string bosshk,
                    string saleid,
                    string arr,
                    string hy_qq_msn,
                    string weixinnumber,
                    string nation,
                    string country,
                    string state,
                    string remark,
                    string birthday,
                    string hy_email,
                    string id,
                    string grouptype,
                    string tjcode,
                    string source,
                    string address,
            string hotelcode
            )
        {
            var result = myservice.Setmemberlist_newtjcode(hy_name,
                      hy_kh,
                      bosscard,
                      cardhotel,
                      usertype,
                      cardtype,
                      ratecode,
                      hy_sjhm,
                      hy_zjlx,
                      hy_zjhm,
                      hy_sex,
                      bosshk,
                      saleid,
                      arr,
                      hy_qq_msn,
                      weixinnumber,
                      nation,
                      country,
                      state,
                      remark,
                      birthday,
                      hy_email,
                      id,
                      grouptype,
                      tjcode,
                      source,
                      address,"",hotelcode);


            return result;

        }
        #endregion

        //绑定openid
        //public static CheckOpenidResult Checkopenidandcardbyhotelcode_new_source(string openid, string mobile, string type, string hotelcode, string name, string source)
        //{
        //    var result = myservice.Checkopenidandcardbyhotelcode_new_source(openid, mobile, type, hotelcode, name, source);
        //    result = CommonFunction.Replacebracket(result);
        //    var obj = JsonConvert.DeserializeObject<List<CheckOpenidResult>>(result)[0];
        //    return obj;
        //}
        public static void SendXf(string openid, string productType, string name, string accountType, string account, string time, string remark,string hotelcode)
        {
            var paramData = new
            {
                productType = new
                {
                    value = productType,
                    color = "#1C1C1C",
                },
                name = new
                {
                    value = name,
                    color = "#1C1C1C",
                },
                accountType = new
                {
                    value = accountType,
                    color = "#1C1C1C",
                },
                account = new
                {
                    value = account,
                    color = "#1C1C1C",
                },
                time = new
                {
                    value = time,
                    color = "#1C1C1C",
                },
                remark = new
                {
                    value = remark,
                    color = "#1C1C1C",
                }
            };
            var json = JsonConvert.SerializeObject(paramData);
            var TemplateData = "hotelcode=" + hotelcode + "&openid=" + openid + "&param=" + json + "&templateName=xiaofei";
            var result = HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);

        }
        public static void SendConsume(string openid,string first, string name, string num, string addtime, string remark, string hotelcode)
        {
            var paramData = new
            {
                first = new
                {
                    value ="您好票券核销成功",
                    color = "#173177",
                },
                keyword1 = new
                {
                    value = name,
                    color = "#173177",
                },
                keyword2 = new
                {
                    value ="1",
                    color = "#173177",
                },
                keyword3 = new
                {
                    value = addtime,
                    color = "#173177",
                },
            
                reamrk = new
                {
                    value = "期待您的下次光临！",
                    color = "#173177",
                },
            };
            var json = JsonConvert.SerializeObject(paramData);
            var TemplateData = "hotelcode=" + hotelcode + "&openid=" + openid + "&param=" + json + "&templateName=consume";
            var result = HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);
        }
        public static void SendWx(string hotelcode, string channelCode, string system, string ex, string method)
        {

            var paramData = new
            {
                first = new
                {
                    value = "有一条新的bug，嘻嘻",
                    color = "#173177",
                },
                keyword1 = new
                {
                    value = system + Environment.MachineName,
                    color = "#173177",
                },
                keyword2 = new
                {
                    value = DateTime.Now.ToString("yyyy-MM-dd"),
                    color = "#173177",
                },
                keyword3 = new
                {
                    value = ex,
                    color = "#173177",
                },

                reamrk = new
                {
                    value =ex,
                    color = "#173177",
                },
            };
            var json = JsonConvert.SerializeObject(paramData);
            var TemplateData = "hotelcode=KSHZ&openid=oypMdv1H5RuTAQafJzjQ6SJ4glpw&param=" + json + "&templateName=Exception";
            var result = HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);

        }
        public static bool pushMsg(QuerymemberJsonResult hy, CAVTicketModel data)
        {
            try
            {
                using (IRedisClient publisher = new RedisClient(Config.RedisUrl, 6379))
                {
                    var msg = new
                    {
                        title = "快顺核销系统",
                        orderno = data.jycode,
                        addtime = DateTime.Now.ToString("yyyy-MM-dd"),
                        CategoryName = data.CategoryName,
                        money = data.fmoney,
                        FormulaName = data.FormulaName,
                        ticketsn = data.ticketsn,
                        mobile = data.mobile,
                    };
                    var str = JsonConvert.SerializeObject(msg);
                    
                    var isSend = publisher.PublishMessage(data.hotelcode, str);

                    if (isSend > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }
          
        }

        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static GetServiceListModel GetServiceList(string hotelcode, string type, string action)
        {
            var postData = "hotelcode=" + hotelcode + "&type=" + type;
            var result = HttpHepler.SendPost(Config.BookingUrl + "?action=" + action, postData);
            var list = JsonConvert.DeserializeObject<GetServiceListModel>(result);
            return list;
        }
    }

}
