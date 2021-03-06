﻿using KS.Model.ApiResultModel;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;
using Newtonsoft.Json;
using RM.Common.DotNetJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Common.SDK;
using KS.Common.SDK.AdvancedAPIs;
using KS.Model;
using KS.Model.Mall.MallResponse;
using KS.Model.Mall.MallRequest;
using KS.DataBase;
using RM.Common.DotNetModel;
using RM.Common.DotNetHttp;
using KS.Model.Common.CommonResponse;
using System.Globalization;


namespace KS.Ticket.SDK.AdvancedAPIs
{
    public  class TicketApi
    {
        private static net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
        private static net.kuaishun.ticketmk.process myprocess = new net.kuaishun.ticketmk.process();
        private static iticketDB iticketdb = DbContextFactory.Create(DBName.ITicketDB) as iticketDB;
        #region  前端中间层接口
        /// <summary>
        /// 商城发券
        /// </summary>
        /// <param name="orderlist"></param>

        public static bool SendTicket(List<QueryOrderModel> orderlist,string user,ref string shopname)
        {
            net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
            net.kuaishun.shopinterface.Service shop = new net.kuaishun.shopinterface.Service();
            bool IsSuccess = true;
            foreach (var item in orderlist[0].success)
            {
                var str = item.onsalecode.Substring(0, 6);
                if (str == "YLHCSD")
                {
                    int num = int.Parse(item.num);
                    var YQTOperatorId = "lyh001";
                    var hotelcode = "YLHCSD";
                    for (int i = 0; i < num; i++)
                    {
                        var result = service.Set_newhy_new_json(YQTOperatorId, YQTOperatorId, hotelcode, item.linkticketsn, item.mobile, hotelcode).Replace("(", "").Replace(")", "");
                        var returncode = JsonHelper.GetJsonValue(result, "returncode");
                        var tp_id = JsonHelper.GetJsonValue(result, "tp_id");
                        iticketdb.SendTicketLog_Mall.Add(new SendTicketLog_Mall
                        {
                            addtime = DateTime.Now,
                            hotelcode = hotelcode,
                            jgxz = item.jgxz,
                            mobile = item.mobile,
                            onsalecode = item.onsalecode,
                            ordercode = item.ordercode,
                            sysj = item.sysj,
                            ticketsn = item.linkticketsn,
                            returncode = returncode,
                            type = 0,
                            tp_id = tp_id
                        });
                        if (returncode != "success")
                        {
                          
                            IsSuccess = false;
                        }
                        else
                        {
                            //try
                            //{
                            //    var resend = service.Set_newhy_new_json(YQTOperatorId, YQTOperatorId, hotelcode, item.linkticketsn, item.mobile, hotelcode).Replace("(", "").Replace(")", "");
                            //    if (JsonHelper.GetJsonValue(result, "returncode") == "success")
                            //    {
                            //        iticketdb.SendTicketLog_Mall.Add(new SendTicketLog_Mall
                            //        {
                            //            addtime = DateTime.Now,
                            //            hotelcode = item.hotelcode,
                            //            jgxz = item.jgxz,
                            //            mobile = item.mobile,
                            //            onsalecode = item.onsalecode,
                            //            ordercode = item.ordercode,
                            //            sysj = item.sysj,
                            //            ticketsn = item.linkticketsn,
                            //        });
                            //    }
                            //}
                            //catch (Exception ex)
                            //{

                            //    CommonApi.SendWx("KSHZ", "", "", "商城发券接口调用失败，订单号" + item.ordercode + "产品：" + item.linkticketsn, "");

                            //}
                            //shop.Setproduct_onsale_single_salenum_json(user, user, item.onsalecode, item.num);
                        }
                    }
                }
                else
                {
                    var formulaidArr=item.linkticketsn.Split(',');
                    foreach (var item1 in formulaidArr)
                    {
                       
                        int num = int.Parse(item.num);
                        for (int i = 0; i < num; i++)
                        {
                            var result = service.Set_newhy_new_json(user, user, item.hotelcode, item1.ToString(), item.mobile, item.hotelcode).Replace("(", "").Replace(")", "");
                            var returncode = JsonHelper.GetJsonValue(result, "returncode");
                            var tp_id = JsonHelper.GetJsonValue(result, "tp_id");
                            iticketdb.SendTicketLog_Mall.Add(new SendTicketLog_Mall
                            {
                                addtime = DateTime.Now,
                                hotelcode = item.hotelcode,
                                jgxz = item.jgxz,
                                mobile = item.mobile,
                                onsalecode = item.onsalecode,
                                ordercode = item.ordercode,
                                sysj = item.sysj,
                                ticketsn = item1.ToString(),
                                returncode = returncode,
                                type=0,
                                tp_id = tp_id
                            });
                            if (JsonHelper.GetJsonValue(result, "returncode") != "success")
                            {
                                
                                IsSuccess = false;
                            }
                            else
                            {
                                //try
                                //{
                                //    var resend = service.Set_newhy_new_json(user, user, item.hotelcode, item1.ToString(), item.mobile, item.hotelcode).Replace("(", "").Replace(")", "");
                                //    if (JsonHelper.GetJsonValue(result, "returncode") == "success")
                                //    {
                                //        iticketdb.SendTicketLog_Mall.Add(new SendTicketLog_Mall
                                //        {
                                //            addtime = DateTime.Now,
                                //            hotelcode = item.hotelcode,
                                //            jgxz = item.jgxz,
                                //            mobile = item.mobile,
                                //            onsalecode = item.onsalecode,
                                //            ordercode = item.ordercode,
                                //            sysj = item.sysj,
                                //            ticketsn = item.linkticketsn,
                                //        });
                                //    }
                                //}
                                //catch (Exception ex)
                                //{

                                //    CommonApi.SendWx("KSHZ", "", "", "商城发券接口调用失败，订单号" + item.ordercode + "产品：" + item.linkticketsn, "");
                                  
                                //}
                              
                                // shop.Setproduct_onsale_single_salenum_json(user, user, item.onsalecode, item.num);
                            }
                        }
                    }
                }
                iticketdb.SaveChanges();
              //  shop.Setproduct_onsale_single_salenum_json(user, user, item.onsalecode, item.num);
                shopname = shopname + item.pname + ",";
            }
            return IsSuccess;
        }

    
        public static bool SendTicketTest(List<QueryOrderModel> orderlist, string user, ref string shopname,PaySuccessServiceModel model)
        {
            net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
            net.kuaishun.shopinterface.Service shop = new net.kuaishun.shopinterface.Service();
            bool IsSuccess = true;
            foreach (var item in orderlist[0].success)
            {
                var str = item.onsalecode.Substring(0, 6);
                if (str == "YLHCSD")
                {
                    int num = int.Parse(item.num);
                    var YQTOperatorId = "lyh001";
                    var hotelcode = "YLHCSD";
                    for (int i = 0; i < num; i++)
                    {
                        var result = service.Set_newhy_new_json(YQTOperatorId, YQTOperatorId, hotelcode, item.linkticketsn, item.mobile, model.salehotelcode).Replace("(", "").Replace(")", "");
                        if (JsonHelper.GetJsonValue(result, "returncode") != "success")
                        {
                            IsSuccess = false;
                        }
                        else
                        {
                            shop.Setproduct_onsale_single_salenum_json(user, user, item.onsalecode, item.num);
                        }
                    }
                }
                else
                {
                    int num = int.Parse(item.num);
                    for (int i = 0; i < num; i++)
                    {
                        var result = service.Set_newhy_new_json(user, user, item.hotelcode, item.linkticketsn, item.mobile,model.salehotelcode).Replace("(", "").Replace(")", "");
                        if (JsonHelper.GetJsonValue(result, "returncode") != "success")
                        {
                            IsSuccess = false;
                        }
                        else
                        {
                            shop.Setproduct_onsale_single_salenum_json(user, user, item.onsalecode, item.num);
                        }
                    }
                }
                shopname = shopname + item.pname + ",";
            }
            return IsSuccess;
        }
        /// <summary>
        /// 查询可用优惠券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<QueryTicketNewResultSuccessItem> QueryTicketNew(QueryTicketnewModel data)
        {
            var result = service.Queryticketnew_json(data.user, data.token, data.hotelGroupId, data.hotelcode, data.mode, data.code).Replace("(","").Replace(")","");
            var obj = JsonConvert.DeserializeObject<List<QueryTicketNewResultSuccessItem>>(result);
            return obj;
        }

