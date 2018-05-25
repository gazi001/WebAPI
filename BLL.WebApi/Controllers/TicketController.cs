using KS.Common.SDK.AdvancedAPIs;
using KS.Mall.SDK.AdvancedAPIs;
using KS.Model;
using KS.Model.Ticket.TicketRequest;
using KS.Ticket.SDK.AdvancedAPIs;
using Newtonsoft.Json;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KS.Model.Mall.MallRequest;
using KS.Model.Ticket.TicketResponse;
using BLL.WebApi.Model;
using System.Threading;
using System.Text.RegularExpressions;
using RM.Common.DotNetCode;
using BLL.WebApi.Helper;
using System.IO;

namespace BLL.WebApi.Controllers
{
    public class TicketController : BaseController
    {
        net.kuaishun.ks.SendSms s = new net.kuaishun.ks.SendSms();
        //net.kuaishun.fx.SendSms s = new net.kuaishun.fx.SendSms();
        //var a = s.SendSameSms("15990104724", "ZZ", "1,2,3,4,5,6", "", "KSHZ");
        //
        // GET: /Ticket/
        private JsonReturn jsonResult = new JsonReturn();
        public ActionResult Index()
        {
         
            //var str = JsonConvert.SerializeObject(data);
            return View();
        }
        #region 云券通前端
        /// <summary>
        /// 查询可用优惠券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult QueryTicketNew(QueryTicketnewModel data)
        {
            var result = TicketApi.QueryTicketNew(data);
            if (result != null)
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "没有数据";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 产品限购查询，判断是否购买上限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetFormulaXg(GetFormulaXgModel data)
        {
            var result = TicketApi.Get_formula_xg_json(data);
            if (result !=null)
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

        /// <summary>
        /// 卡券购买成功处理
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CardPaySuccess(string postData)
        {
            var data = JsonConvert.DeserializeObject<CardPaySuccessModel>(postData);
            var orderInfo = data.orderInfo;
            var tradeno = CommonApi.QueryTradeNo(data.setnewhy.salehotelcode, orderInfo.ordercode, data.notify, Config.CheckWxPayUrl);
            if (tradeno.transaction_id !="null")
            {
                orderInfo.remark = "[已支付]微信订单号：" + tradeno.transaction_id;
                var result = MallApi.Setorder_json(orderInfo);
                if (result.returncode != "false")
                {
                    
                    var ticketResult = TicketApi.SetNewHyJson(data.setnewhy);
                    if (ticketResult.returncode != "false" && ticketResult.returncode!="false2")
                    {
                        jsonResult.code = ApiCode.成功;
                        jsonResult.msg = "接口调用成功";
                    }
                    else
                    {
                        jsonResult.code = ApiCode.发送产品失败;
                        jsonResult.msg = "发送产品失败";
                    }
                }
                else
                {
                    jsonResult.code = ApiCode.订单生成失败;
                    jsonResult.msg = "订单生成失败";
                }
            }
            else
            {
                jsonResult.code = ApiCode.没有找到微信支付订单;
                jsonResult.msg = "没有找到微信支付订单";
            }
            return this.MyJson(jsonResult);
        }

        [HttpPost]//商城发产品
        public JsonResult SetNewHyJson(string postData)
        {
            var data = JsonConvert.DeserializeObject<SetNewHyJsonModel>(postData);
            var ticketResult = TicketApi.SetNewHyJson(data);
            if (ticketResult.returncode != "false" && ticketResult.returncode != "false2")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
            }
            else
            {
                jsonResult.code = ApiCode.发送产品失败;
                jsonResult.msg = "发送产品失败";
            }
            return this.MyJson(jsonResult);
        }

        [HttpPost]
        public JsonResult TEST(string  postData)
        {
            var obj = JsonConvert.DeserializeObject<CardPaySuccessModel>(postData);
            var str = JsonConvert.SerializeObject(obj);
            return this.MyJson(obj);
        }

        public JsonResult Getbosscardbyhotelcodeandsource(GetbosscardbyhotelcodeandsourceModel data)
        {
            var result = TicketApi.Getbosscardbyhotelcodeandsource(data);
            return this.MyJson(result);
        }

        public JsonResult GetMemberBalance(string  postData)
        {
            var data = JsonConvert.DeserializeObject<GetMemberBalance>(postData);
            var db = new MPDBEntities();
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            var t = DateTime.Parse(time);
           
                if (!db.MemeberBalances.Any(x => x.AddTime == t))
                {
                    Thread Thread1 = new Thread(GetMemberBalance);
                    Thread1.IsBackground = true;
                    Thread1.Start(data);
                    //OrderServiceThread(data);
                    jsonResult.code = ApiCode.数据正在生成;
                    jsonResult.msg = "数据正在生成中。。。";
                    return this.MyJson(jsonResult);
                    // jsonResult.data = d;
                }
                else
                {
                    //json=
                    // db.SaveChanges();
                    var d = db.MemeberBalances.Where(x => x.AddTime == t && x.hotelcode == data.p1.hotelcode).ToList();
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";
                    jsonResult.data = d;
                    return this.MyJson(jsonResult);
                }
            
        }

        public JsonResult ReGetMemberBalance(string postData)
        {
            var data = JsonConvert.DeserializeObject<GetMemberBalance>(postData);
            var db = new MPDBEntities();
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            var t = DateTime.Parse(time);
            Thread Thread1 = new Thread(ReGetMemberBalance);
            Thread1.IsBackground = true;
            Thread1.Start(data);
            //OrderServiceThread(data);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "数据正在生成中。。。";
            return this.MyJson(jsonResult);
            // jsonResult.data = d;
        }
        private void ReGetMemberBalance(object obj)
        {
            var data = obj as GetMemberBalance;
            var db = new MPDBEntities();
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            var t = DateTime.Parse(time);
            var list = db.MemeberBalances.Where(x => x.AddTime == t && x.hotelcode == data.p1.hotelcode).ToList();
            foreach (var item in list)
            {
                db.MemeberBalances.Remove(item);
            }
            db.SaveChanges();
            var newlist = TicketApi.Getbosscardbyhotelcodeandsource(data.p1);
            if (!db.MemeberBalances.Any(x => x.AddTime == t)&&newlist.success!=null)
            {

                if (newlist.success.Count > 0)
                {
                    //MemberBalanceModel m = new MemberBalanceModel();
                    foreach (var item in newlist.success)
                    {
                        data.p2.mode = "m";
                        data.p2.code = item.mobile;
                        var member = TicketApi.GetMemberBalance(data.p2);
                        if (member != null)
                        {
                            if (member.tmoney != 0 || member.tmoney_dj != 0 || member.tmoney_xf != 0)
                            {
                                // json.Add(member);
                                if (!db.MemeberBalances.Any(x => x.bosscard == member.bosscard && x.AddTime == t))
                                {
                                    db.MemeberBalances.Add(new MemeberBalance
                                    {
                                        bosscard = member.bosscard,
                                        mobile = member.mobile,
                                        name = member.name,
                                        tmoney = member.tmoney,
                                        tmoney_dj = member.tmoney_dj,
                                        tmoney_gq = member.tmoney_gq,
                                        tmoney_kp = member.tmoney_kp,
                                        tmoney_xf = member.tmoney_xf,
                                        yhqnum = member.yhqnum,
                                        AddTime = DateTime.Parse(time),
                                        hotelcode = data.p1.hotelcode,
                                        realbosscard=member.realbosscard,
                                    });
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void GetMemberBalance(object obj)
        {
            var db = new MPDBEntities();
            var data = obj as GetMemberBalance;
            var list = TicketApi.Getbosscardbyhotelcodeandsource(data.p1);
            var time = DateTime.Now.ToString("yyyy-MM-dd");
            var t = DateTime.Parse(time);
           // var json = new List<MemberBalanceResult>();
            if (!db.MemeberBalances.Any(x => x.AddTime == t)&&list.success!=null)
            {
                if (list.success.Count > 0)
                {
                    //MemberBalanceModel m = new MemberBalanceModel();
                    foreach (var item in list.success)
                    {
                        data.p2.mode = "m";
                        data.p2.code = item.mobile;
                        var member = TicketApi.GetMemberBalance(data.p2);
                        if (member != null)
                        {
                            if (member.tmoney != 0 || member.tmoney_dj != 0 || member.tmoney_xf != 0)
                            {
                                // json.Add(member);
                                if (!db.MemeberBalances.Any(x => x.bosscard == member.bosscard && x.AddTime == t))
                                {
                                    db.MemeberBalances.Add(new MemeberBalance
                                    {
                                        bosscard = member.bosscard,
                                        mobile = member.mobile,
                                        name = member.name,
                                        tmoney = member.tmoney,
                                        tmoney_dj = member.tmoney_dj,
                                        tmoney_gq = member.tmoney_gq,
                                        tmoney_kp = member.tmoney_kp,
                                        tmoney_xf = member.tmoney_xf,
                                        yhqnum = member.yhqnum,
                                        AddTime = DateTime.Parse(time),
                                        hotelcode = data.p1.hotelcode,
                                        realbosscard = member.realbosscard,
                                 
                                    });
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
                
                //jsonResult.code = ApiCode.成功;
                //jsonResult.msg = "接口调用成功";
                //jsonResult.data = json;
            }
        }
        //扣款
        public JsonResult DebitCard(TicketJLAddNotimeSetModel data)
        {
            var result = TicketApi.DebitCard(data);
            return this.MyJson(result);

        }
        //获取个人信息
        public JsonResult Queryticketall_json(QueryTicketAllModel data)
        {
            var result = TicketApi.Queryticketall_json(data);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = result;
            return this.MyJson(result);

        }
        //验证该手机号是否适用活动
        public JsonResult Getticket_user_byregedit(GetticketUserByRegeditModel data)
        {
            var result = TicketApi.Getticket_user_byregedit(data);

                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result[0].returncode;
            return this.MyJson(result);

        }
        /// <summary>
        /// 查所有券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult Queryticketnew_nowx_bdly_json(QueryticketNewNowxBDLYModel data)
        {
            var result = TicketApi.Queryticketnew_nowx_bdly_json(data);
            if (result != null)
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
                jsonResult.data = result;
            }
            else
            {
                jsonResult.code = ApiCode.没有实体数据;
                jsonResult.msg = "接口失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 核销券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult Setticket_yhquse(TicketJLAddTicketSNNewSetModel data)
        {
            var result = TicketApi.Setticket_yhquse_json(data);
            if (result.msg == "success")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";

            }
            else
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = "接口错误";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 获取报表会员绑定数统计，年月日
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult Getticket_jl_hx_json(GetticketJLHXModel data)
        {
            var result = TicketApi.Getticket_jl_hx_json(data);
            if (result != null)
            {
                if (data.type == "1")
                {
                    var ls = result.GroupBy(a => a.userdate.ToString("yyyy")).Select(g => (new { name = g.Key, count = g.Count() }));
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = ls;
                }else if (data.type == "2")
                {

                    var ls = result.GroupBy(a => a.userdate.ToString("yyyy-MM")).Select(g => (new { name = g.Key, count = g.Count() }));
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = ls;
                }
                else if (data.type == "3")
                {
                    //var ls = result.GroupBy(a => a.userdate.ToString("yyyy-MM")).Select(g => (new { name = g.Key, count = g.Count() }));
                  
                    var ls = TicketApi.WeekScreen(data.starttime, data.endtime, data.hotelcode);
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = ls;
                }
                else 
                {
                    var ls = result.GroupBy(a => a.userdate.ToString("yyyy-MM-dd")).Select(g => (new { name = g.Key, count = g.Count() }));
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = ls;
                }
            }
            else
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = "接口错误";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 核销扣款
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult CAVTicket(CAVTicketModel data)
        {
            Random ran = new Random();
            Double a=0;
            var hy_xx = TicketApi.Querymember_json("","M",data.mobile);
            
             var member= TicketApi.Getmember_bymobile("", "", data.grouptype, data.mobile);
            if (hy_xx[0].name != null)
            {
                var mobile =hy_xx[0].Mobile;
                if (data.type == "1")
                {
                    #region
                    try
                    {
                        var db = new MPDBEntities();
                        
                            var config = db.TicketExpire_t.Where(x => x.hotelcode == data.hotelcode).FirstOrDefault();
                            if (config != null)
                            {
                                if (config.deadtime != "")
                                {
                                    data.firstts = config.deadtime;
                                }
                                else
                                {
                                    data.firstts = "1800";
                                }


                                if (config.spacetime != "")
                                {
                                    a = Convert.ToDouble(config.spacetime);
                                }
                                else
                                {
                                    a = 0;
                                }
                            }
                            else
                            {
                                data.firstts = "1800";
                                a = 0;
                            }

                        
                        data.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                        data.userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        using (var dc = new iticketEntities())
                        {
                            var con = dc.CAVRecord_t.Where(x => x.hotelcode == data.hotelcode && x.scene == data.scene && x.categoryid == data.CategoryId && x.mobile == mobile).OrderByDescending(x => x.userdate).FirstOrDefault();

                            if (con == null)
                            {
                                var ls = TicketApi.CAVTicketFirstTS(data);

                                if (ls.IndexOf("success") > -1)
                                {

                                    var da = new CAVRecord_t()
                                    {
                                        channel = data.channel,
                                        time = data.time,
                                        addname = data.addname,
                                        categoryid = data.CategoryId,
                                        deptcode = data.deptcode,
                                        firstts = data.firstts,
                                        fmoney = data.fmoney,
                                        FormulaId = data.FormulaId,
                                        gdcode = data.gdcode,
                                        hotelcode = data.hotelcode,
                                        hotelcodenew = data.hotelcodenew,
                                        jycode = data.jycode,
                                        no = data.no,
                                        scene = data.scene,
                                        sflag = data.sflag,
                                        ticketsn = data.ticketsn,
                                        tmoney = data.tmoney,
                                        tp_id = data.Tp_Id,
                                        ts = data.ts,
                                        usercode = data.usercode,
                                        userdate = Convert.ToDateTime(data.userdate),
                                        usertype = data.usertype,
                                        xfcode = data.xfcode,
                                        xftype = data.xftype,
                                        addtime = DateTime.Now,
                                        mobile = data.mobile,
                                        categoryname = data.CategoryName,
                                        formulaname = data.FormulaName,
                                        username=hy_xx[0].name,
                                        bosscard=hy_xx[0].CardNo,
                                        realbosscard=hy_xx[0].realbosscard,
                                        rate=data.rate
                                    };
                                    dc.CAVRecord_t.Add(da);
                                    dc.SaveChanges();
                                    string M = Regex.Replace(data.mobile, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");

                                    var queryall = new QueryTicketAllModel();
                                    queryall.user = "";
                                    queryall.token = "";
                                    queryall.hotelgroupid = "";
                                    queryall.hotelcode = data.hotelcode;
                                    queryall.mode = "m";
                                    queryall.code = data.mobile;
                                    var pricexx = TicketApi.Queryticketall_json(queryall);
                                    if ((data.sflag == "1" || data.sflag == "2") && data.usertype == "0")
                                    {
                                        
                                        if (pricexx != null)
                                        {
                                            var dx = s.SendSameSms(data.mobile, "KK", "" + M + "," + data.tmoney + "," + pricexx[0].tmoney + "", "", data.hotelcode);
                                        }
                                    }
                                    if (data.sflag == "8")
                                    {
                                        if (member.Count > 0)
                                        {
                                            var id = int.Parse(data.scene);
                                            var scene = db.HotelScene_t.FirstOrDefault(x => x.id == id);
                                            if (scene != null)
                                            {
                                                CommonApi.SendXf(member[0].openid, "消费点", scene.scenename, "会员卡号", hy_xx[0].realbosscard == "" ? hy_xx[0].CardNo : hy_xx[0].realbosscard, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "", data.hotelcode);
                                            }
                                        }
                                    }
                                    jsonResult.code = ApiCode.成功;
                                    jsonResult.msg = ls;
                                    jsonResult.data = da;
                                }
                                else
                                {
                                    jsonResult.code = ApiCode.接口调用失败;
                                    jsonResult.msg = "失败";

                                }
                            }
                            else
                            {
                                DateTime dt1 = Convert.ToDateTime(con.userdate);
                                DateTime dt2 = DateTime.Now;
                                TimeSpan ts = dt1.Subtract(dt2).Duration();
                                Double douLen = ts.TotalSeconds;
                                if (douLen > a)
                                {
                                    var ls = TicketApi.CAVTicketFirstTS(data);

                                    if (ls.IndexOf("success") > -1)
                                    {
                                        var da = new CAVRecord_t()
                                        {
                                            channel = data.channel,
                                            time = data.time,
                                            addname = data.addname,
                                            categoryid = data.CategoryId,
                                            deptcode = data.deptcode,
                                            firstts = data.firstts,
                                            fmoney = data.fmoney,
                                            FormulaId = data.FormulaId,
                                            gdcode = data.gdcode,
                                            hotelcode = data.hotelcode,
                                            hotelcodenew = data.hotelcodenew,
                                            jycode = data.jycode,
                                            no = data.no,
                                            scene = data.scene,
                                            sflag = data.sflag,
                                            ticketsn = data.ticketsn,
                                            tmoney = data.tmoney,
                                            tp_id = data.Tp_Id,
                                            ts = data.ts,
                                            usercode = data.usercode,
                                            userdate = Convert.ToDateTime(data.userdate),
                                            usertype = data.usertype,
                                            xfcode = data.xfcode,
                                            xftype = data.xftype,
                                            addtime = DateTime.Now,
                                            mobile = data.mobile,
                                            categoryname = data.CategoryName,
                                            formulaname = data.FormulaName,
                                            username = hy_xx[0].name,
                                            bosscard = hy_xx[0].CardNo,
                                            realbosscard = hy_xx[0].realbosscard,
                                            rate = data.rate
                                        };
                                        dc.CAVRecord_t.Add(da);
                                        dc.SaveChanges();
                                        string M = Regex.Replace(data.mobile, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");

                                        var queryall = new QueryTicketAllModel();
                                        queryall.user = "";
                                        queryall.token = "";
                                        queryall.hotelgroupid = "";
                                        queryall.hotelcode = data.hotelcode;
                                        queryall.mode = "m";
                                        queryall.code = data.mobile;
                                        var pricexx = TicketApi.Queryticketall_json(queryall);
                                        if ((data.sflag == "1" || data.sflag == "2") && data.usertype == "0")
                                        {
                                           
                                            if (pricexx != null)
                                            {
                                                var dx = s.SendSameSms(data.mobile, "KK", "" + M + "," + data.tmoney + "," + pricexx[0].tmoney + "", "", data.hotelcode);
                                            }
                                        }
                                        if (data.sflag == "8")
                                        {
                                            if (member.Count > 0)
                                            {
                                                var id = int.Parse(data.scene);
                                                var scene = db.HotelScene_t.FirstOrDefault(x => x.id == id);
                                                if (scene != null)
                                                {
                                                    CommonApi.SendXf(member[0].openid, "消费点", scene.scenename, "会员卡号", hy_xx[0].realbosscard == "" ? hy_xx[0].CardNo : hy_xx[0].realbosscard, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "", data.hotelcode);
                                                }
                                            }
                                        }
                                        jsonResult.code = ApiCode.成功;
                                        jsonResult.msg = ls;
                                        jsonResult.data = da;
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.接口调用失败;
                                        jsonResult.msg = "失败";
                                    }

                                }
                                else
                                {
                                    jsonResult.code = ApiCode.程序异常;
                                    jsonResult.msg = "间隔时间未到";
                                }
                            }

                        }

                    }
                    catch
                    {
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "程序异常";
                    }
                    #endregion
                }
                else if (data.type == "2")//核销
                {
                    #region
                    try
                    {
                        using (var db = new MPDBEntities())
                        {
                            var config = db.TicketExpire_t.Where(x => x.hotelcode == data.hotelcode).FirstOrDefault();
                            if (config != null)
                            {
                                if (config.deadtime != "")
                                {
                                    data.firstts = config.deadtime;
                                }
                                else
                                {
                                    data.firstts = "1800";
                                }
                                if (config.spacetime != "")
                                {
                                    a = Convert.ToDouble(config.spacetime);
                                }
                                else
                                {
                                    a = 0;
                                }
                            }
                            else
                            {
                                data.firstts = "1800";
                                a = 0;
                            }
                        }
                        data.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                        data.userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        using (var dc = new iticketEntities())
                        {
                            //var con = dc.CAVRecord_t.Where(x => x.hotelcode == data.hotelcode && x.scene == data.scene && x.categoryid == data.CategoryId&&x.).OrderByDescending(x => x.userdate).FirstOrDefault();

                            //if (con == null)
                            //{
                            var ls = TicketApi.CAVTicketNow(data);

                            if (ls.IndexOf("success") > -1)
                            {
                                var da = new CAVRecord_t()
                                {
                                    channel = data.channel,
                                    time = data.time,
                                    addname = data.addname,
                                    categoryid = data.CategoryId,
                                    deptcode = data.deptcode,
                                    firstts = data.firstts,
                                    fmoney = data.fmoney,
                                    FormulaId = data.FormulaId,
                                    gdcode = data.gdcode,
                                    hotelcode = data.hotelcode,
                                    hotelcodenew = data.hotelcodenew,
                                    jycode = data.jycode,
                                    no = data.no,
                                    scene = data.scene,
                                    sflag = data.sflag,
                                    ticketsn = data.ticketsn,
                                    tmoney = data.tmoney,
                                    tp_id = data.Tp_Id,
                                    ts = data.ts,
                                    usercode = data.usercode,
                                    userdate = Convert.ToDateTime(data.userdate),
                                    usertype = data.usertype,
                                    xfcode = data.xfcode,
                                    xftype = data.xftype,
                                    addtime = DateTime.Now,
                                    mobile = data.mobile,
                                    categoryname = data.CategoryName,
                                    formulaname = data.FormulaName,
                                    username = hy_xx[0].name,
                                    bosscard = hy_xx[0].CardNo,
                                    realbosscard = hy_xx[0].realbosscard,
                                    rate = data.rate
                                };
                                dc.CAVRecord_t.Add(da);
                                dc.SaveChanges();
                                //        // var MPDB = new MPDBEntities();
                                //        // var hotelInfo = MPDB.MPConfigs.Where(x => x.ShopCode == data.hotelcode).FirstOrDefault();

                                //        //var openid = TicketApi.Getmember_bymobile("", "", hotelInfo.YQTHotelgrouptype, data.mobile);
                                //        //if(openid.Count>0)
                                //        //{
                                //        //    var res=WxHelper.SendTemplateMsg(data.hotelcode, openid[0].openid, data.CategoryName, "1");
                                //        //}
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = ls;
                                jsonResult.data = da;
                                if (member.Count > 0)
                                {
                                    CommonApi.SendConsume(member[0].openid, "消费点", data.CategoryName,"1", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "", data.hotelcode);    
                                }
                                RedisHelper.pushMsg(hy_xx[0], data);
                                //    }
                                //    else
                                //    {
                                //        jsonResult.code = ApiCode.接口调用失败;
                                //        jsonResult.msg = "失败";
                                //    }
                                //}
                                //else
                                //{
                                //    DateTime dt1 = Convert.ToDateTime(con.userdate);
                                //    DateTime dt2 = DateTime.Now;
                                //    TimeSpan ts = dt1.Subtract(dt2).Duration();
                                //    Double douLen = ts.TotalSeconds;
                                //    if (douLen > a)
                                //    {
                                //        var ls = TicketApi.CAVTicketNow(data);

                                //        if (ls.IndexOf("success") > -1)
                                //        {
                                //            var da = new CAVRecord_t()
                                //            {
                                //                channel = data.channel,
                                //                time = data.time,
                                //                addname = data.addname,
                                //                categoryid = data.CategoryId,
                                //                deptcode = data.deptcode,
                                //                firstts = data.firstts,
                                //                fmoney = data.fmoney,
                                //                FormulaId = data.FormulaId,
                                //                gdcode = data.gdcode,
                                //                hotelcode = data.hotelcode,
                                //                hotelcodenew = data.hotelcodenew,
                                //                jycode = data.jycode,
                                //                no = data.no,
                                //                scene = data.scene,
                                //                sflag = data.sflag,
                                //                ticketsn = data.ticketsn,
                                //                tmoney = data.tmoney,
                                //                tp_id = data.Tp_Id,
                                //                ts = data.ts,
                                //                usercode = data.usercode,
                                //                userdate = Convert.ToDateTime(data.userdate),
                                //                usertype = data.usertype,
                                //                xfcode = data.xfcode,
                                //                xftype = data.xftype,
                                //                addtime = DateTime.Now,
                                //                mobile = data.mobile,
                                //                categoryname = data.CategoryName,
                                //                formulaname = data.FormulaName,
                                //                username = hy_xx[0].name,
                                //                bosscard = hy_xx[0].CardNo,
                                //                realbosscard = hy_xx[0].realbosscard,
                                //            };
                                //            dc.CAVRecord_t.Add(da);
                                //            dc.SaveChanges();
                                //            jsonResult.code = ApiCode.成功;
                                //            jsonResult.msg = ls;
                                //            jsonResult.data = da;
                                //        }
                                //        else
                                //        {
                                //            jsonResult.code = ApiCode.接口调用失败;
                                //            jsonResult.msg = "失败";
                                //        }

                                //    }
                                //    else
                                //    {
                                //        jsonResult.code = ApiCode.程序异常;
                                //        jsonResult.msg = "间隔时间未到";
                                //    }
                                //}
                            }
                        }
                    }
                    catch
                    {
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "程序异常";
                    }
                    #endregion
                }
                else if (data.type == "3")
                {
                    #region
                    try
                    {

                        data.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                        data.userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        using (var dc = new iticketEntities())
                        {
                            var ls = TicketApi.CAVTicketNoFirstTS(data);

                            if (ls.IndexOf("success") > -1)
                            {

                                var da = new CAVRecord_t()
                                {
                                    channel = data.channel,
                                    time = data.time,
                                    addname = data.addname,
                                    categoryid = data.CategoryId,
                                    deptcode = data.deptcode,
                                    firstts = data.firstts,
                                    fmoney = data.fmoney,
                                    FormulaId = data.FormulaId,
                                    gdcode = data.gdcode,
                                    hotelcode = data.hotelcode,
                                    hotelcodenew = data.hotelcodenew,
                                    jycode = data.jycode,
                                    no = data.no,
                                    scene = data.scene,
                                    sflag = data.sflag,
                                    ticketsn = data.ticketsn,
                                    tmoney = data.tmoney,
                                    tp_id = data.Tp_Id,
                                    ts = data.ts,
                                    usercode = data.usercode,
                                    userdate = Convert.ToDateTime(data.userdate),
                                    usertype = data.usertype,
                                    xfcode = data.xfcode,
                                    xftype = data.xftype,
                                    addtime = DateTime.Now,
                                    mobile = data.mobile,
                                    categoryname = data.CategoryName,
                                    formulaname = data.FormulaName,
                                    username = hy_xx[0].name,
                                    bosscard = hy_xx[0].CardNo,
                                    realbosscard = hy_xx[0].realbosscard,
                                    rate = data.rate
                                };
                                dc.CAVRecord_t.Add(da);
                                dc.SaveChanges();
                                string M = Regex.Replace(data.mobile, "(\\d{3})\\d{4}(\\d{4})", "$1****$2");

                                var queryall = new QueryTicketAllModel();
                                queryall.user = "";
                                queryall.token = "";
                                queryall.hotelgroupid = "";
                                queryall.hotelcode = data.hotelcode;
                                queryall.mode = "m";
                                queryall.code = data.mobile;
                                var pricexx = TicketApi.Queryticketall_json(queryall);
                                if ((data.sflag == "1" || data.sflag == "2") && data.usertype == "0")
                                {
                                    if (pricexx != null)
                                    {
                                        var dx = s.SendSameSms(data.mobile, "KK", "" + M + "," + data.tmoney + ","+pricexx[0].tmoney+"", "", data.hotelcode);
                                    }
                                }
                                if (data.sflag == "8")
                                {
                                    if (member.Count > 0)
                                    {
                                        var db = new MPDBEntities();
                                        //CommonApi.SendXf(member[0].openid,"",pricexx[0].)
                                        var id = int.Parse(data.scene);
                                        var scene = db.HotelScene_t.FirstOrDefault(x => x.id == id);
                                        if (scene != null)
                                        {
                                            CommonApi.SendXf(member[0].openid, "消费点", scene.scenename, "会员卡号", hy_xx[0].realbosscard == "" ? hy_xx[0].CardNo : hy_xx[0].realbosscard, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), "", data.hotelcode);
                                        }
                                    }
                                }
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = ls;
                                jsonResult.data = da;
                            }
                            else
                            {
                                jsonResult.code = ApiCode.接口调用失败;
                                jsonResult.msg = "失败";
                            }
                        }

                    }
                    catch
                    {
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "程序异常";
                    }
                    #endregion
                }
                else if (data.type == "4")
                {
                    #region
                    try
                    {
                        using (var db = new MPDBEntities())
                        {
                            var config = db.TicketExpire_t.Where(x => x.hotelcode == data.hotelcode).FirstOrDefault();
                            if (config != null)
                            {
                                if (config.deadtime != "")
                                {
                                    data.firstts = config.deadtime;
                                }
                                else
                                {
                                    data.firstts = "1800";
                                }


                                if (config.spacetime != "")
                                {
                                    a = Convert.ToDouble(config.spacetime);
                                }
                                else
                                {
                                    a = 0;
                                }
                            }
                            else
                            {
                                data.firstts = "1800";
                                a = 0;
                            }
                        }
                        data.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                        data.userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        using (var dc = new iticketEntities())
                        {
                            var con = dc.CAVRecord_t.Where(x => x.hotelcode == data.hotelcode && x.scene == data.scene && x.categoryid == data.CategoryId && x.mobile == mobile).OrderByDescending(x => x.userdate).FirstOrDefault();

                            if (con == null)
                            {
                                var ls = TicketApi.CAVTicketChnnel(data);
                                if (ls.IndexOf("success") > -1)
                                {
                                    var da = new CAVRecord_t()
                                    {
                                        channel = data.channel,
                                        time = data.time,
                                        addname = data.addname,
                                        categoryid = data.CategoryId,
                                        deptcode = data.deptcode,
                                        firstts = data.firstts,
                                        fmoney = data.fmoney,
                                        FormulaId = data.FormulaId,
                                        gdcode = data.gdcode,
                                        hotelcode = data.hotelcode,
                                        hotelcodenew = data.hotelcodenew,
                                        jycode = data.jycode,
                                        no = data.no,
                                        scene = data.scene,
                                        sflag = data.sflag,
                                        ticketsn = data.ticketsn,
                                        tmoney = data.tmoney,
                                        tp_id = data.Tp_Id,
                                        ts = data.ts,
                                        usercode = data.usercode,
                                        userdate = Convert.ToDateTime(data.userdate),
                                        usertype = data.usertype,
                                        xfcode = data.xfcode,
                                        xftype = data.xftype,
                                        addtime = DateTime.Now,
                                        mobile = data.mobile,
                                        categoryname = data.CategoryName,
                                        formulaname = data.FormulaName,
                                        username = hy_xx[0].name,
                                        bosscard = hy_xx[0].CardNo,
                                        realbosscard = hy_xx[0].realbosscard,
                                        rate = data.rate
                                    };
                                    dc.CAVRecord_t.Add(da);
                                    dc.SaveChanges();
                                    jsonResult.code = ApiCode.成功;
                                    jsonResult.msg = ls;
                                    jsonResult.data = da;
                                }
                                else
                                {
                                    jsonResult.code = ApiCode.接口调用失败;
                                    jsonResult.msg = "失败";
                                }
                            }
                            else
                            {
                                DateTime dt1 = Convert.ToDateTime(con.userdate);
                                DateTime dt2 = DateTime.Now;
                                TimeSpan ts = dt1.Subtract(dt2).Duration();
                                Double douLen = ts.TotalSeconds;
                                if (douLen > a)
                                {
                                    var ls = TicketApi.CAVTicketChnnel(data);

                                    if (ls.IndexOf("false") == -1)
                                    {
                                        var da = new CAVRecord_t()
                                        {
                                            channel = data.channel,
                                            time = data.time,
                                            addname = data.addname,
                                            categoryid = data.CategoryId,
                                            deptcode = data.deptcode,
                                            firstts = data.firstts,
                                            fmoney = data.fmoney,
                                            FormulaId = data.FormulaId,
                                            gdcode = data.gdcode,
                                            hotelcode = data.hotelcode,
                                            hotelcodenew = data.hotelcodenew,
                                            jycode = data.jycode,
                                            no = data.no,
                                            scene = data.scene,
                                            sflag = data.sflag,
                                            ticketsn = data.ticketsn,
                                            tmoney = data.tmoney,
                                            tp_id = data.Tp_Id,
                                            ts = data.ts,
                                            usercode = data.usercode,
                                            userdate = Convert.ToDateTime(data.userdate),
                                            usertype = data.usertype,
                                            xfcode = data.xfcode,
                                            xftype = data.xftype,
                                            addtime = DateTime.Now,
                                            mobile = data.mobile,
                                            categoryname = data.CategoryName,
                                            formulaname = data.FormulaName,
                                            username = hy_xx[0].name,
                                            bosscard = hy_xx[0].CardNo,
                                            realbosscard = hy_xx[0].realbosscard,
                                            rate = data.rate
                                        };
                                        dc.CAVRecord_t.Add(da);
                                        dc.SaveChanges();
                                        jsonResult.code = ApiCode.成功;
                                        jsonResult.msg = ls;
                                        jsonResult.data = da;
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.接口调用失败;
                                        jsonResult.msg = "失败";
                                    }

                                }
                                else
                                {
                                    jsonResult.code = ApiCode.程序异常;
                                    jsonResult.msg = "间隔时间未到";
                                }
                            }
                        }
                    }
                    catch
                    {
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "程序异常";
                    }
                    #endregion
                }
                else if (data.type == "5")
                {
                    #region
                    try
                    {

                        data.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                        data.userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        using (var dc = new iticketEntities())
                        {
                            var ls = TicketApi.CAVTicket(data);

                            if (ls.IndexOf("success") > -1)
                            {

                                var da = new CAVRecord_t()
                                {
                                    channel = data.channel,
                                    time = data.time,
                                    addname = data.addname,
                                    categoryid = data.CategoryId,
                                    deptcode = data.deptcode,
                                    firstts = data.firstts,
                                    fmoney = data.fmoney,
                                    FormulaId = data.FormulaId,
                                    gdcode = data.gdcode,
                                    hotelcode = data.hotelcode,
                                    hotelcodenew = data.hotelcodenew,
                                    jycode = data.jycode,
                                    no = data.no,
                                    scene = data.scene,
                                    sflag = data.sflag,
                                    ticketsn = data.ticketsn,
                                    tmoney = data.tmoney,
                                    tp_id = data.Tp_Id,
                                    ts = data.ts,
                                    usercode = data.usercode,
                                    userdate = Convert.ToDateTime(data.userdate),
                                    usertype = data.usertype,
                                    xfcode = data.xfcode,
                                    xftype = data.xftype,
                                    addtime = DateTime.Now,
                                    mobile = data.mobile,
                                    categoryname = data.CategoryName,
                                    formulaname = data.FormulaName,
                                    username = hy_xx[0].name,
                                    bosscard = hy_xx[0].CardNo,
                                    realbosscard = hy_xx[0].realbosscard,
                                    rate = data.rate
                                };
                                dc.CAVRecord_t.Add(da);
                                dc.SaveChanges();
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = ls;
                                jsonResult.data = da;
                            }
                            else
                            {
                                jsonResult.code = ApiCode.接口调用失败;
                                jsonResult.msg = "失败";
                            }
                        }

                    }
                    catch
                    {
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "程序异常";
                    }
                    #endregion
                }
                else
                {
                    jsonResult.code = ApiCode.程序异常;
                    jsonResult.msg = "程序异常";
                }
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "未找到会员信息";
            }
            return this.MyJson(jsonResult, "yyyy-MM-dd HH:mm:ss");
        }

        [HttpPost]//批量核销
        public JsonResult batchConsumeTicket(batchConsumeTicketModel data)
        {
            if (ModelState.IsValid)
            {
                if (data.ticketsnList != null)
                {
                    var ticketMore = data.ticketsnList.Split(',');
                    var errorlist = new List<string>();
                    foreach (var item in ticketMore)
                    {
                        var query = new QueryTicketnewModel()
                        {
                            user = "",
                            token = "",
                            hotelGroupId = "",
                            hotelcode = data.hotelcode,
                            mode = "ticketsn",
                            code = item.ToString(),
                        };
                        var ticket = TicketApi.QueryTicketNew(query);
                        if (ticket.Count > 0)
                        {
                            var one = ticket[0];
                            var jycode = OrderHelper.GetRandom1(data.hotelcode);
                            var userdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            var ConsumeTicket = new CAVTicketModel()
                            {
                                ticketsn = item.ToString(),
                                CategoryId = one.CategoryId,
                                FormulaId = one.FormulaId,
                                Tp_Id = one.tp_id,
                                userdate = userdate,
                                hotelcode = data.hotelcode,
                                hotelcodenew = data.hotelcodenew,
                                gdcode = data.gdcode,
                                addname = data.addname,
                                fmoney = one.fmoney,
                                tmoney = one.tmoney,
                                xfcode = data.xfcode,
                                jycode = jycode,
                                ts = one.ts,
                                firstts = "",
                            };
                            var res = TicketApi.CAVTicketNow(ConsumeTicket);
                            if (res.IndexOf("success") > -1)
                            {
                                var dc = new iticketEntities();
                                var hy_xx = TicketApi.Querymember_json("", "M", one.mobile);
                                var da = new CAVRecord_t()
                                {
                                    // channel = data.channel,
                                    //time = data.time,
                                    addname = data.addname,
                                    categoryid = one.CategoryId,
                                    //deptcode = data.deptcode,
                                    firstts = "",
                                    fmoney = one.fmoney,
                                    FormulaId = one.FormulaId,
                                    gdcode = data.gdcode,
                                    hotelcode = data.hotelcode,
                                    hotelcodenew = data.hotelcodenew,
                                    jycode = jycode,
                                    //no = data.no,
                                    scene = data.scene,
                                    sflag = one.sflag,
                                    ticketsn = one.TicketSN,
                                    tmoney = one.tmoney,
                                    tp_id = one.tp_id,
                                    ts = one.ts,
                                    // usercode = data.usercode,
                                    userdate = Convert.ToDateTime(userdate),
                                    usertype = "0",
                                    xfcode = data.xfcode,
                                    //xftype = data.xftype,
                                    addtime = DateTime.Now,
                                    mobile = one.mobile,
                                    categoryname = one.CategoryName,
                                    formulaname = one.FormulaName,
                                    username = hy_xx[0].name,
                                    bosscard = hy_xx[0].CardNo,
                                    realbosscard = hy_xx[0].realbosscard,
                                    rate=data.rate,
                                };
                                dc.CAVRecord_t.Add(da);
                                dc.SaveChanges();
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = "成功";
                            }
                            else
                            {
                                errorlist.Add(item.ToString());
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = "成功";
                                jsonResult.data = errorlist;
                            }
                        }
                    }
                }
                else
                {
                    jsonResult.code = ApiCode.参数不全;
                    jsonResult.msg = "成功";
                }
            }
            else
            {
                var item = ModelState.Values.Where(x => x.Errors.Count > 0).Take(1).SingleOrDefault();
                jsonResult.msg = item.Errors.Where(b => !string.IsNullOrWhiteSpace(b.ErrorMessage)).Take(1).SingleOrDefault().ErrorMessage;
                jsonResult.code = ApiCode.参数不全;
            }
            return this.MyJson(jsonResult, "yyyy-MM-dd HH:mm:ss");
        }

        [HttpPost]//获取核销记录报表
        public JsonResult GetConsumeList(GetConsumeModel data)
        {
            if (ModelState.IsValid)
            {
                var db = new iticketEntities();
                var res = from a in db.CAVRecord_t where a.hotelcode==data.hotelcode select  a ;
                if (data.starttime != null && data.endtime != null)
                {
                    var a = DateTime.Parse(data.starttime);
                    var b = DateTime.Parse(data.endtime);
                    res = res.Where(x => x.addtime >=a && x.addtime <=b);
                }
                if (data.scene != null)
                {
                    res = res.Where(x => x.scene == data.scene);
                }
                if (data.sflag != null)
                {
                    res = res.Where(x => x.sflag == data.sflag);
                }
                if (res.Count() > 0)
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.data = res;

                }
                else
                {
                    jsonResult.code = ApiCode.没有实体数据;
                    jsonResult.msg = "没数据";
                }
            }
            return this.MyJson(jsonResult, "yyyy-MM-dd HH:mm:ss",NullValueHandling.Include);
        }
        #endregion
        // [HttpPost]
        #region 云券通后台
        [HttpPost]//定义产品
        public JsonResult CreateFormula()
        {
            try
            {
                var sr = new StreamReader(Request.InputStream);
                var stream = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<CreateFormulaData>(stream);
                jsonResult = TicketApi.CreateFormula(data);
            }
            catch (Exception ex)
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = ex.ToString();
           
            }

            return this.MyJson(jsonResult);
        }
        [HttpPost]//发行
        public JsonResult SetCRMTicketSn()
        {
            try
            {
                var sr = new StreamReader(Request.InputStream);
                var stream = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<SetCRMTicketSnModel>(stream);
                jsonResult = TicketApi.SetCRMTicketSn(data);
            }
            catch (Exception ex)
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = ex.ToString();
            }

            return this.MyJson(jsonResult);
        }
        #endregion
    }
}
