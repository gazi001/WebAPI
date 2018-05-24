using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.WebApi.Model;
using KS.Common.SDK.AdvancedAPIs;
using KS.Model.Member;
using KS.Ticket.SDK.AdvancedAPIs;
using Newtonsoft.Json;
using RM.Common.DotNetCode;
using RM.Common.DotNetModel;

namespace BLL.WebApi.Controllers
{
    /// <summary>
    /// 会员中间层方法
    /// </summary>
    public class MemberController : BaseController
    {
        private JsonReturn jsonResult = new JsonReturn();
        protected LogHelper Logger = new LogHelper("Member");
        [HttpPost]//添加每个会员可以绑定openid个数
        public JsonResult AddMemberBindNum(AddMemberBindNumModel data)
        {
            using(var db = new MemberEntities())
            {
                var obj = db.OpenidToMobile_t.FirstOrDefault(x => x.id == data.id);
                if (obj == null)
                {
                    db.OpenidToMobile_t.Add(new OpenidToMobile_t
                    {
                        realbosscard=data.realbosscard,
                        bosscard=data.bosscard,
                        mobile = data.mobile,
                        openidnum = data.openidnum,
                        grouptype = data.grouptype,
                        addtime = DateTime.Now,
                        hotelcode = data.hotelcode,
                    });
                    db.bindopenidlogs.Add(new bindopenidlog 
                    {
                        realbosscard=data.realbosscard,
                        addtime=DateTime.Now,
                        bindnum=data.openidnum.ToString(),
                        bosscard = data.bosscard,
                        isbind="true",
                        mobile=data.mobile,
                        name=data.name,
                        type="add",
                        usercode=data.usercode,
                        grouptype=data.grouptype,
                        hotelcode=data.hotelcode,
                    });
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "添加成功";
                }
                else
                {
                    db.bindopenidlogs.Add(new bindopenidlog
                    {
                        realbosscard=data.realbosscard,
                        addtime = DateTime.Now,
                        bindnum = data.openidnum.ToString(),
                        bosscard = data.bosscard,
                        isbind = "true",
                        mobile = data.mobile,
                        name = data.name,
                        type = "update",
                        usercode = data.usercode,
                        grouptype = data.grouptype,
                        hotelcode = data.hotelcode,
                    });
                    obj.openidnum = data.openidnum;
                    obj.realbosscard = data.realbosscard;
                    //obj.bosscard = data.bosscard;
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "修改成功";
                }
                db.SaveChanges();
            }
            return this.Json(jsonResult);
        }
        [HttpPost]//获取绑定操作记录
        public JsonResult GetMemberBindLog(AddMemberBindNumModel data)
        {
            using (var db = new MemberEntities())
            {
                var list = db.bindopenidlogs.Where(x => x.hotelcode == data.hotelcode && x.grouptype == data.grouptype&&x.mobile==data.mobile).ToList();
                if (list.Count > 0)
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = list;
                }
                else
                {
                    jsonResult.code = ApiCode.没有实体数据;
                    jsonResult.msg = "没有实体数据";
                }
            }
            return this.Json(jsonResult);
        }
        [HttpPost]//获取
        public JsonResult GetMemberBindNum(AddMemberBindNumModel data)
        {
            using (var db = new MemberEntities()) 
            {
                var list = db.OpenidToMobile_t.Where(x => x.mobile == data.mobile && x.hotelcode == data.hotelcode).ToList();
                if (list.Count > 0)
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.data = list;
                    jsonResult.msg = "成功";
                }
                else
                {
                    jsonResult.code = ApiCode.没有实体数据;
                   // jsonResult.data = list;
                    jsonResult.msg = "没有实体数据";
                }
            }
            return this.MyJson(jsonResult,"yyyy-MM-dd",NullValueHandling.Include);
        }
        public ActionResult Index()
        {
            var MPDB = new MPDBEntities();
            var r = MPDB.HotelScene_t.ToList();
            return View();
        }

        [HttpPost]//会员注册
        public JsonResult Register(RegisterModel data)
        {
            Logger.WriteLog(string.Concat(new string[]
				{
					"----------- 记录程序日志 Log-----------\r\n",
                    "地址：\r\n",
                     Request.Url.ToString(),
                     "\r\n",
                     "请求参数：\r\n",
                     JsonConvert.SerializeObject(data),
				}));
            if (ModelState.IsValid)
            {
                try
                {
                    var MPDB = new MPDBEntities();
                    var db = new MemberEntities();
                    var num = db.OpenidToMobile_t.Where(x => x.mobile == data.mobile && x.hotelcode == data.hotelcode).FirstOrDefault();
                    int openidnum = 1;
                    if (num == null)
                    {
                        var hotelopenid = MPDB.TicketExpire_t.FirstOrDefault(x => x.hotelcode == data.hotelcode);
                        if (hotelopenid != null)
                        {
                            openidnum = int.Parse(hotelopenid.WXbindingnum);
                        }
                    }
                    else
                    {
                        openidnum = num.openidnum.Value;
                    }
                    var hotelInfo = MPDB.MPConfigs.Where(x => x.ShopCode == data.hotelcode).FirstOrDefault();
                    data.grouptype = hotelInfo.YQTHotelgrouptype;
                    data.user = hotelInfo.YQTOperatorId;
                    data.token = hotelInfo.YQTOperatorId;
                    var obj = TicketApi.Getmember_bymobile(data.user, data.token, data.grouptype, data.mobile);
                    if (obj.Count >= openidnum)
                    {
                        jsonResult.code = ApiCode.绑定数量到达上限;
                        jsonResult.msg = "绑定数量到达上限";
                    }
                    else
                    {
                        if (obj.Count == 0 && data.tjcode != null)
                        {
                            if (data.hy_name == null)
                            {
                                data.hy_name = "保密";
                            }
                            var isRegister = CommonApi.Setmemberlist_newtjcode(data.hy_name, "", "", data.hotelcode, "VIP", "VIP", "VIP", data.mobile, "", "", "", "", "", "", "", "", "", "", "0", "", "", "", "", hotelInfo.YQTHotelgrouptype, data.tjcode, data.source, "",data.hotelcode);
                            if (isRegister)
                            {
                                db.CRM_DCB.Add(new CRM_DCB
                                {
                                    hy_name = data.hy_name,
                                    hy_kh = data.hy_kh,
                                    bosscard = data.bosscard,
                                    cardhotel = data.cardhotel,
                                    usertype = data.usertype,
                                    cardtype = data.cardtype,
                                    ratecode = data.ratecode,
                                    hy_sjhm = data.hy_sjhm,
                                    hy_zjlx = data.hy_zjlx,
                                    hy_zjhm = data.hy_zjhm,
                                    hy_sex = data.hy_sex,
                                    bosshk = data.bosshk,
                                    saleid = data.saleid,
                                    // arr=data.arr,
                                    hy_qq_msn = data.hy_qq_msn,
                                    weixinnumber = data.weixinnumber,
                                    nation = data.nation,
                                    country = data.country,
                                    state = data.state,
                                    remark = data.remark,
                                    birthday = data.birthday == null ? DateTime.Parse("1900-01-01") : DateTime.Parse(data.birthday),
                                    //hy_email= data.hy_email,
                                    //  id=data.id,
                                    //grouptype= data.grouptype,
                                    tjcode = data.tjcode,
                                    source = data.source,
                                    address = data.address

                                });
                                db.SaveChanges();
                            }
                        }
                        if (data.hy_name == null||data.hy_name=="")
                        {
                            data.hy_name = "保密";
                        }
                        var resbindopenid = CommonApi.CheckOpenidAndCardByHotelCode(data.openid, data.mobile, data.grouptype, data.hotelcode, data.hy_name, data.source);
                        if (resbindopenid.returncode == "success")
                        {
                            if (!db.MemberBindInfoes.Any(x => x.hotelcode == data.hotelcode && x.openid == data.openid && x.source == data.source))
                            {
                                db.MemberBindInfoes.Add(new MemberBindInfo
                                {
                                    hotelcode = data.hotelcode,
                                    hy_sjhm = data.mobile,
                                    openid = data.openid,
                                    source = data.source,
                                });
                                db.SaveChanges();
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = "成功";
                            }
                            if (hotelInfo != null)
                            {
                                bool YQTGiftId = true;
                                if (hotelInfo.YQTGiftId != null && hotelInfo.YQTGiftId != "")
                                {
                                    var result1 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTGiftId, data.hotelcode);
                                    if (result1.returncode == "false")
                                    {
                                        var isSend = TicketApi.SetNewHyJson(data.user,data.token, data.hotelcode, hotelInfo.YQTGiftId, data.mobile);
                                        if (isSend.returncode != "success")
                                        {
                                            Logger.WriteLog(string.Concat(new string[]{   "地址：\r\n",
                                                 Request.Url.ToString(),                     
                                                 "\r\n",                     
                                                 "请求参数：\r\n",
                                                 data.hotelcode+","+hotelInfo.YQTGiftId+","+data.mobile,
                                                 "返回参数：\r\n",
                                                 JsonConvert.SerializeObject(jsonResult),
                                            }));
                                            YQTGiftId = false;
                                        }
                                    }
                                }
                                bool YQTRechargeableId = true;
                                if (hotelInfo.YQTRechargeableId != null && hotelInfo.YQTRechargeableId != "")
                                {
                                    var result2 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTRechargeableId, data.hotelcode);
                                    if (result2.returncode == "false")
                                    {
                                        var isSend = TicketApi.SetNewHyJson(data.user, data.token, data.hotelcode, hotelInfo.YQTRechargeableId, data.mobile);
                                        if (isSend.returncode != "success")
                                        {
                                            Logger.WriteLog(string.Concat(new string[]{   "地址：\r\n",
                                                Request.Url.ToString(),
                                                "\r\n",
                                                "请求参数：\r\n",
                                                data.hotelcode+","+hotelInfo.YQTRechargeableId+","+data.mobile,
                                                "返回参数：\r\n",
                                                JsonConvert.SerializeObject(jsonResult),
                                            }));
                                            YQTRechargeableId = false;
                                        }
                                    }
                                }

                                bool YQTregisterId = true;
                                if (hotelInfo.YQTregisterId != null && hotelInfo.YQTregisterId != "")
                                {
                                    var result3 = TicketApi.GetTicketUserbyRegedit("", "", "mobile", data.mobile, hotelInfo.YQTregisterId, data.hotelcode);
                                    if (result3.returncode == "false")
                                    {
                                        var isSend = TicketApi.SetNewHyJson(data.user, data.token, data.hotelcode, hotelInfo.YQTregisterId, data.mobile);
                                        if (isSend.returncode != "success")
                                        {
                                            Logger.WriteLog(string.Concat(new string[]{   "地址：\r\n",
                                                Request.Url.ToString(),                    
                                                "\r\n",                     
                                                "请求参数：\r\n",
                                                data.hotelcode+","+hotelInfo.YQTregisterId+","+data.mobile,                     
                                                "返回参数：\r\n",
                                                JsonConvert.SerializeObject(jsonResult),
                                            }));
                                            YQTregisterId = false;
                                        }
                                    }
                                }
                                //if (YQTGiftId && YQTregisterId && YQTRechargeableId)
                                //{
                                //    jsonResult.code = ApiCode.成功;
                                //    jsonResult.msg = "成功";
                                //}
                                jsonResult.code = ApiCode.成功;
                                jsonResult.msg = "成功";
                            }
                        }
                        else
                        {
                            jsonResult.code = ApiCode.注册失败;
                            jsonResult.msg = "注册失败";
                        }
                    }
                }
                catch (Exception ex)
                {

                    Logger.WriteLog(string.Concat(new string[]
				{
					"----------- 记录程序日志 Log-----------\r\n",
                    "地址：\r\n",
                     Request.Url.ToString(),
                     "\r\n",
                     "请求参数：\r\n",
                     JsonConvert.SerializeObject(data),
                     "返回参数：\r\n",
                     JsonConvert.SerializeObject(jsonResult),
                     "异常：\r\n",
                     ex.ToString(),
				}));
                    jsonResult.code = ApiCode.程序异常;
                    jsonResult.msg = ex.ToString();
                    return this.MyJson(jsonResult);
                }
               
            }
            else
            {
                var item = ModelState.Values.Where(x => x.Errors.Count > 0).Take(1).SingleOrDefault();
                jsonResult.msg = item.Errors.Where(b => !string.IsNullOrWhiteSpace(b.ErrorMessage)).Take(1).SingleOrDefault().ErrorMessage;
                jsonResult.code = ApiCode.接口调用失败;
            }
            
            return this.MyJson(jsonResult);
        }
        [HttpPost]//添加场景下的工号
        public JsonResult AddUserScene(AddUserSceneModel data)
        {
             var db = new MemberEntities();
             var item = db.UserScenes.FirstOrDefault(x => x.usercode == data.usercode && x.hotelcode == data.hotelcode && x.hotelgroupcode == data.hotelgroupcode);
             if (item == null)
             {
                 db.UserScenes.Add(new UserScene
                 {
                     scenename=data.scenename,
                     name=data.name,
                     hotelcode = data.hotelcode,
                     hotelgroupcode = data.hotelgroupcode,
                     scene = data.scene,
                     usercode = data.usercode,
                     flag=1,
                     addtime=DateTime.Now
                 });
             }
             else
             {
                 item.scenename = data.scenename;
                 item.name = data.name;
                 item.flag = data.flag;
                 item.hotelcode = data.hotelcode;
                 item.hotelgroupcode = data.hotelgroupcode;
                 item.usercode = data.usercode;
                 item.scene = data.scene;
                 
             }
             db.SaveChanges();
             jsonResult.code = ApiCode.成功;
             jsonResult.msg = "成功";
             return this.MyJson(jsonResult);
 
        }
        [HttpPost]//获取场景下的工号
        public JsonResult GetUserScene(AddUserSceneModel data)
        {
            var db = new MemberEntities();
            var res = db.UserScenes.Where(x => x.scene==data.scene&& x.hotelcode == data.hotelcode && x.hotelgroupcode == data.hotelgroupcode);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "成功";
            jsonResult.data = res;
            return this.MyJson(jsonResult);
        }

        [HttpPost]//获取工号属于哪个场景
        public JsonResult GetSceneByUser(AddUserSceneModel data)
        {
            var db = new MemberEntities();
            var res = db.UserScenes.FirstOrDefault(x => x.usercode == data.usercode && x.hotelcode == data.hotelcode && x.hotelgroupcode == data.hotelgroupcode&&x.flag==1);
            if (res != null)
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
                jsonResult.data = res;
            }
            else
            {
                jsonResult.code = ApiCode.没有实体数据;
                jsonResult.msg = "没有实体数据";
                jsonResult.data = res;
 
            }
            return this.MyJson(jsonResult);
        }
    }
}