        /// <summary>
        /// 产品限购查询，判断是否购买上限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetFormulaXgResult Get_formula_xg_json(GetFormulaXgModel data)
        {
            var result = service.Get_formula_xg_json(data.user, data.token,data.hotelcode,data.mobile,data.formulaid,data.num);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetFormulaXgResult>>(result)[0];
            return obj;
        }
        /// <summary>
        /// 发产品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetNewHyResult SetNewHyJson(SetNewHyJsonModel data)
        {
            var result = service.Set_newhy_new_json(data.user, data.token, data.hotelcode, data.FormulaId, data.mobile, data.salehotelcode).Replace("(", "").Replace(")", "");  
            var obj = JsonConvert.DeserializeObject<List<SetNewHyResult>>(result)[0];
            return obj;
        }
        /// <summary>
        /// 注册发包
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="FormulaId"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static SetNewHyResult SetNewHyJson(string user, string token, string hotelcode, string FormulaId, string mobile)
        {
            var result = service.Set_newhy_json(user, token, hotelcode, FormulaId, mobile).Replace("(", "").Replace(")", "");
            var obj = JsonConvert.DeserializeObject<List<SetNewHyResult>>(result)[0];
            return obj;
        }
        public static GetFormulaInfoByFormulaidResult GetFormula_info_byformulaid_json(GetFormulaInfoByFormulaidModel data)
        {
            var result = service.GetFormula_info_byformulaid_json(data.user, data.token, data.hotelcode, data.formulaid);
            var obj = JsonConvert.DeserializeObject < List<GetFormulaInfoByFormulaidResult>>(result)[0];
            return obj;
        }

