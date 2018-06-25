
using KS.Model;
using KS.Model.ApiResultModel;
using KS.Ticket.SDK.AdvancedAPIs;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;
using RM.Common.DotNetJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Common.SDK.AdvancedAPIs;
using KS.Model.Mall.MallRequest;
using KS.Model.Mall.MallResponse;
using KS.Common.SDK;
using KS.Model.Mall;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;
using RM.Common.DotNetModel;

namespace KS.Mall.SDK.AdvancedAPIs
{
    public class MallApi
    {
        /// <summary>
        /// 商城接口
        /// </summary>
        static net.kuaishun.shopinterface.Service service = new net.kuaishun.shopinterface.Service();
        static net.kuaishun.interfacecrs.Service myservice = new net.kuaishun.interfacecrs.Service();
        public static object _lock = new object();
        public static void Test(string ordercode)
        {
            var tradeno = QueryTradeNo("HZYHQFL", ordercode, "https://www.ksticket.com/booking/public/mallorder", Config.CheckWxPayUrl);
            if (tradeno.code == StatusCode.成功 && tradeno.transaction_id != null)
            {

               // var order = service.Getorder_byall_json("", "", "", "", "", ordercode, "", "", "HZYHQFL", "").Replace("(","").Replace(")","");
                //var list = JsonConvert.DeserializeObject<List<QueryOrderModel>>(order);
                //var result = service.Setorder_single_remark_json("", "", ordercode, "1", "2", "[已支付]微信支付，微信单号:" + tradeno.transaction_id, "692", "QFL-CXB-001");
                // var res = service.Setproduct_onsale_single_salenum_json("", "", list.FirstOrDefault().success[0].onsalecode, list.FirstOrDefault().success[0].num);
                //改状态
               //var isChange = UpdateOrderState(model.trade_no, "[已支付]微信支付，微信单号:" + tradeno.transaction_id+","+model.remark, "2", model.bosscard, model.total, "1",model.hotelcode);
            }
        }
        #region 商城支付成功处理
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
                    if (transaction_id != "" && transaction_id != null && transaction_id != "null")
                    {
                        obj.transaction_id = transaction_id;
                        obj.code = StatusCode.成功;
                        obj.msg = "成功";
                    }
                    else
                    {
                        obj.transaction_id = transaction_id;
                        obj.code = StatusCode.微信订单号不存在;
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
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="remark"></param>
        /// <param name="state"></param>
        /// <param name="oid"></param>
        /// <param name="paymoney"></param>
        /// <param name="ispay"></param>
        /// <returns></returns>
        public static bool UpdateOrderState(string ordercode,string remark,string state,string oid,string paymoney,string ispay,string hotelcode)
        {
            //if (hotelcode == "KSHZ")
            //{l
            lock (_lock)
            {
                bool isChange = true;
                var order = service.Getorder_single_json("", "", ordercode, hotelcode);
                var OrderList = JsonConvert.DeserializeObject<List<GetOrderSingleResult>>(order);
                if (OrderList != null && OrderList[0].success.Count > 0)
                {
                    foreach (var item in OrderList[0].success)
                    {
                        if (item.state == state || item.ispay == ispay)
                        {
                            isChange = false;
                        }
                    }
                }
                if (isChange)
                {
                    //net.kuaishun.shopinterface.Service service = new net.kuaishun.shopinterface.Service();
                    var result = service.Setorder_single_remark_json("", "", ordercode, ispay, state, remark, paymoney, oid);
                    var IsTrue = JsonHelper.GetJsonValue(result, "returncode");
                    if (IsTrue != "")
                    {

                        isChange = true;
                    }
                    else
                    {
                        isChange = false;
                    }
                }
                return isChange;
            }
          //  }
            //else
            //{
            //    //net.kuaishun.shopinterface.Service service = new net.kuaishun.shopinterface.Service();
            //    var result = service.Setorder_single_remark_json("", "", ordercode, ispay, state, remark, paymoney, oid);
            //    var IsTrue = JsonHelper.GetJsonValue(result, "returncode");
            //    if (IsTrue != "")
            //    {
            //        return true;
            //    }
            //    return false;
            //}
        }

        public static string shopname="";
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static List<QueryOrderModel> QueryOrder(string hotelcode, string ordercode)
        {
            var result = service.Getorder_single_json("", "", ordercode, hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<QueryOrderModel>>(result);
            return obj;
        }

        #region  模板消息发送方法
        /// <summary>
        /// 商城购买成功
        /// </summary>
        /// <param name="shopname"></param>
        /// <param name="total"></param>
        /// <param name="tel"></param>
        /// <param name="payway"></param>
        /// <param name="hotelcode"></param>
        /// <param name="guestname"></param>
        /// <param name="Template"></param>
        /// <param name="first"></param>
        public static void SendTempPaySuccess(string shopname, string total, string tel, string payway, string hotelcode, string guestname,string Template,string first)
        {
            string TemplateData = "";
            var paramData = new
            {
                first = new
                {
                    value = first,
                    color = "#173177",
                },
                keyword1 = new
                {
                    value = shopname,
                    color = "#173177",
                },
                keyword2 = new
                {
                    value = total,
                    color = "#173177",
                },
                keyword3 = new
                {
                    value = DateTime.Now.ToString("yyyy-MM-dd"),
                    color = "#173177",
                },
                remark = new
                {
                    value = "如有疑问请及时联系我们:" + tel,
                    color = "#173177",
                },
            };
            var json = JsonConvert.SerializeObject(paramData);
            TemplateData = "hotelcode=" + hotelcode + "&openid=" + guestname + "&param=" + json + "&templateName=PaySuccess";
            if (hotelcode != "YLHCSD" && hotelcode != "JINLANJIA")
            {
                HttpHepler.SendPost(Config.SendTemplateUrl, TemplateData);
            }
            else
            {
                CommonApi.SendMsg("", "GMCG", "", hotelcode);
            }
        }
        #endregion
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <param name="hotelcode"></param>
        /// <param name="type"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private static GetServiceListModel GetServiceList(string hotelcode, string type, string action)
        {
            var postData = "hotelcode=" + hotelcode + "&type=" + type;
            var result=HttpHepler.SendPost(Config.BookingUrl+"?action="+action, postData);
            var list = JsonConvert.DeserializeObject<GetServiceListModel>(result);
            return list;
        }
        /// <summary>
        /// 商城支付成功处理
        /// /// </summary>
        /// <param name="model"></param>
        public static JsonReturn PaySuccessService(PaySuccessServiceModel model)
        {
            //查订单
            JsonReturn res = new JsonReturn();
            res.code = ApiCode.成功;
            res.msg = "成功";
            var tradeno = QueryTradeNo(model.hotelcode, model.trade_no, model.notify, Config.CheckWxPayUrl);
            if (tradeno.code == StatusCode.成功)
            {
                //改状态
                var isChange = UpdateOrderState(model.trade_no, "[已支付]微信支付，微信单号:" + tradeno.transaction_id + "," + model.remark, "2", model.bosscard, model.total, "1", model.hotelcode);
                if (isChange)
                {
                    var orderlist = QueryOrder(model.hotelcode, model.trade_no);
                    if (orderlist[0].success.Count > 0)
                    {
                        shopname = "";
                        var isSendTicket = TicketApi.SendTicket(orderlist, model.user, ref shopname);
                        if (isSendTicket)
                        {
                            //var r = service.Setproduct_onsale_single_salenum_json(model.user, model.user, data.onsalecode, data.salenum);
                            SendTempPaySuccess(shopname, model.total, model.tel, model.payway, model.hotelcode, model.openid, "", "您好，您已成功购买");
                            var list = GetServiceList(model.hotelcode, "3", "GetOpenIdList");
                            if (list.data.Count > 0)
                            {
                                foreach (var item in list.data)
                                {
                                    if (model.name != "" || model.mobile != "")
                                    {
                                        SendTempPaySuccess(shopname, model.total, model.tel, model.payway, model.hotelcode, item.openid1, "", "您好，商城有一笔新订单\n购买人：" + model.name + "\n联系方式：" + model.mobile);
                                    }
                                    else
                                    {
                                        SendTempPaySuccess(shopname, model.total, model.tel, model.payway, model.hotelcode, item.openid1, "", "您好，商城有一笔新订单\n购买人：" + model.name + "\n联系方式：" + orderlist[0].success[0].mobile);
                                    }
                                }
                            }
                        }
                        else
                        {
                            res.code = ApiCode.发送产品失败;
                            res.msg = "发送产品失败";
                        }
                    }
                }
                else
                {
                    res.code = ApiCode.已支付;
                    res.msg = "已支付";
                }
            }
            else
            {
                res.code = ApiCode.没有找到微信支付订单;
                res.msg = "没有找到微信支付订单";
            }
            return res;
        }

        public static void PaySuccessServiceTest(PaySuccessServiceModel model)
        {
            //查订单
            var tradeno = QueryTradeNo(model.hotelcode, model.trade_no, model.notify, Config.CheckWxPayUrl);
            if (tradeno.code == StatusCode.成功 && tradeno.transaction_id != null)
            {
                //改状态
                var isChange = UpdateOrderState(model.trade_no, "[已支付]微信支付，微信单号:" + tradeno.transaction_id + "," + model.remark, "2", model.bosscard, model.total, "1",model.hotelcode);
                var orderlist = QueryOrder(model.hotelcode, model.trade_no);
                if (orderlist[0].success.Count > 0)
                {
                    shopname = "";
                    var isSendTicket = TicketApi.SendTicketTest(orderlist, model.user, ref shopname,model);
                    if (isSendTicket)
                    {

                        //var r = service.Setproduct_onsale_single_salenum_json(model.user, model.user, data.onsalecode, data.salenum);
                        SendTempPaySuccess(shopname, model.total, model.tel, model.payway, model.hotelcode, model.openid, "", "您好，您已成功购买");
                        var list = GetServiceList(model.hotelcode, "3", "GetOpenIdList");
                        if (list.data.Count > 0)
                        {
                            foreach (var item in list.data)
                            {
                                SendTempPaySuccess(shopname, model.total, model.tel, model.payway, model.hotelcode, item.openid1, "", "您好，商城有一笔新订单\n购买人：" + model.name + "\n联系方式：" + model.mobile);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 首页接口
        /// <summary>
        /// 获取轮播图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetCarouselFigureResult Getlp_img_json(GetCarouselFigureModel data)
        {
            string imgdetails = service.Getlp_img_json(data.user, data.token, data.hotelcode);
            imgdetails = CommonFunction.Replacebracket(imgdetails);
            var json = JsonConvert.DeserializeObject<List<GetCarouselFigureResult>>(imgdetails)[0];
            return json;
        }

        /// <summary>
        /// 获取活动
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetActivityResult Gethd_json(GetActivityModel data)
        {
            string details = service.Gethd_json(data.user, data.token, data.hotelcode, data.flag);
            details = CommonFunction.Replacebracket(details);
            var returnStr = JsonConvert.DeserializeObject<List<GetActivityResult>>(details)[0];
            return returnStr;
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetGoodsResult Getproduct_onsale_front_all_json(GetGoodsModel data)
        {
            string details = service.Getproduct_onsale_front_all_json(data.user, data.token, data.hotelcode);
            details = CommonFunction.Replacebracket(details);
            var returnStr = JsonConvert.DeserializeObject<List<GetGoodsResult>>(details)[0];
            return returnStr;
        }
        /// <summary>
        /// 获取所有商品根据key
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetGoodsResult Getproduct_onsale_front_all_bykey_json(GetGoodsModel data)
        {
            string details = service.Getproduct_onsale_front_all_bykey_json(data.user, data.token, data.hotelcode,data.key);
            details = CommonFunction.Replacebracket(details);
            var returnStr = JsonConvert.DeserializeObject<List<GetGoodsResult>>(details)[0];
            return returnStr;
        }

        #endregion

        #region 分类页
        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetClassResult Gettype_json(GetClassModel data)
        {
            string imgdetails = service.Gettype_json(data.user, data.token,data.hotelcode,data.type);
            imgdetails = CommonFunction.Replacebracket(imgdetails);
            var json = JsonConvert.DeserializeObject<List<GetClassResult>>(imgdetails)[0];
            return json;
        }
        /// <summary>
        /// 查询单个分类下商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetProductTypeResult Getproduct_onsale_bytype_json(GetProducttypeModel data)
        {
            string imgdetails = service.Getproduct_onsale_bytype_json(data.user, data.token, data.hotelcode, data.type);
            imgdetails = CommonFunction.Replacebracket(imgdetails);
            var json = JsonConvert.DeserializeObject<List<GetProductTypeResult>>(imgdetails)[0];
            return json;
        }
        #endregion

        #region 活动商品页
        /// <summary>
        /// 根据活动code获取商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetCodeToGoodsResult Getproduct_onsale_byhdcode_json(GetCodeToGoodsModel data)
        {
            var result = service.Getproduct_onsale_byhdcode_json(data.user, data.token,data.hotelcode,data.activitycode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetCodeToGoodsResult>>(result)[0];
            return obj;
        }
        #endregion

        #region 商品详情页
        /// <summary>
        /// 获取单个商品详情
        /// </summary>
        /// <param name="data"></param> 
        /// <returns></returns>
        public static GetProductSingleResult Getproduct_onsale_single_json(GetProductSingleModel data)
        {
            var result = service.Getproduct_onsale_single_json(data.user, data.token,data.onsalecode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductSingleResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 获取单个商品轮播图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetProductImgResult Getproduct_img_json(GetProductImgModel data)
        {
            var result = service.Getproduct_img_json(data.user, data.token,data.onsalecode,data.flag);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductImgResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 商品评论
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetProductScoreResult Getproduct_score_json(GetProductScoreModel data)
        {
            var result = service.Getproduct_score_json(data.user, data.token,data.hotelcode,data.onsalecode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductScoreResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 商品规格获取
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetProductPriceResult Getproduct_price_json(GetProductPriceModel data)
        {
            var result = service.Getproduct_price_json(data.user, data.token, data.onsalecode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductPriceResult>>(result)[0];
            //var numstr = service.Getorder_byall_json("", "", "", "", "", "", "", "", "", data.pname).Replace("(", "").Replace(")", "");
            //var num = JsonConvert.DeserializeObject<List<GetOrderByMobileResult>>(numstr)[0];
            //if (num.success != null)
            //{
            //    var numlist = new List<object>();
            //    foreach (var item in num.success.GroupBy(x=>x.onsalecode))
            //    {
            //        foreach (var item1 in item)
            //        {
            //            if (item1.state == "1")
            //            {
            //                var one = new
            //                {
            //                    onsalecode = item.FirstOrDefault().onsalecode,
            //                    num=item.Count(),
            //                };
            //                numlist.Add(one);
            //            }
            //        }  
            //    }
            //    var res = new
            //    {
            //        goodInfo = obj,
            //        numlist = numlist
            //    };
 
            //}
            return obj;
        }

        /// <summary>
        /// 商品规格和未支付订单数量获取
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object GetproductPriceAndOrder(GetProductPriceModel data)
        {
            var result = service.Getproduct_price_json(data.user, data.token, data.onsalecode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductPriceResult>>(result)[0];
            var numstr = service.Getorder_byall_json("", "", "", "", "", "", "", "", data.hotelcode, data.pname).Replace("(", "").Replace(")", "");
            var num = JsonConvert.DeserializeObject<List<GetOrderByMobileResult>>(numstr)[0];
            var numlist = new List<object>();
            if (num.success != null)
            {

                foreach (var item in num.success.GroupBy(x => x.onsalecode))
                {
                    foreach (var item1 in item)
                    {
                        if (item1.state == "1")
                        {
                            var one = new
                            {
                                onsalecode = item.FirstOrDefault().onsalecode,
                                num = item.Count(),
                            };
                            numlist.Add(one);
                        }
                    }
                }
                var res = new
                {
                    goodInfo = obj,
                    numlist = numlist
                };
                return res;
            }
            else
            {
                var res = new
                {
                    goodInfo = obj,
                    numlist = numlist
                };
                return res;
            }
          
        }

        /// <summary>
        /// 根据适用时间和价格性质获取未支付订单数
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int GetNotPayOrderNumByPrice(GetProductPriceModel data)
        {
            var numstr = service.Getorder_byall_json("", "", "", "", "", "", "", "", data.hotelcode, data.pname).Replace("(", "").Replace(")", "");
            var numobj = JsonConvert.DeserializeObject<List<GetOrderByMobileResult>>(numstr)[0];
            if (numobj.success != null)
            {
                var num = numobj.success.Where(x => x.sysj == data.sysj && x.jgxz == data.jgxz&&x.state=="1").ToList().Count;
                return num;
            }
            return 0;
        }
        #endregion

        #region 商品收藏
        /// <summary>
        /// 查询是否收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ProductScSearchResult Getproduct_sc_search_bymobile_json(ProductScSearchModel data)
        {
            var result = service.Getproduct_sc_search_bymobile_json(data.user, data.token,data.hotelcode,data.onsalecode,data.mobile,data.flag);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<ProductScSearchResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 新增收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetProductScResult Setproduct_sc_json(SetProductScModel data)
        {
            var result = service.Setproduct_sc_json(data.user, data.token,data.bosscard,data.useraccount,data.hotelcode,data.mobile,data.onsalecode,data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetProductScResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StopProductScResult Stopproduct_sc_json(StopProductScModel data)
        {
            var result = service.Stopproduct_sc_json(data.user, data.token,data.hotelcode,data.flag,data.onsalecode,data.mobile);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<StopProductScResult>>(result)[0];
            return obj;
        }
        #endregion

        #region 购物车
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetbuyproductResult Setbuyproduct_json(SetbuyproductModel data)
        {
            var result = service.Setbuyproduct_json(data.user, data.token,data.ordercode,data.pcode,data.price,data.yhprice,data.num,data.totalprice,data.jgxz,data.sysj,data.linkticketsn,data.onsalecode,data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetbuyproductResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 获取用户购物车信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetBuyProductResult Getbuyproduct_json(GetBuyProductModel data)
        {
            var result = service.Getbuyproduct_json(data.user, data.token,data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetBuyProductResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetProductScSearchResult Getproduct_sc_search_json(GetProductScSearchModel data)
        {
            var result = service.Getproduct_sc_search_json(data.user, data.token,data.hotelcode,data.bosscard,data.useraccount,data.mobile,data.flag);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetProductScSearchResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 购物车规格
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //public static GetProductPriceResult Getproduct_price_json(GetProductPriceModel data)
        //{
        //    var result = service.Getproduct_price_json(data.user, data.token, data.onsalecode);
        //    result = CommonFunction.Replacebracket(result);
        //    var obj = JsonConvert.DeserializeObject<List<GetProductPriceResult>>(result)[0];
        //    return obj;
        //}

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StopBuyProductResult Stopbuyproduct_json(StopBuyProductModel data)
        {
            var result = service.Stopbuyproduct_json(data.user, data.token,data.id,data.flag);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<StopBuyProductResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 修改购物车内容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetbuyProductSingleResult Setbuyproduct_single_json(SetbuyProductSingleModel data)
        {
            var result = service.Setbuyproduct_single_json(data.user, data.token,data.ordercode,data.pcode,data.price,data.yhprice,data.num,data.totalprice,data.jgxz,data.sysj,data.linkticketsn,data.id);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject < List<SetbuyProductSingleResult>>(result)[0];
            return obj;
        }
        #endregion

        #region 订单
        /// <summary>
        /// 查询购物车详情
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetBuyProductSingleResult Getbuyproduct_single_json(GetBuyProductSingleModel data)
        {
            var result = service.Getbuyproduct_single_json(data.user, data.token,data.id);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetBuyProductSingleResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 订单详情写入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetOrderDetailsResult Setorder_details_json(SetOrderDetailsModel data)
        {
            var result = service.Setorder_details_json(data.user, data.token,data.buycode,data.ordercode,data.onsalecode,data.price,data.num,data.totalprice,data.jgxz,data.sysj,data.linkticketsn,data.buyuseticketsn,data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetOrderDetailsResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 订单生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetOrderResult Setorder_json(SetOrderModel data)
        {
            var result = service.Setorder_json(data.user, data.token, data.name, data.mobile, data.address, data.zipcode, data.ordercode, data.sex, data.bosscard, data.useraccount, data.paycode, data.paymoney, data.ispay, data.state, data.remark, data.thfs, data.hotelcode, data.sessionid, data.totalprice, data.oid, data.tjcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetOrderResult>>(result)[0];
            return obj;
        }
        #endregion

        #region
        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetOrderRemarkResult SetOrderRemark(SetOrderRemarkModel data)
        {
            var result = service.Setorder_single_remark_json(data.user, data.token, data.ordercode, data.ispay, data.state, data.remark, data.paymoney, data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetOrderRemarkResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 新增评价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetProductScoreResult SetProductScore(SetProductScoreModel data)
        {
            var result = service.Setproduct_score_json(data.user, data.token,data.pcode,data.score,data.remark,data.jgxz,data.sysj,data.mobile,data.oid);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetProductScoreResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 根据手机号查询该会员所有订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetOrderByMobileResult GetOrderByMobile(GetOrderByMobileModel data)
        {
            var result = service.Getorder_bymobile_json(data.user, data.token,data.mobile,data.hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetOrderByMobileResult>>(result)[0];
            return obj;
        }
        
        /// <summary>/
        /// 根据订单号查询订单详情
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetOrderSingleResult GetOrderSingle(GetOrderSingleModel data)
        {
            var result = service.Getorder_single_json(data.user, data.token, data.ordercode, data.hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetOrderSingleResult>>(result)[0];
            return obj;
        }
        #endregion
        /// <summary>
        /// 根据手机号获取所有评价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ProductScoreByMobileResult Getproduct_score_bymobile_json(GetProductScoreByMobileModel data)
        {
            var result = service.Getproduct_score_bymobile_json(data.user, data.token,data.hotelcode,data.pcode,data.mobile);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<ProductScoreByMobileResult>>(result)[0];
            return obj;
        }


        /// <summary>
        /// 产品修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static SetProductOnSaleSingleSaleNumResult SetProductOnSaleSingleSaleNum(SetProductOnSaleSingleSaleNumModel data)
        {
            var result = service.Setproduct_onsale_single_salenum_json(data.user, data.token, data.onsalecode, data.salenum);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<SetProductOnSaleSingleSaleNumResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 验证购物车车是否有该商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetBuyProductOtherResult Getbuyproduct_other_json(GetBuyProductOtherModel data)
        {
            var result = service.Getbuyproduct_other_json(data.user, data.token,data.oid, data.onsalecode, data.jgxz,data.sysj);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetBuyProductOtherResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 验证库存
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetproductPriceOthResult GetproductPriceOth(GetproductPriceOthModel data)
        {
            var result = service.Getproduct_price_oth_json("", "",  data.onsalecode, data.jgxz, data.sysj);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetproductPriceOthResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 商城限购
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetorderByonsalecodeResult Getorder_byonsalecode_json(GetorderByonsalecodeModel data)
        {
            var result = service.Getorder_byonsalecode_json("", "", data.onsalecode, data.jgxz, data.sysj,data.mobile,data.hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetorderByonsalecodeResult>>(result)[0];
            return obj;
        }

        /// <summary>
        /// 获取底部
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static GetGetbuttomResult Getbuttom_info_json(GetCarouselFigureModel data)
        {
            var result = service.Getbuttom_info_json("", "", data.hotelcode);
            result = CommonFunction.Replacebracket(result);
            var obj = JsonConvert.DeserializeObject<List<GetGetbuttomResult>>(result)[0];
            return obj;
 
        }
    }
}