        public static GetbosscardbyhotelcodeandsourceResult Getbosscardbyhotelcodeandsource(GetbosscardbyhotelcodeandsourceModel data)
        {
            net.kuaishun.interfacecrs.Service service = new net.kuaishun.interfacecrs.Service();
            var result = service.Getbosscardbyhotelcodeandsource(data.hotelcode,data.where,data.source,data.grouptype).Replace("(","").Replace(")","");
            //var result = service.GetFormula_info_byformulaid_json(data.user, data.token, data.hotelcode, data.formulaid);
            var obj = JsonConvert.DeserializeObject<List<GetbosscardbyhotelcodeandsourceResult>>(result)[0];
            return obj;
        }
        public static MemberBalanceResult GetMemberBalance(MemberBalanceModel data)
        {
            var result = service.Queryticketall_json("", "", data.hotelGroupId, data.hotelcode, data.mode, data.code).Replace("(", "").Replace(")", "");
            if (result != "]")
            {
                var obj = JsonConvert.DeserializeObject<List<MemberBalanceResult>>(result)[0];
                return obj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 发产品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetproductXgResult GetproductXg(GetproductXgModel data)
        {

            var result = service.Getproduct_xg_json(data.user, data.token,data.formulaid, data.hotelcode);
            var obj = JsonConvert.DeserializeObject<List<GetproductXgResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 获取单人票券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static  List<QueryticketNewNowxBDLYResult> Queryticketnew_nowx_bdly_json(QueryticketNewNowxBDLYModel data)
        {

            var result = service.Queryticketnew_nowx_bdly_json(data.user, data.token,data.hotelGroupId, data.hotelcode,data.mode,data.code);
            result = CommonFunction.Replacebracket(result);
            if (result == "]")
            {
                return null;
            }
            else
            {

                var obj = JsonConvert.DeserializeObject<List<QueryticketNewNowxBDLYResult>>(result);
                return obj;
            }
        }

        /// <summary>
        /// （注册）查询馈赠包等
        /// </summary>
        /// <param name="user"></param>
        /// <param name="token"></param>
        /// <param name="mode"></param>
        /// <param name="code"></param>
        /// <param name="formulaid"></param>
        /// <param name="hotelcode"></param>
        /// <returns></returns>
        public static SetNewHyResult GetTicketUserbyRegedit(string user, string token, string mode, string code, string formulaid, string hotelcode)
        {
            var result = service.Getticket_user_byregedit(user, token, mode, code, formulaid, hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetNewHyResult>>(result)[0];
            if(obj!=null)
                return obj;
            return null;           
        }

        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TicketJLAddNotimeSetResult DebitCard(TicketJLAddNotimeSetModel data)
        {
                Random ran = new Random();
                myprocess.no=data.no;
                myprocess.usercode=data.usercode;
                myprocess.pwd=data.pwd;
                myprocess.ticketsn=data.ticketsn;
                myprocess.CategoryId=data.CategoryId;
                myprocess.FormulaId=data.FormulaId;
                myprocess.Tp_Id=data.Tp_Id;
                myprocess.userdate=DateTime.Now.ToString("yyyy-MM-dd");//1999-08-08
                myprocess.usertype=data.usertype;
                myprocess.hotelcode=data.hotelcode;//消费酒店
                myprocess.deptcode=data.deptcode;
                myprocess.gdcode="DC";//DC
                myprocess.addname=data.addname;//oid
                myprocess.fmoney=data.fmoney;//扣款金额
                myprocess.tmoney=data.tmoney;
                myprocess.xfcode=data.xfcode;//ordercode
                myprocess.jycode = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ran.Next(1000, 9999);//唯一
                myprocess.xftype=data.xftype;
                myprocess.hotelcodenew=data.hotelcodenew;//消费酒店
                var result = service.Ticket_jl_add_notime_Set(myprocess);
                result = CommonFunction.Replacebracket(result);
                var obj = JsonConvert.DeserializeObject<List<TicketJLAddNotimeSetResult>>(result)[0];
                return obj;
        }

        /// <summary>
        /// 获取单人信息，余额等
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<QueryTicketAllResult> Queryticketall_json(QueryTicketAllModel data)
        {

            var result = service.Queryticketall_json(data.user, data.token, data.hotelgroupid, data.hotelcode, data.mode, data.code);
            result = CommonFunction.Replacebracket(result);
            if (result != "]")
            {
                var obj = JsonConvert.DeserializeObject<List<QueryTicketAllResult>>(result);
                return obj;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 查询荣誉是否适用活动
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<GetticketUserByRegeditResult> Getticket_user_byregedit(GetticketUserByRegeditModel data)
        {

            var result = service.Getticket_user_byregedit(data.user, data.token,data.mode,data.code,data.formulaid,data.hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetticketUserByRegeditResult>>(result);
            return obj;
        }

        /// <summary>
        /// 扣款接口（无时间戳验证）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CAVTicket(CAVTicketModel data)
        {
            myprocess.no = "";
            myprocess.usercode = "test";
            myprocess.pwd = "test";
            myprocess.ticketsn = "";
            myprocess.CategoryId = data.CategoryId;
            myprocess.FormulaId = data.FormulaId;
            myprocess.Tp_Id = data.Tp_Id;
            myprocess.userdate = data.userdate;//1999-08-08
            myprocess.usertype = data.usertype;
            myprocess.hotelcode = data.hotelcode;//消费酒店
            myprocess.deptcode = "";
            myprocess.gdcode = data.gdcode;//DC
            myprocess.addname = data.addname;//oid
            myprocess.fmoney = data.fmoney;//扣款金额
            myprocess.tmoney = data.tmoney;
            myprocess.xfcode = data.xfcode;//ordercode
            myprocess.jycode = data.jycode;
            myprocess.xftype = data.xftype;
            myprocess.hotelcodenew = data.hotelcodenew;//消费酒店
            myprocess.ts = data.ts;
            myprocess.channel = data.channel;
            var result = service.Ticket_jl_add_notime_channel_Set(myprocess);
            result = CommonFunction.Replacebracket(result);
            //var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return result;
        }

        /// <summary>
        /// 扣款接口（有时间戳验证，firstts，传入秒数）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CAVTicketFirstTS(CAVTicketModel data)
        {
            
            myprocess.no = "";
            myprocess.usercode = "test";
            myprocess.pwd = "test";
            myprocess.ticketsn = "";
            myprocess.CategoryId = data.CategoryId;
            myprocess.FormulaId = data.FormulaId;
            myprocess.Tp_Id = data.Tp_Id;
            myprocess.userdate = data.userdate;//1999-08-08
            myprocess.usertype = data.usertype;
            myprocess.hotelcode = data.hotelcode;//消费酒店
            myprocess.deptcode = "";
            myprocess.gdcode = data.gdcode;//DC
            myprocess.addname = data.addname;//oid
            myprocess.fmoney = data.fmoney;//扣款金额
            myprocess.tmoney = data.tmoney;
            myprocess.xfcode = data.xfcode;//ordercode
            myprocess.jycode = data.jycode;
            myprocess.xftype = data.xftype;
            myprocess.hotelcodenew = data.hotelcodenew;//消费酒店
            myprocess.ts = data.ts;
            myprocess.firstts = data.firstts;
            var result = service.Ticket_jl_add_new_Set(myprocess);
            result = CommonFunction.Replacebracket(result);
            //var obj = JsonConvert.SerializeObject(myprocess);
            //var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return result;
        }

        /// <summary>
        /// 扣款接口（无时间戳验证）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CAVTicketNoFirstTS(CAVTicketModel data)
        {

            myprocess.no = "";
            myprocess.usercode = "test";
            myprocess.pwd = "test";
            myprocess.ticketsn = "";
            myprocess.CategoryId = data.CategoryId;
            myprocess.FormulaId = data.FormulaId;
            myprocess.Tp_Id = data.Tp_Id;
            myprocess.userdate = data.userdate;//1999-08-08
            myprocess.usertype = data.usertype;
            myprocess.hotelcode = data.hotelcode;//消费酒店
            myprocess.deptcode = "";
            myprocess.gdcode = data.gdcode;//DC
            myprocess.addname = data.addname;//oid
            myprocess.fmoney = data.fmoney;//扣款金额
            myprocess.tmoney = data.tmoney;
            myprocess.xfcode = data.xfcode;//ordercode
            myprocess.jycode = data.jycode;
            myprocess.xftype = data.xftype;
            myprocess.hotelcodenew = data.hotelcodenew;//消费酒店
            myprocess.ts = data.ts;
            var result = service.Ticket_jl_add_notime_Set(myprocess);
            result = CommonFunction.Replacebracket(result);
            //var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return result;
        }
        /// <summary>
        /// 扣款接口（channel）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CAVTicketChnnel(CAVTicketModel data)
        {
            myprocess.no = "";
            myprocess.usercode = "test";
            myprocess.pwd = "test";
            myprocess.ticketsn = "";
            myprocess.CategoryId = data.CategoryId;
            myprocess.FormulaId = data.FormulaId;
            myprocess.Tp_Id = data.Tp_Id;
            myprocess.userdate = data.userdate;//1999-08-08
            myprocess.usertype = data.usertype;
            myprocess.hotelcode = data.hotelcode;//消费酒店
            myprocess.deptcode = "";
            myprocess.gdcode = data.gdcode;//DC
            myprocess.addname = data.addname;//oid
            myprocess.fmoney = data.fmoney;//扣款金额
            myprocess.tmoney = data.tmoney;
            myprocess.xfcode = data.xfcode;//ordercode
            myprocess.jycode = data.jycode;
            myprocess.xftype = data.xftype;
            myprocess.hotelcodenew = data.hotelcodenew;//消费酒店
            myprocess.ts = data.ts;
            myprocess.channel = data.channel;
            myprocess.firstts = data.firstts;
            var result = service.Ticket_jl_add_channel_Set(myprocess);
            result = CommonFunction.Replacebracket(result);
            //var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return result;
        }

        /// <summary>
        /// 核销接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CAVTicketNow(CAVTicketModel data)
        {
            myprocess.no = "";
            myprocess.usercode = "green";
            myprocess.pwd = "gxhysn";
            myprocess.ticketsn = data.ticketsn;
            myprocess.CategoryId = data.CategoryId;
            myprocess.FormulaId = data.FormulaId;
            myprocess.Tp_Id = data.Tp_Id;
            myprocess.userdate = data.userdate;//1999-08-08
            myprocess.usertype = "0";
            myprocess.hotelcode = data.hotelcode;//消费酒店
            myprocess.deptcode = "";
            myprocess.gdcode = data.gdcode;//DC
            myprocess.addname = data.addname;//oid
            myprocess.fmoney = data.fmoney;//扣款金额
            myprocess.tmoney = data.tmoney;
            myprocess.xfcode = data.xfcode;//ordercode
            myprocess.jycode = data.jycode;
            myprocess.hotelcodenew = data.hotelcodenew;//消费酒店
            myprocess.ts = data.ts;
            myprocess.firstts = data.firstts;
            var result = service.Ticket_jl_add_ticketsnnew_Set(myprocess);
            result = CommonFunction.Replacebracket(result);
            //var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return result;
        }

        /// <summary>
        /// 核销接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TicketJLAddTicketSNNewSetResult Setticket_yhquse_json(TicketJLAddTicketSNNewSetModel data)
        {

            var result = service.Setticket_yhquse_json(data.user, data.token, data.ticketsn, data.resno, data.oid, data.tmoney, data.hotelcode, data.gdcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<TicketJLAddTicketSNNewSetResult>(result);
            return obj;
        }

        /// <summary>
        /// 获取报表会员绑定数统计，年月日
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<GetticketJLHXResult> Getticket_jl_hx_json(GetticketJLHXModel data)
        {

            var result = service.Getticket_jl_hx_json(data.user, data.token, data.starttime, data.endtime, data.hotelcode);
            if (result != "[]")
            {
                var obj = JsonConvert.DeserializeObject<List<GetticketJLHXResult>>(result);
                return obj;
            }
            else
            {
                return null;
            }
        }
        public static List<Getticket_jl_hx_jsonResult> WeekScreen(string BeginTime, string EndTime, string HotelCode)
        {
            net.kuaishun.ticketmk.Service service = new net.kuaishun.ticketmk.Service();
            var str = service.Getticket_jl_hx_json("", "", BeginTime, EndTime, HotelCode);
            var data = JsonConvert.DeserializeObject<List<GetticketJLHXResult>>(str);
            var StartTime = Convert.ToDateTime(BeginTime);
            var OverTime = Convert.ToDateTime(EndTime);
            var list = new List<Getticket_jl_hx_jsonResult>();
            while (StartTime <= OverTime)
            {
                var r = new Getticket_jl_hx_jsonResult();
                //开始时间是周几
                var BeginWeek = (int)StartTime.DayOfWeek;
                //最后时间是周几
                var EndWeek = (int)OverTime.DayOfWeek;
                if (BeginWeek == 1)
                {
                    if (StartTime.AddDays(6) > OverTime)
                    {
                        r.name = StartTime + "---" + OverTime;
                        r.count = data.Where(x => Convert.ToDateTime(x.userdate) >= StartTime && Convert.ToDateTime(x.userdate) <= OverTime).ToList().Count.ToString();
                        list.Add(r);
                        break;
                    }
                    else
                    {
                        r.name = StartTime + "---" + StartTime.AddDays(6);
                        r.count = data.Where(x => Convert.ToDateTime(x.userdate) >= StartTime && Convert.ToDateTime(x.userdate) <= StartTime.AddDays(6)).ToList().Count.ToString();
                        StartTime = StartTime.AddDays(7);
                        list.Add(r);

                    }
                }
                else
                {
                    var day = 7 - BeginWeek;
                    if (StartTime.AddDays(day) > OverTime)
                    {
                        r.name = StartTime + "---" + OverTime;
                        r.count = data.Where(x => Convert.ToDateTime(x.userdate) >= StartTime && Convert.ToDateTime(x.userdate) <= OverTime).ToList().Count.ToString();
                        list.Add(r);
                        break;
                    }
                    else
                    {
                        r.name = StartTime + "---" + StartTime.AddDays(day);
                        r.count = data.Where(x => Convert.ToDateTime(x.userdate) >= StartTime && Convert.ToDateTime(x.userdate) <= StartTime.AddDays(6)).ToList().Count.ToString();
                        StartTime = StartTime.AddDays(day + 1);
                        list.Add(r);
                    }
                }
            }
            return list;
        }

        //验证手机号有无openid
        public static List<Getmember_bymobileResult> Getmember_bymobile(string user, string token, string grouptype, string mobile)
        {
            var result = service.Getmember_bymobile_json(user, token, grouptype, mobile);
            var obj = JsonConvert.DeserializeObject<List<Getmember_bymobileResult>>(result);
            return obj;
        }

        //会员信息
        public static List<QuerymemberJsonResult> Querymember_json(string hotelgroupid, string mode, string code)
        {

            var result = service.Querymember_json(hotelgroupid,mode,code);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<QuerymemberJsonResult>>(result);
            return obj;
        }

        public static string batchConsumeTicket(string ticketsn)
        {

            return "";


        }
        #endregion
      
        #region 后台云券通中间层接口

       // private 
        #region 产品定义
        //根据前端的传值判断sflag
        public static string GetIsBool(string sflag, string isbool)
        {
            var Isbool = "";
            switch (sflag)
            {
                case "1":
                    Isbool = isbool;
                    break;
                case "2":
                    Isbool = isbool;
                    break;
                default:
                    Isbool = "0";
                    break;
            }
            return Isbool;
        }
        public static string GetSflag(string type, string cate_xz, string cate_type = "")
        {
            string sflag = "";
            switch (type)
            {
                case "putong":
                    if (cate_xz == "0")
                    {
                        sflag = "3";
                    }
                    else
                    {
                        sflag = cate_type == "0" ? "1" : "2";
                    }
                    break;
                case "youhui":
                    sflag = "8";
                    break;
                case "chongzhi":
                    sflag = cate_type == "0" ? "4" : "5";
                    break;
                case "rongyu":
                    sflag = "10";
                    break;
                case "jifen":
                    sflag = "11";
                    break;
                default:
                    break;
                  
            }
            return sflag;
        }

        public static string GetIscz(string sflag)
        {
            var Iscz = "";
            switch (sflag)
            {
                case "1":
                    Iscz = "1";
                    break;
                case "2":
                    Iscz = "1";
                    break;
                case "3":
                    Iscz = "1";
                    break;
                case "4":
                    Iscz = "0";
                    break;
                case "5":
                    Iscz = "0";
                    break;
              
                case "8":
                    Iscz = "1";
                    break;
                case "11":
                    Iscz = "1";
                    break;
                case "10":
                    Iscz = "1";
                    break;
                default:
                    Iscz = "0";
                    break;
            }
            return Iscz;
        }
        public static string GetProNum(string type, string num,string cate_type = "")
        {
            switch (type)
            {
                case "putong":
                    if (num == "")
                    {
                        num = "999999";
                    }
                   
                    break;
                case "youhui":
                    if (num == "")
                    {
                        num = "999999";
                    }
                    break;
                case "chongzhi":
                    if (num == "")
                    {
                        num = "999999";
                    }
                    break;
                case "rongyu":
                    num = "-999999";
                    break;
                case "jifen":
                    if (num == "")
                    {
                        num = "999999";
                    }
                    break;
                default:
                    break;

            }
            return num;
        }
        public static JsonReturn CreateFormula(CreateFormulaData data)
        {
            JsonReturn jsonResult = new JsonReturn();
            if (data != null)
            {
                try
                {
                    //var sflg = GetSflag(data.type);
                    if (data.arr != null && data.arr.Count > 0)
                    {
                        var pro_num = GetProNum(data.type, data.pro_num);

                        //调用webservice接口定义产品包，返回产品包id
                        //TODO:   Formulacode, fmoney,fnum, 字段没有
                        var formulaid = service.SetFormula_json("", "", data.pro_name, data.summary, "00", data.price, pro_num, data.hotelcode);
                        if (formulaid != "")
                        {
                            var formulaint = int.Parse(formulaid);
                            //本地关系型数据库存储产品信息
                            var formula = new Formula_t
                            {
                                fmoney = decimal.Parse(data.price),
                                Formulacode = "",
                                FormulaId = int.Parse(formulaid),
                                FormulaName = data.pro_name,
                                FormulaSummary = data.summary,
                                hotelcode = data.hotelcode,
                                //maxnum=int.Parse( data.pro_num),
                                flag = 0,
                            };
                            iticketdb.Formula_t.Add(formula);
                            iticketdb.SaveChanges();

                            //TODO:redis缓存记录产品信息
                            var setformulafw = service.SetFormula_fw_pl_json("", "", formulaid, data.pro_arr, data.hotelcode);
                            var rangArr = data.pro_arr.Split(',');
                            foreach (var item1 in rangArr)
                            {
                                if (!iticketdb.Formula_fw_t.Any(x => x.hotelcode == data.hotelcode && x.FormulaId == formulaint))
                                {
                                    iticketdb.Formula_fw_t.Add(new Formula_fw_t
                                    {
                                        FormulaId = formulaint,
                                        hotelcode = data.hotelcode,
                                        usehotelcode = item1.ToString(),
                                    });
                                }
                            }
                            iticketdb.SaveChanges();
                            //TODO:打包产品
                            var dbstr = service.Setdbname_json("", "", data.pro_name, formulaid, data.hotelcode).Replace("(", "").Replace(")", "");
                            if (dbstr != null)
                            {
                                var dbobj = JsonConvert.DeserializeObject<List<SetdbnameResult>>(dbstr)[0];
                                if (dbobj.returncode == "success")
                                {
                                    iticketdb.dbname_t.Add(new dbname_t
                                    {
                                        dbname = data.pro_name,
                                        formulaid = formulaid,
                                        hotelcode = data.hotelcode,
                                    });
                                    iticketdb.SaveChanges();
                                    if (CreateCategory(data.arr, data, dbobj))
                                    {

                                        jsonResult.code = ApiCode.成功;
                                        jsonResult.msg = "成功";
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.子券定义失败;
                                        jsonResult.msg = "子券定义失败";
                                    }

                                }

                            }

                        }
                        else
                        {
                            jsonResult.code = ApiCode.产品定义失败;
                            jsonResult.msg = "产品定义失败";

                        }
                    }
                    else
                    {
                        jsonResult.code = ApiCode.参数不全;
                        jsonResult.msg ="接口调用失败";
                    }
                }
                catch (Exception ex)
                {

                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = ex.ToString();
                }
            }
            return jsonResult;

        }

        public static bool CreateCategory(List<CreateCategoryData> CreateCategoryData, CreateFormulaData CreateFormulaData, SetdbnameResult dbname)
        {
            var isSet = true;
            try
            {
                for (int i = 0; i < CreateCategoryData.Count; i++)
                {
                    if (CreateCategoryData[i].cate_name != null)
                    {
                        var sflg = GetSflag(CreateFormulaData.type, CreateCategoryData[i].cate_xz, CreateCategoryData[i].cate_type);
                        var iscz = GetIscz(sflg);
                        if (sflg == "4" || sflg == "5")
                        {
                            var isbool = GetIsBool(sflg, CreateCategoryData[i].isbool);
                            var Categoryid = service.Setcategory_cz_json
    ("", "", CreateCategoryData[i].cate_name, CreateCategoryData[i].cate_code, CreateCategoryData[i].cate_summary, CreateFormulaData.hotelcode, "", "00", "00","0", CreateCategoryData[i].cate_type, sflg, CreateCategoryData[i].cate_start_date.ToString(), (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", CreateCategoryData[i].cate_bm, CreateCategoryData[i].cate_price, "0", CreateCategoryData[i].iszs, iscz, "");
                            if (Categoryid != "")
                            {
                                SaveCategory(CreateCategoryData, CreateFormulaData, dbname, i, sflg, isbool, Categoryid, iscz);
                            }
                            else
                            {
                                isSet = false;
                            }
                        }
                        else
                        {
                            var isbool = GetIsBool(sflg, CreateCategoryData[i].isbool);
                            var Categoryid = service.Setcategory_json("", "", CreateCategoryData[i].cate_name, CreateCategoryData[i].cate_code, CreateCategoryData[i].cate_summary, CreateFormulaData.hotelcode, "", "00", "00", "0", CreateCategoryData[i].cate_type, sflg, CreateCategoryData[i].cate_start_date.ToString(), (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", CreateCategoryData[i].cate_bm, CreateCategoryData[i].cate_price == "" ? "9999" : CreateCategoryData[i].cate_price, "0", CreateCategoryData[i].iszs, iscz);
                            if (Categoryid != "")
                            {
                                SaveCategory(CreateCategoryData, CreateFormulaData, dbname, i, sflg, isbool, Categoryid, iscz);
                            }
                            else
                            {
                                isSet = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isSet = false;

            }
            return isSet;

        }
        private static void SaveCatogory(List<CreateCategoryData> CreateCategoryData, string hotelcode, string num, string usercode, string pro_num, string dbid, int i, string sflg, string isbool, string Categoryid, string iscz)
        {
            var dbnameid = service.Setdbdata_json("", "", Categoryid, num, dbid, hotelcode);
            if (dbnameid.IndexOf("success") > -1)
            {
                var cateint = int.Parse(Categoryid);
                iticketdb.dbdata_t.Add(new dbdata_t
                {
                    CategoryId = cateint,
                    dbnameid = int.Parse(dbid),
                    hotelcode = hotelcode,
                    num = int.Parse(num),
                    oid = usercode,
                });
                iticketdb.Category_t.Add(new Category_t
                {
                    addtime = DateTime.Now,
                    BeginString = "00",
                    bgpic = "",
                    Categorycode = CreateCategoryData[i].cate_code,
                    CategoryId = cateint,
                    CategoryName = CreateCategoryData[i].cate_name,
                    CategoryNamebm = CreateCategoryData[i].cate_bm,
                    dzpq = "1",
                    endnum = int.Parse("00"),
                    EndString = "00",
                    ExpireDate = CreateCategoryData[i].cate_start_date.ToString(),
                    ExpireDateend = (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(),
                    flag = 0,
                    fmoney = CreateCategoryData[i].cate_price == null ? decimal.Parse("0") : decimal.Parse(CreateCategoryData[i].cate_price),
                    fnum = 0,
                    hotelcode = hotelcode,
                    HotelId = hotelcode,
                    isbool = int.Parse(isbool),//未确定
                    iscz = int.Parse(iscz),
                    istest = 0,
                    iswxly = 0,
                    iszs = CreateCategoryData[i].iszs == null ? 1 : int.Parse(CreateCategoryData[i].iszs),
                    maxNum = int.Parse(pro_num == "" ? "999999" : pro_num),
                    moneytype = 0,
                    pic = "",
                    rate = 1,
                    sflag = int.Parse(sflg),
                    Summary = CreateCategoryData[i].cate_summary,
                    type = 0,
                });
                var setcatefw = service.Setcategory_fw_pl_json("", "", Categoryid, CreateCategoryData[i].range, hotelcode);
                var rangArr = CreateCategoryData[i].range.Split(',');
                foreach (var item1 in rangArr)
                {
                    if (item1.ToString() != "")
                    {
                        if (!iticketdb.Category_fw_t.Any(x => x.hotelcode == hotelcode && x.CategoryId == cateint))
                        {
                            iticketdb.Category_fw_t.Add(new Category_fw_t
                            {
                                CategoryId = cateint,
                                hotelcode = hotelcode,
                                usehotelcode = item1.ToString(),
                                addtime = DateTime.Now,
                            });
                        }
                    }
                }
                iticketdb.SaveChanges();
            }
        }
        private static void SaveCategory(List<CreateCategoryData> CreateCategoryData, CreateFormulaData CreateFormulaData, SetdbnameResult dbname, int i, string sflg, string isbool, string Categoryid, string iscz)
        {
            var dbnameid = service.Setdbdata_json("", "", Categoryid, CreateFormulaData.data[i], dbname.id, CreateFormulaData.hotelcode);
            if (dbnameid.IndexOf("success") > -1)
            {
                var cateint = int.Parse(Categoryid);
                iticketdb.dbdata_t.Add(new dbdata_t
                {
                    CategoryId = cateint,
                    dbnameid = int.Parse(dbname.id),
                    hotelcode = CreateFormulaData.hotelcode,
                    num = int.Parse(CreateFormulaData.data[i]),
                    oid = CreateFormulaData.usercode,
                });
                iticketdb.Category_t.Add(new Category_t
                {
                    addtime = DateTime.Now,
                    BeginString = "00",
                    bgpic = "",
                    Categorycode = CreateCategoryData[i].cate_code,
                    CategoryId = cateint,
                    CategoryName = CreateCategoryData[i].cate_name,
                    CategoryNamebm = CreateCategoryData[i].cate_bm,
                    dzpq = "1",
                    endnum = int.Parse("00"),
                    EndString = "00",
                    ExpireDate = CreateCategoryData[i].cate_start_date.ToString(),
                    ExpireDateend = (CreateCategoryData[i].cate_start_date + CreateCategoryData[i].cate_end_date).ToString(),
                    flag = 0,
                    fmoney =CreateCategoryData[i].cate_price==""?decimal.Parse("9999"): decimal.Parse(CreateCategoryData[i].cate_price),
                    fnum = 0,
                    hotelcode = CreateFormulaData.hotelcode,
                    HotelId = CreateFormulaData.hotelcode,
                    isbool = int.Parse(isbool),//未确定
                    iscz = int.Parse(iscz),
                    istest = 0,
                    iswxly = 0,
                    iszs = CreateCategoryData[i].iszs == null ? 1 : int.Parse(CreateCategoryData[i].iszs),
                    maxNum = int.Parse(CreateFormulaData.pro_num == "" ? "999999" : CreateFormulaData.pro_num),
                    moneytype = 0,
                    pic = "",
                    rate = 1,
                    sflag = int.Parse(sflg),
                    Summary = CreateCategoryData[i].cate_summary,
                    type = 0,
                });
                var setcatefw = service.Setcategory_fw_pl_json("", "", Categoryid, CreateCategoryData[i].range, CreateFormulaData.hotelcode);
                var rangArr = CreateCategoryData[i].range.Split(',');
                foreach (var item1 in rangArr)
                {
                    if (item1.ToString() != "")
                    {
                        if (!iticketdb.Category_fw_t.Any(x => x.hotelcode == CreateFormulaData.hotelcode && x.CategoryId == cateint))
                        {
                            iticketdb.Category_fw_t.Add(new Category_fw_t
                            {
                                CategoryId = cateint,
                                hotelcode = CreateFormulaData.hotelcode,
                                usehotelcode = item1.ToString(),
                                addtime = DateTime.Now,

                            });
                        }
                    }
                }
                iticketdb.SaveChanges();
            }
        }
        #endregion
        #region 产品修改
        public static JsonReturn UpdateFormula(UpdateFormulaData data)
        {
            var res = new JsonReturn() { code = ApiCode.成功, msg = "成功" };
            var retformula = service.RetFormula_json(data.usercode, "", data.pro_name, data.summary==""?" ":data.summary, data.id, "00", data.price==""?"0":data.price, data.pro_num==""?"999999":data.pro_num, data.hotelcode);
            if (retformula.IndexOf("success") > -1)
            {
                var delformulafw = service.DelFormula_fw_json("", "", data.id, data.hotelcode);
                if (delformulafw.IndexOf("success") > -1)
                {
                    var SetFormula_fw = service.SetFormula_fw_pl_json("", "", data.id,  data.pro_arr,data.hotelcode);

                    int sum = 0;
                    if (data.arr != null && data.arr.Count > 0)
                    {
                        var dbstr = service.Setdbname_json("", "", data.pro_name, data.id, data.hotelcode).Replace("(", "").Replace(")", "");
                        for (int i = 0; i < data.arr.Count; i++)
                        {
                            var sflg = GetSflag(data.type, data.arr[i].cate_xz, data.arr[i].cate_type);
                            var iscz = GetIscz(sflg);
                            if (sflg == "4" || sflg == "5")
                            {
                                var isbool = GetIsBool(sflg, data.arr[i].isbool);
                                var Categoryid = service.Setcategory_cz_json
        ("", "", data.arr[i].cate_name, data.arr[i].cate_code, data.arr[i].cate_summary, data.hotelcode, "", "00", "00", "0", data.arr[i].cate_type, sflg, data.arr[i].cate_start_date.ToString(), (data.arr[i].cate_start_date + data.arr[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", data.arr[i].cate_bm, data.arr[i].cate_price, "0", data.arr[i].iszs, iscz, "");
                                if (Categoryid != "")
                                {
                                    SaveCatogory(data.arr, data.hotelcode, data.data_new[i], data.usercode, data.pro_num, data.yformul.dbid, i, sflg, isbool, Categoryid, iscz);
                                }
                                else
                                {
                                    sum++;
                                }
                            }
                            else
                            {
                                var isbool = GetIsBool(sflg, data.arr[i].isbool);
                                var Categoryid = service.Setcategory_json("", "", data.arr[i].cate_name, data.arr[i].cate_code, data.arr[i].cate_summary, data.hotelcode, "", "00", "00", "0", data.arr[i].cate_type, sflg, data.arr[i].cate_start_date.ToString(), (data.arr[i].cate_start_date + data.arr[i].cate_end_date).ToString(), "00", "1", isbool, "0", "1", data.arr[i].cate_bm, data.arr[i].cate_price, "0", data.arr[i].iszs, iscz);
                                if (Categoryid != "")
                                {
                                    SaveCatogory(data.arr, data.hotelcode, data.data_new[i], data.usercode, data.pro_num, data.yformul.dbid, i, sflg, isbool, Categoryid, iscz);
                                }
                                else
                                {
                                    sum++;
                                }
                            }
                        }
                        if (sum > 0)
                        {
                            res.code = ApiCode.子券定义失败;
                            res.msg = "子券定义失败";
                        }
                    }
                }
                else
                {
                    res.code = ApiCode.删除产品范围失败;
                    res.msg = "删除产品范围失败";
                }
            }
            else
            {
                res.code = ApiCode.修改产品信息失败;
                res.msg = "修改产品信息失败";
            }
            return res;
         //   var Setlog_pl = service.Setlog_pl_json(data.usercode,data.usercode,"产品定义","FormulaName,FormulaSummary,fmoney,fnum,hotelcode",)
        }
        #endregion
        #region 产品发行
        public static JsonReturn SetCRMTicketSn(SetCRMTicketSnModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var ticket_create = service.ticket_create_new_json(data.user, data.token, data.id, data.hotelcode,data.num, data.FormulaId);
            if (ticket_create.IndexOf("success") > -1)
            {
                var ticket_tp_create = service.ticket_tp_create_json(data.user, data.token, data.FormulaId, data.salemode, data.tmoney, data.num, data.oid, data.kpmoney, data.hotelcode);
                if (ticket_tp_create.IndexOf("success") > -1)
                {
                    var ticket_tpdata_pl_create = service.ticket_tpdata_pl_create_json(data.user, data.token, data.FormulaId, data.oid, data.num, data.sessionid, data.hotelcode);
                    if (ticket_tpdata_pl_create.IndexOf("success") > -1)
                    {
                        var tpdata_pl_snum = JsonHelper.GetJsonValue(ticket_tpdata_pl_create, "successnum");
                        var tpdata_pl_fnum = JsonHelper.GetJsonValue(ticket_tpdata_pl_create, "falsenum");
                        var SetCRM_ticketsn_pl = service.SetCRM_ticketsn_pl_json(data.user, data.token, data.usertype, data.bosscard, data.name, data.sjhm, data.birth, data.ident, data.cardhotel, data.sno, data.saleid, data.hotelcode, data.salemodeid, data.FormulaId, data.num, data.OperatorId);
                        if (SetCRM_ticketsn_pl.IndexOf("success") > -1)
                        {
                            var SetCRM_ticketsn_pl_snum = JsonHelper.GetJsonValue(SetCRM_ticketsn_pl, "successnum");
                            var SetCRM_ticketsn_pl_fnum = JsonHelper.GetJsonValue(SetCRM_ticketsn_pl, "falsenum");
                            jsonResult.code = ApiCode.成功;
                            jsonResult.msg = "成功";
                            var res = new
                            {
                                tpdata_pl_snum = tpdata_pl_snum,
                                tpdata_pl_fnum = tpdata_pl_fnum,
                                SetCRM_ticketsn_pl_snum = SetCRM_ticketsn_pl_snum,
                                SetCRM_ticketsn_pl_fnum = SetCRM_ticketsn_pl_fnum,
                            };
                            jsonResult.data = res;
                            
                        }
                        else
                        {
                            jsonResult.code = ApiCode.发行失败;
                            jsonResult.msg = "发行失败";
                            var res = new
                            {
                                tpdata_pl_snum = tpdata_pl_snum,
                                tpdata_pl_fnum = tpdata_pl_fnum,

                            };
                            jsonResult.data = res;

                        }

                    }
                    else
                    {
                        jsonResult.code = ApiCode.封套打包数据批量生成失败;
                        jsonResult.msg = "封套打包数据批量生成失败";
                    }

                }
                else
                {
                    jsonResult.code = ApiCode.产品封套生成失败;
                    jsonResult.msg = "产品封套生成失败";
                }
            }
            else
            {
                jsonResult.code = ApiCode.子券生成失败;
                jsonResult.msg = "子券生成失败";
            }

            return jsonResult;
        }
        #endregion

        #region 修改子券范围
        public static JsonReturn UpdateCategoryFw(UpdateCategoryFwModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            if (data.cate_arr != null)
            {
                var fwArr = data.cate_arr.Split(',');

                var list = iticketdb.Category_fw_t.Where(x => x.CategoryId == data.id && x.hotelcode == data.hotelcode).ToList();
                foreach (var item in list)
                {
                    iticketdb.Category_fw_t.Remove(item);
                }
                foreach (var item in fwArr)
                {
                    if (item != "")
                    {
                        iticketdb.Category_fw_t.Add(new Category_fw_t
                        {
                            CategoryId = data.id,
                            hotelcode = data.hotelcode,
                            usehotelcode = item.ToString(),
                            addtime = DateTime.Now,
                        });
                    }
                }
                iticketdb.SaveChanges();
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            else
            {
                jsonResult.code = ApiCode.参数不全;
                jsonResult.msg = "参数不全";
            }
            return jsonResult;
        }
        #endregion
        #endregion

        //添加可授权产品
        public static JsonReturn AddFormulaAuthorized(AddFormulaAuthorizedModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var formula = iticketdb.formula_authorized.FirstOrDefault(x => x.hotelcode == data.hotelcode && x.formulaid == data.formulaid);
            if (formula==null)
            {
                iticketdb.formula_authorized.Add(new formula_authorized
                {
                    formulaid = data.formulaid,
                    hotelcode = data.hotelcode,
                    addtime = DateTime.Now,
                    addname = data.addname,
                    formulaname = data.formulaname,
                });
                iticketdb.SaveChanges();
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            else
            {
                formula.formulaid=data.formulaid;
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "修改成功";
 
            }
            return jsonResult;
        }

        public static JsonReturn AuthorizedLog(AuthorizedLogModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var formula = iticketdb.AuthorizedLog_t.FirstOrDefault(x => x.hotelcode == data.hotelcode &&x.status==0&&x.roomcode==data.roomcode);
            if (formula == null)
            {
                var postData = "hotelcode=" +data.hotelcode;
                string info = HttpHepler.SendPost(Config.WxAPIUrl + "/API/GetWX.ashx?action=GetGZHxx", postData);
                var hotel = JsonConvert.DeserializeObject<HotelInfoJson>(info).data[0];
                var result = service.Set_newhy_new_json(hotel.YQTOperatorId, hotel.YQTOperatorId,data.hotelcode,data.formulaid.ToString(), data.mobile, data.hotelcode);
                var returncode = JsonHelper.GetJsonValue(result, "returncode");
                var tp_id = JsonHelper.GetJsonValue(result, "tp_id");
                if (returncode == "success")
                {
                    iticketdb.AuthorizedLog_t.Add(new AuthorizedLog_t
                    {
                        formulaid = data.formulaid,
                        hotelcode = data.hotelcode,
                        addtime = DateTime.Now,
                        mobile = data.mobile,
                        returncode = returncode,
                        roomcode = data.roomcode,
                        status = data.status,
                        tp_id = data.tp_id,
                        admin=data.admin,
                        cardno=data.cardno,
                        username=data.username,
                        formulaname=data.formulaname
                    });
                    iticketdb.SaveChanges();
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    var list = CommonApi.GetServiceList(data.hotelcode, "6", "GetOpenIdList");
                    if (list.data.Count > 0)
                    {
                        foreach (var item in list.data)
                        {
                            SendAuthMsg("授权成功", data.admin, data.username, "已授权", "备注", data.hotelcode, item.openid1,"AuthSuccess");
                        }
                    }   
                    //SendAuthMsg("授权成功",data.admin,data.username,"已授权","备注",data.hotelcode,)
                }
                else
                {
                    jsonResult.code = ApiCode.发行失败;
                    jsonResult.msg = "发行失败";
                }
            }
            else
            {

               
                if (iticketdb.AuthorizedLog_t.Any(x => x.hotelcode == data.hotelcode && x.status == 0 && x.roomcode == data.roomcode && x.mobile == data.mobile))
                {
                    formula.canceltime = DateTime.Now;
                    formula.canceladmin = data.canceladmin;
                    formula.status = data.status;
                    if (data.status == 1)
                    {
                        var result = service.Changetpstate("", "", formula.tp_id, "998", data.admin, formula.hotelcode);
                        var list = CommonApi.GetServiceList(data.hotelcode, "6", "GetOpenIdList");
                        if (list.data.Count > 0)
                        {
                            foreach (var item in list.data)
                            {
                                SendAuthMsg("授权取消", data.username, data.admin, "已取消", "备注", data.hotelcode, item.openid1, "AuthSuccess");
                            }
                        }   
                    }
                   
                    iticketdb.SaveChanges();
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "修改成功";
                }
                else
                {
                    jsonResult.code = ApiCode.已存在;
                    jsonResult.msg = "已存在";
                }

            }
            return jsonResult;
        }
        private static void SendAuthMsg(string first, string keyword1, string keyword2, string keyword3,string remark,string hotelcode,string openid,string name)
        {
            var paramData = new
            {
                first = new
                {
                    value = first,
                    color = "#173177",
                },
                keyword1 = new
                {
                    value = keyword1,
                    color = "#173177",
                },
                keyword2 = new
                {
                    value = keyword2,
                    color = "#173177",
                },
                keyword3 = new
                {
                    value = keyword3,
                    color = "#173177",
                },
                remark = new
                {
                    value =remark,
                    color = "#173177",
                },
            };
            var json = JsonConvert.SerializeObject(paramData);
            string TemplateData = "hotelcode=" + hotelcode + "&openid=" + openid + "&param=" + json + "&templateName="+name+"";
            HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);
        }
        public static JsonReturn GetFormulaAuthorized(AddFormulaAuthorizedModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var formula = iticketdb.formula_authorized.Where(x => x.hotelcode == data.hotelcode);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "成功";
            jsonResult.data = formula;
            return jsonResult;
        }

        public static JsonReturn DelFormulaAuthorized(AddFormulaAuthorizedModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var formula = iticketdb.formula_authorized.FirstOrDefault(x => x.hotelcode == data.hotelcode && x.formulaid == data.formulaid);
            if (formula != null)
            {
                iticketdb.formula_authorized.Remove(formula);
                iticketdb.SaveChanges();
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            return jsonResult;
        }
        //https://ks.kuaishun.net/BLL/Ticket/GetAuthorizedLog
        public static JsonReturn GetAuthorizedLog(AuthorizedLogModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var log = iticketdb.AuthorizedLog_t.FirstOrDefault(x => x.hotelcode == data.hotelcode && x.status == 0 && x.roomcode == data.roomcode);
            if (log == null)
            {
                jsonResult.code = ApiCode.没有实体数据;
                jsonResult.msg = "没有实体数据";
            }
            else
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
                jsonResult.data = log;
            }
          
            return jsonResult;
        }

        public static JsonReturn GetAuthorizedList(GetAuthorizedListModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var log = iticketdb.AuthorizedLog_t.Where(x => x.hotelcode == data.hotelcode &&x.addtime>=data.starttime&&x.addtime<=data.endtime).ToList();
            if (log == null)
            {
                jsonResult.code = ApiCode.没有实体数据;
                jsonResult.msg = "没有实体数据";
            }
            else
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
                jsonResult.data = log;
            }

            return jsonResult;
        }

        public static JsonReturn CheckConsume(List<CheckConsumeModel> data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var list = new List<object>();
            var listorgin = new List<object>();
            DateTimeFormatInfo dtFormat =new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "yyyy-MM-dd";
            foreach (var item in data)
            {
                var t1 = Convert.ToDateTime(item.time, dtFormat);
                var t2 = t1.AddDays(1);
                var one = iticketdb.CAVRecord_t.FirstOrDefault(x => x.addtime>=t1&&x.addtime<t2&&x.xfcode==item.xfcode);
                if (one != null)
                {
                    if (one.rate != item.rate || one.num != item.num)
                    {
                        listorgin.Add(new 
                        {
                            id=one.id,
                            excel_rate = item.rate,
                            excel_num= item.num,
                            db_rate=one.rate,
                            db_num=one.num,
                            xfcode=item.xfcode
                        });
                    }
                }
            }
          
            jsonResult.code = ApiCode.成功;
            jsonResult.data = listorgin;
            return jsonResult;
        }

        public static JsonReturn UpdateConsumeLog(UpdateConsumeLogModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var one = iticketdb.CAVRecord_t.FirstOrDefault(x => x.id == data.id);
            if (one != null)
            {
                one.rate = data.rate;
                one.num = data.num;
                one.xfcode = data.xfcode;
                iticketdb.SaveChanges();
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            return jsonResult;
        }

        public static JsonReturn BatchTransfer(BatchTransferModel data)
        {
            JsonReturn jsonResult = new JsonReturn();
            var querymodel  = new QueryTicketnewModel();
            querymodel.user="";
            querymodel.token="";
            querymodel.hotelGroupId="";
            querymodel.hotelcode=data.hotelcode;
            querymodel.mode="mobile";
            querymodel.code=data.oldmobile;
            var queryticket = QueryTicketNew(querymodel);
            if (queryticket!=null&&queryticket.Count > 0)
            {
                
                var count = data.newmobilelist.Sum(x => x.count);
                var list = queryticket.Where(x => x.bflag == "可使用" && x.FormulaId == data.formulaid).Take(count).ToList();
                var mobilelist = new List<string>();
                if (count == list.Count)
                {
                    for (int z = 0; z < count; z++)
                    {
                        for (int i = 0; i < data.newmobilelist.Count; i++)
                        {
                            for (int j = 0; j < data.newmobilelist[i].count; j++)
                            {
                                var res = service.Setticketsnchange(data.oldmobile, list[z].TicketSN, data.newmobilelist[i].mobile, data.grouptype, data.hotelcode, data.giftId, data.summary);
                                var returncode = JsonHelper.GetJsonValue(res, "returncode");
                                if (res != "success")
                                {
                                    mobilelist.Add(data.newmobilelist[i].mobile);
                                }
                            }
                        }

                    }
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "成功";
                    jsonResult.data = mobilelist;
                }
                else
                {
                    jsonResult.code = ApiCode.转赠数量不匹配;
                    jsonResult.msg = "转赠数量不匹配";
                }
            }
            return jsonResult;
        }
    }
}
