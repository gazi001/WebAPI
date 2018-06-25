using BLL.WebApi.Model;
using KS.Common.SDK.AdvancedAPIs;
using KS.Mall.SDK.AdvancedAPIs;
using KS.Model;
using KS.Model.ApiResultModel;
using KS.Model.Common.CommonRequest;
using KS.Model.Mall;
using KS.Model.Mall.MallRequest;
using KS.Model.Mall.MallResponse;
using KS.Model.Ticket.TicketRequest;
using KS.Ticket.SDK.AdvancedAPIs;
using Newtonsoft.Json;
using RM.Common.DotNetCode;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace BLL.WebApi.Controllers
{
    public class MallController : BaseController
    {
        //
        // GET: /Mall/
        public string  Test()
        {
            var res = MallApi.QueryTradeNo("KSHZ", "KSHZ1805301401070003", "https://www.ksticket.com/booking/public/mallordere", Config.CheckWxPayUrl);
            return "";
        }
       
        private JsonReturn jsonResult = new JsonReturn();

        public JsonResult PaySuccess(string postData)
        {
            var model = JsonConvert.DeserializeObject<PaySuccessServiceModel>(postData);
            var res=MallApi.PaySuccessService(model);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            return this.Json(res);
        }
        public JsonResult PaySuccessTest(string postData)
        {
            var model = JsonConvert.DeserializeObject<PaySuccessServiceModel>(postData);
            MallApi.PaySuccessServiceTest(model);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            return this.Json(jsonResult);
        }

        #region 商城首页方法
        /// <summary>
        /// 获取轮播图列表
        /// </summary>
        /// <param name="data">获取轮播图参数</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetCarouselFigure(GetCarouselFigureModel data)
        {
            var result = MallApi.Getlp_img_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductsList(GetGoodsModel data)
        {
            var result = MallApi.Getproduct_onsale_front_all_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);

        }

        /// <summary>
        /// 获取活动
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetActivity(GetActivityModel data)
        {
            var result = MallApi.Gethd_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 首页三个接口整合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Index(GetActivityModel data)
        {
            var GetCarouselFigureModel = new GetCarouselFigureModel();
            GetCarouselFigureModel.hotelcode = data.hotelcode;
            var GetGoodsModel = new GetGoodsModel();
            GetGoodsModel.hotelcode = data.hotelcode;
            var GoodsResult = MallApi.Getproduct_onsale_front_all_json(GetGoodsModel);
            var ActivityResult = MallApi.Gethd_json(data);
            var CarouselFigureResult = MallApi.Getlp_img_json(GetCarouselFigureModel);
            var result = new
            {
                GoodsResult = GoodsResult.success,
                ActivityResult = ActivityResult.success,
                CarouselFigureResult = CarouselFigureResult.success,
            };
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = result;
            return this.MyJson(jsonResult);
        }

        [HttpPost]
        public JsonResult GetProductList(GetActivityModel data)
        {
            var GetCarouselFigureModel = new GetCarouselFigureModel();
            GetCarouselFigureModel.hotelcode = data.hotelcode;
            var GetGoodsModel = new GetGoodsModel();
            GetGoodsModel.hotelcode = data.hotelcode;
            var GoodsResult = MallApi.Getproduct_onsale_front_all_json(GetGoodsModel);
            if (GoodsResult.returncode != "false")
            {
                var list = new List<GetIndexProductList>();
                if (GoodsResult.success.Count > 0)
                {
                    GetProductPriceModel pricedata = new GetProductPriceModel();
                    foreach (var item in GoodsResult.success)
                    {
                        if (item.showendtime != "" && item.showendtime != "")
                        {
                            var t1 = DateTime.Parse(item.showstarttime);
                            var t2 = DateTime.Parse(item.showendtime);
                            if (DateTime.Now >= t1 && DateTime.Now <= t2)
                            {
                                var one = new GetIndexProductList()
                                {
                                    priceList = new List<GetProductPriceSuccessItem>()
                                };
                                one.goodItem = item;
                                pricedata.onsalecode = item.onsalecode;
                                var price = MallApi.Getproduct_price_json(pricedata);
                                if (price.success != null)
                                {
                                    if (price.success.Count > 0)
                                    {
                                        // list.Add(price.success);
                                        one.priceList.AddRange(price.success);
                                        list.Add(one);
                                    }
                                }
                            }
                        }
                    }
                }
                var ActivityResult = MallApi.Gethd_json(data);
                GetClassModel type = new GetClassModel{user="",token="",hotelcode=data.hotelcode,type="0"};
                var ClassResult = MallApi.Gettype_json(type);
                var CarouselFigureResult = MallApi.Getlp_img_json(GetCarouselFigureModel);
                var result = new
                {
                    ClassResult=ClassResult,
                    GoodsResult = list,
                    ActivityResult = ActivityResult.success,
                    CarouselFigureResult = CarouselFigureResult.success,
                };
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result;
            }
            else
            {

                var ActivityResult = MallApi.Gethd_json(data);
                var CarouselFigureResult = MallApi.Getlp_img_json(GetCarouselFigureModel);
                if (CarouselFigureResult.returncode != "false")
                {
                    var result = new
                    {
                        CarouselFigureResult = CarouselFigureResult.success,
                    };
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";
                    jsonResult.data = result;
                }
                else
                {
                    jsonResult.code = ApiCode.库存不足;
                    jsonResult.msg = "没有添加轮播图";
                }
            }
            return this.MyJson(jsonResult);
 
        }

        [HttpPost]
        public JsonResult GetProductListByKey(GetActivityModel data)
        {
            var GetCarouselFigureModel = new GetCarouselFigureModel();
            GetCarouselFigureModel.hotelcode = data.hotelcode;
            var GetGoodsModel = new GetGoodsModel();
            GetGoodsModel.hotelcode = data.hotelcode;
            GetGoodsModel.key = data.key;
            //适用范围筛选
            var GoodsResult = MallApi.Getproduct_onsale_front_all_bykey_json(GetGoodsModel);

            var list = new List<GetIndexProductList>();
            if (GoodsResult.success.Count > 0)
            {
                GetProductPriceModel pricedata = new GetProductPriceModel();
                foreach (var item in GoodsResult.success)
                {
                    var one = new GetIndexProductList()
                    {
                        priceList = new List<GetProductPriceSuccessItem>()
                    };
                    one.goodItem = item;
                    pricedata.onsalecode = item.onsalecode;
                    var price = MallApi.Getproduct_price_json(pricedata);
                    if (price.success.Count > 0)
                    {
                        // list.Add(price.success);
                        one.priceList.AddRange(price.success);
                        list.Add(one);
                    }
                }
            }
            var ActivityResult = MallApi.Gethd_json(data);
            var CarouselFigureResult = MallApi.Getlp_img_json(GetCarouselFigureModel);
            var result = new
            {
                GoodsResult = list,
                ActivityResult = ActivityResult.success,
                CarouselFigureResult = CarouselFigureResult.success,
            };
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = result;
            return this.MyJson(jsonResult);
        }
        #endregion

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetClass(GetClassModel data)
        {
            var result = MallApi.Gettype_json(data);
            var list = new List<object>();
            foreach (var item in result.success)
            {
               // var type = item;
                GetProducttypeModel model = new GetProducttypeModel();
                model.type = item.typecode;
                model.hotelcode = data.hotelcode;
                var product = MallApi.Getproduct_onsale_bytype_json(model);
                var json = new
                {
                    type = item,
                    product = product.success,
                };
                list.Add(json);
            }
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = list; 
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 查询单个分类下商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductType(GetProducttypeModel data)
        {
           
            var result = MallApi.Getproduct_onsale_bytype_json(data);
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

        public JsonResult GetProductList(GetClassModel data)
        {
            var typelist = MallApi.Gettype_json(data);
            var list = new List<object>();
            if (typelist.returncode != "false")
            {
                if (typelist.success.Count > 0)
                {
                    GetProducttypeModel model = new GetProducttypeModel();
                    foreach (var item in typelist.success)
                    {
                        model.type = item.typecode;
                        model.hotelcode = data.hotelcode;
                        var product = MallApi.Getproduct_onsale_bytype_json(model);
                        list.Add(product.success);
                    }
                }
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = list;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetOrderRemark(SetOrderRemarkModel data)
        {
            //data.ispay = "";
            //data.oid = "";
            //data.paymoney = "";
            //data.remark = "";
            //data.token = "";
            //data.user = "";
            var result = MallApi.SetOrderRemark(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
               // jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 新增评价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetProductScore(SetProductScoreModel data)
        {
            var result = MallApi.SetProductScore(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                //jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 根据手机号查询该会员所有订单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderByMobile(GetOrderByMobileModel data)
        {
            var result = MallApi.GetOrderByMobile(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 根据订单号查询订单详情
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderSingle(GetOrderSingleModel data)
        {
            var result = MallApi.GetOrderSingle(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
       //-------------------------------------分割线-------------------------------------------//

        /// <summary>
        ///   查询购物车详情  
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBuyProductSingle(GetBuyProductSingleModel data)
        {
            var result = MallApi.Getbuyproduct_single_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
 
        }

        /// <summary>
        /// 订单详情写入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetOrderDetails(SetOrderDetailsModel data)
        {
            var result = MallApi.Setorder_details_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
               
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 订单生成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetOrder(SetOrderModel data)
        {
            var result = MallApi.Setorder_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";

            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Setbuyproduct(SetbuyproductModel data)
        {
            data.user = "";
            data.token = "";
            data.ordercode = "123456";
            var result = MallApi.Setbuyproduct_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";

            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 获取用户购物车信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetBuyProduct(GetBuyProductModel data)
        {
            var result = MallApi.Getbuyproduct_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 收藏列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductScSearch(GetProductScSearchModel data)
        {
            var result = MallApi.Getproduct_sc_search_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
 
        }

        /// <summary>
        /// 根据openid获取收藏列表
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductScByOpenId(GetProductScAndGetBossCard data)
        {
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            if (result.returncode != "false")
            {
                var SetProductScModel = new GetProductScSearchModel();
                SetProductScModel.bosscard = result.bosscard;
                SetProductScModel.hotelcode = data.hotelcode;
                SetProductScModel.mobile = result.mobile;
                SetProductScModel.flag = "0";
                SetProductScModel.useraccount = result.cardno;

                var IsSuccess = MallApi.Getproduct_sc_search_json(SetProductScModel);
                if (IsSuccess.returncode != "false")
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "获取成功";
                    jsonResult.data = IsSuccess;
                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "接口调用失败";
                    //jsonResult.data = IsSuccess;
                }
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        ///  删除购物车商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StopBuyProduct(string postData)
        {
            var list = JsonConvert.DeserializeObject<DelCarModel>(postData);
            var model = new StopBuyProductModel();
            model.flag = "1";
            foreach (var item in list.list)
            {
                model.id = item;
                MallApi.Stopbuyproduct_json(model);
            }
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 修改购物车内容
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetbuyProductSingle(SetbuyProductSingleModel data)
        {
            var result = MallApi.Setbuyproduct_single_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                // jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 根据活动code获取商品
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductByCode(GetCodeToGoodsModel data)
        {
            var result = MallApi.Getproduct_onsale_byhdcode_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 获取单个商品详情
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductSingle(GetProductSingleModel data)
        {
            var result = MallApi.Getproduct_onsale_single_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 获取单个商品轮播图
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductImg(GetProductImgModel data)
        {
            var result = MallApi.Getproduct_img_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 获取商品信息（轮播图、详情）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductInfo(GetProductImgModel data)
        {
            var imgList = MallApi.Getproduct_img_json(data);
            var GetProductSingleModel= new GetProductSingleModel();
            GetProductSingleModel.onsalecode=data.onsalecode;
            var productInfo = MallApi.Getproduct_onsale_single_json(GetProductSingleModel);
            var result = new
            {
                Info = productInfo.success,
                Img = imgList.success,
            };
            if (result != null)
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
        /// 商品评论
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductScore(GetProductScoreModel data)
        {
            var result = MallApi.Getproduct_score_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 商品规格获取
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductPrice(GetProductPriceModel data)
        {
            var result = MallApi.Getproduct_price_json(data);
            if (result.returncode!= "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        [HttpPost]// 根据适用时间和价格性质获取未支付订单数
        public JsonResult GetNotPayOrderNumByPrice(GetProductPriceModel data)
        {
            var num = MallApi.GetNotPayOrderNumByPrice(data);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = num;
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 查询是否收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult ProductScSearch(ProductScSearchModel data)
        {
            var result = MallApi.Getproduct_sc_search_bymobile_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 新增收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetProductSc(SetProductScModel data)
        {
            var result = MallApi.Setproduct_sc_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.returncode;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 新增收藏（判断是否会员）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetProductScNew(SetCollectionAndGetBossCard data)
        {
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            if (result.returncode != "false")
            {
                var SetProductScModel = new SetProductScModel();
                SetProductScModel.bosscard = result.bosscard;
                SetProductScModel.hotelcode = data.hotelcode;
                SetProductScModel.mobile = result.mobile;
                SetProductScModel.onsalecode = data.onsalecode;
                SetProductScModel.useraccount = result.cardno;
                SetProductScModel.oid = result.bosscard;
                var IsSuccess = MallApi.Setproduct_sc_json(SetProductScModel);
                if (IsSuccess.returncode != "false")
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "收藏成功";
                    jsonResult.data = result;
                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "接口调用失败";
                    //jsonResult.data = IsSuccess;
                }
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }
        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult StopProductSc(StopProductScModel data)
        {
            var result = MallApi.Stopproduct_sc_json(data);
            if (result.returncode == "false")
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            else
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.returncode;
            }
            return this.MyJson(jsonResult);
 
        }

        /// <summary>
        ///  根据手机号获取所有评价
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductScore_ByMobile(GetProductScoreByMobileModel data)
        {
            data.pcode = "";
            data.user = "";
            data.token = "";
            var result = MallApi.Getproduct_score_bymobile_json(data);
            if (result.returncode != "false")
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "接口调用成功";
                jsonResult.data = result.success;
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        public static object _Lock = new object();
        /// <summary>
        /// 下单整合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PlaceOrderTest(PlaceOrderModel data)
        {
            //var _Lock = new object();
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            if (result.returncode != "false")
            {
                var ticketArr = data.linkticketsn.Split(',');
                //云券通限购查询
                var GetFormulaInfoByFormulaidModel = new GetFormulaInfoByFormulaidModel();
                GetFormulaInfoByFormulaidModel.hotelcode = data.hotelcode;
                GetFormulaInfoByFormulaidModel.formulaid = data.linkticketsn;
                var yqtxg = TicketApi.GetFormula_info_byformulaid_json(GetFormulaInfoByFormulaidModel);
                if (yqtxg != null)
                {
                    if (Convert.ToInt64(data.num) + Convert.ToInt64(yqtxg.maxnum) <= Convert.ToInt64(yqtxg.fnum))
                    {
                        var GetFormulaXgModel = new GetFormulaXgModel();
                        GetFormulaXgModel.formulaid = data.linkticketsn;
                        GetFormulaXgModel.hotelcode = data.hotelcode;
                        GetFormulaXgModel.mobile = result.mobile;
                        GetFormulaXgModel.num = data.num;

                        var xgresult = TicketApi.Get_formula_xg_json(GetFormulaXgModel);
                        if (xgresult.returncode == "true")
                        {
                            var GetorderByonsalecodeModel = new GetorderByonsalecodeModel();
                            GetorderByonsalecodeModel.onsalecode = data.onsalecode;
                            GetorderByonsalecodeModel.jgxz = data.jgxz;
                            GetorderByonsalecodeModel.sysj = data.sysj;
                            GetorderByonsalecodeModel.mobile = result.mobile;
                            GetorderByonsalecodeModel.hotelcode = data.hotelcode;
                            var scxg = MallApi.Getorder_byonsalecode_json(GetorderByonsalecodeModel);
                            int number = 0;
                            if (scxg.returncode != "false")
                            {
                                for (int i = 0; i < scxg.success.Count; i++)
                                {
                                    if (scxg.success[i].state != "3")
                                    {
                                        number = number + int.Parse(scxg.success[i].num);
                                    }
                                }
                            }
                            if (number + Convert.ToInt64(data.num) <= Convert.ToInt64(data.xgnum))
                            {
                                var GetproductPriceOthModel = new GetproductPriceOthModel();
                                GetproductPriceOthModel.jgxz = data.jgxz;
                                GetproductPriceOthModel.onsalecode = data.onsalecode;
                                GetproductPriceOthModel.sysj = data.sysj;
                                var stock = MallApi.GetproductPriceOth(GetproductPriceOthModel);
                                if (stock.success[0].num >= int.Parse(data.num))
                                {
                                    var ordercode = OrderHelper.GetRandom1(data.hotelcode);
                                    var totalprice = (decimal.Parse(data.price) * int.Parse(data.num)).ToString();
                                    var SetOrderDetailsModel = new SetOrderDetailsModel();
                                    SetOrderDetailsModel.buycode = ordercode;
                                    SetOrderDetailsModel.jgxz = data.jgxz;
                                    SetOrderDetailsModel.linkticketsn = data.linkticketsn;
                                    SetOrderDetailsModel.num = data.num;
                                    SetOrderDetailsModel.oid = result.bosscard;
                                    SetOrderDetailsModel.onsalecode = data.onsalecode;
                                    SetOrderDetailsModel.ordercode = ordercode;
                                    SetOrderDetailsModel.price = data.price;
                                    SetOrderDetailsModel.sysj = data.sysj;
                                    SetOrderDetailsModel.totalprice = totalprice;
                                    SetOrderDetailsModel.buyuseticketsn = data.buyuseticketsn;
                                    var SetOrderDetailsResult = MallApi.Setorder_details_json(SetOrderDetailsModel);
                                    var model = new SetOrderModel();
                                    model.address = "";
                                    model.bosscard = result.bosscard;
                                    model.hotelcode = data.hotelcode;
                                    model.ispay = "0";
                                    model.mobile = result.mobile;
                                    model.name = result.name;
                                    model.oid = result.bosscard;
                                    model.ordercode = ordercode;
                                    model.paycode = data.paycode;
                                    model.paymoney = totalprice;
                                    model.remark = "";
                                    model.sessionid = OrderHelper.GetRandom1("M_" + data.hotelcode);
                                    model.sex = "";
                                    model.state = "1";
                                    model.thfs = "";
                                    model.totalprice = totalprice;
                                    model.useraccount = result.cardno;
                                    model.zipcode = "";
                                    var setorderresult = MallApi.Setorder_json(model);
                                    if (setorderresult.returncode == "true" && SetOrderDetailsResult.returncode == "true")
                                    {
                                        jsonResult.code = ApiCode.成功;
                                        jsonResult.msg = "成功";
                                        jsonResult.data = ordercode;
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.接口调用失败;
                                        jsonResult.msg = "下单失败";
                                    }
                                }
                                else
                                {
                                    jsonResult.code = ApiCode.库存不足;
                                    jsonResult.msg = "库存不足";
                                }
                            }
                            else
                            {
                                jsonResult.code = ApiCode.达到限购;
                                jsonResult.msg = "限购";
                            }
                        }
                        else
                        {
                            jsonResult.code = ApiCode.达到限购;
                            jsonResult.msg = "限购";
                        }
                    }
                    else
                    {
                        jsonResult.code = ApiCode.云券通限购失败;
                        jsonResult.msg = "云券通限购失败";
                    }
                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "GetFormula_info_byformulaid_json接口调用失败";
                }
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 下单整合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PlaceOrder(PlaceOrderModel data)
        {
            //var _Lock = new object();
            var str = JsonConvert.SerializeObject(data);
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            var IsPlaceOrder = false;
            if (result.returncode != "false")
            {
                var ticketArr = data.linkticketsn.Split(',');
                //云券通限购查询
                foreach (var item in ticketArr)
                {
                    var GetFormulaInfoByFormulaidModel = new GetFormulaInfoByFormulaidModel();
                    GetFormulaInfoByFormulaidModel.hotelcode = data.hotelcode;
                    GetFormulaInfoByFormulaidModel.formulaid = item;
                    var yqtxg = TicketApi.GetFormula_info_byformulaid_json(GetFormulaInfoByFormulaidModel);
                    if (yqtxg != null)
                    {
                        if (Convert.ToInt64(data.num) + Convert.ToInt64(yqtxg.maxnum) <= Convert.ToInt64(yqtxg.fnum))
                        {
                            var GetFormulaXgModel = new GetFormulaXgModel();
                            GetFormulaXgModel.formulaid = item;
                            GetFormulaXgModel.hotelcode = data.hotelcode;
                            GetFormulaXgModel.mobile = result.mobile;
                            GetFormulaXgModel.num = data.num;

                            var xgresult = TicketApi.Get_formula_xg_json(GetFormulaXgModel);
                            if (xgresult.returncode == "true")
                            {
                                var GetorderByonsalecodeModel = new GetorderByonsalecodeModel();
                                GetorderByonsalecodeModel.onsalecode = data.onsalecode;
                                GetorderByonsalecodeModel.jgxz = data.jgxz;
                                GetorderByonsalecodeModel.sysj = data.sysj;
                                GetorderByonsalecodeModel.mobile = result.mobile;
                                GetorderByonsalecodeModel.hotelcode = data.hotelcode;
                                var scxg = MallApi.Getorder_byonsalecode_json(GetorderByonsalecodeModel);
                                int number = 0;
                                if (scxg.returncode != "false")
                                {
                                    for (int i = 0; i < scxg.success.Count; i++)
                                    {
                                        if (scxg.success[i].state != "3")
                                        {
                                            number = number + int.Parse(scxg.success[i].num);
                                        }
                                    }
                                }
                                if (number + Convert.ToInt64(data.num) <= Convert.ToInt64(data.xgnum))
                                {
                                    var GetproductPriceOthModel = new GetproductPriceOthModel();
                                    GetproductPriceOthModel.jgxz = data.jgxz;
                                    GetproductPriceOthModel.onsalecode = data.onsalecode;
                                    GetproductPriceOthModel.sysj = data.sysj;
                                    var stock = MallApi.GetproductPriceOth(GetproductPriceOthModel);
                                    if (stock.success[0].num >= int.Parse(data.num))
                                    {
                                        IsPlaceOrder = true;
                                        
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.库存不足;
                                        jsonResult.msg = "库存不足";
                                    }
                                }
                                else
                                {
                                    jsonResult.code = ApiCode.达到限购;
                                    jsonResult.msg = "限购";
                                }
                            }
                            else
                            {
                                jsonResult.code = ApiCode.达到限购;
                                jsonResult.msg = "限购";
                            }
                        }
                        else
                        {
                            jsonResult.code = ApiCode.云券通限购失败;
                            jsonResult.msg = "云券通限购失败";
                        }
                    }
                    else
                    {
                        jsonResult.code = ApiCode.接口调用失败;
                        jsonResult.msg = "GetFormula_info_byformulaid_json接口调用失败";
                    }
                }
                //var GetFormulaInfoByFormulaidModel = new GetFormulaInfoByFormulaidModel();
                //GetFormulaInfoByFormulaidModel.hotelcode = data.hotelcode;
                //GetFormulaInfoByFormulaidModel.formulaid = data.linkticketsn;
                if (IsPlaceOrder)
                {
                    var ordercode = OrderHelper.GetRandom1(data.hotelcode);
                    var totalprice = (decimal.Parse(data.price) * int.Parse(data.num)).ToString();
                    var SetOrderDetailsModel = new SetOrderDetailsModel();
                    SetOrderDetailsModel.buycode = ordercode;
                    SetOrderDetailsModel.jgxz = data.jgxz;
                    SetOrderDetailsModel.linkticketsn = data.linkticketsn;
                    SetOrderDetailsModel.num = data.num;
                    SetOrderDetailsModel.oid = result.bosscard;
                    SetOrderDetailsModel.onsalecode = data.onsalecode;
                    SetOrderDetailsModel.ordercode = ordercode;
                    SetOrderDetailsModel.price = data.price;
                    SetOrderDetailsModel.sysj = data.sysj;
                    SetOrderDetailsModel.totalprice = totalprice;
                    SetOrderDetailsModel.buyuseticketsn = data.buyuseticketsn;
                    var SetOrderDetailsResult = MallApi.Setorder_details_json(SetOrderDetailsModel);
                    var model = new SetOrderModel();
                    model.address = "";
                    model.bosscard = result.bosscard;
                    model.hotelcode = data.hotelcode;
                    model.ispay = "0";
                    model.mobile = result.mobile;
                    model.name = result.name;
                    model.oid = result.bosscard;
                    model.ordercode = ordercode;
                    model.paycode = data.paycode;
                    model.paymoney = totalprice;
                    model.remark = "";
                    model.sessionid = OrderHelper.GetRandom1("M_" + data.hotelcode);
                    model.sex = "";
                    model.state = "1";
                    model.thfs = "";
                    model.totalprice = totalprice;
                    model.useraccount = result.cardno;
                    model.zipcode = "";
                    var setorderresult = MallApi.Setorder_json(model);
                    if (setorderresult.returncode == "true" && SetOrderDetailsResult.returncode == "true")
                    {
                        jsonResult.code = ApiCode.成功;
                        jsonResult.msg = "成功";
                        jsonResult.data = ordercode;
                    }
                    else
                    {
                        jsonResult.code = ApiCode.接口调用失败;
                        jsonResult.msg = "下单失败";
                    }
                }
           
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }


        /// <summary>
        /// 购物车下单整合
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult MakeOrder(PlaceOrderModel data)
        {
            var _Lock = new object();
            var ordercode = OrderHelper.GetRandom1(data.hotelcode);
            var totalprice = "";
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            if (result.returncode != "false")
            {
                var GetFormulaXgModel = new GetFormulaXgModel();
                var GetBuyProductSingleModel = new GetBuyProductSingleModel();
                string[] spid = data.id.Split(',');//，拼接传入的商品id
                for (int i = 0; i < spid.Length-1; i++)//循环查商品信息
                {
                    var IsPlaceOrder = false;
                    if (spid[i] != "")
                    {
                        GetBuyProductSingleModel.id = spid[i];
                        var xdresult = MallApi.Getbuyproduct_single_json(GetBuyProductSingleModel);
                        if (xdresult.returncode != "false")
                        {
                            var ticketArr = xdresult.success[0].linkticketsn.Split(',');
                            foreach (var item in ticketArr)
                            {
                                if (item != "")
                                {
                                    //云券通限购查询
                                    var GetFormulaInfoByFormulaidModel = new GetFormulaInfoByFormulaidModel();
                                    GetFormulaInfoByFormulaidModel.hotelcode = data.hotelcode;
                                    GetFormulaInfoByFormulaidModel.formulaid = item;
                                    var yqtxg = TicketApi.GetFormula_info_byformulaid_json(GetFormulaInfoByFormulaidModel);
                                    if (yqtxg != null)
                                    {
                                        if (Convert.ToInt64(xdresult.success[0].num) + Convert.ToInt64(yqtxg.maxnum) <= Convert.ToInt64(yqtxg.fnum))
                                        {
                                            GetFormulaXgModel.formulaid = item;
                                            GetFormulaXgModel.hotelcode = xdresult.success[0].hotelcode;
                                            GetFormulaXgModel.mobile = result.mobile;
                                            GetFormulaXgModel.num = xdresult.success[0].num;
                                            var xgresult = TicketApi.Get_formula_xg_json(GetFormulaXgModel);
                                            if (xgresult.returncode == "true")
                                            {
                                                var GetproductPriceOthModel = new GetproductPriceOthModel();
                                                GetproductPriceOthModel.jgxz = xdresult.success[0].jgxz;
                                                GetproductPriceOthModel.onsalecode = xdresult.success[0].onsalecode;
                                                GetproductPriceOthModel.sysj = xdresult.success[0].sysj;
                                                var stock = MallApi.GetproductPriceOth(GetproductPriceOthModel);

                                                var GetorderByonsalecodeModel = new GetorderByonsalecodeModel();
                                                GetorderByonsalecodeModel.onsalecode = xdresult.success[0].onsalecode;
                                                GetorderByonsalecodeModel.jgxz = xdresult.success[0].jgxz;
                                                GetorderByonsalecodeModel.sysj = xdresult.success[0].sysj;
                                                GetorderByonsalecodeModel.mobile = result.mobile;
                                                GetorderByonsalecodeModel.hotelcode = data.hotelcode;
                                                var scxg = MallApi.Getorder_byonsalecode_json(GetorderByonsalecodeModel);
                                                int number = 0;
                                                if (scxg.returncode != "false")
                                                {
                                                    for (int a = 0; a < scxg.success.Count; a++)
                                                    {
                                                        if (scxg.success[a].state != "3")
                                                        {
                                                            number = number + 1;
                                                        }
                                                    }
                                                }
                                                if (number + Convert.ToInt64(xdresult.success[0].num) <= Convert.ToInt64(stock.success[0].xgnum))
                                                {
                                                    if (stock.success[0].num >= int.Parse(xdresult.success[0].num))
                                                    {
                                                        //ordercode = OrderHelper.GetRandom1(xdresult.success[0].hotelcode);
                                                        IsPlaceOrder = true;
                                                    }
                                                    else
                                                    {
                                                        jsonResult.code = ApiCode.库存不足;
                                                        jsonResult.msg = "库存不足";
                                                        return this.MyJson(jsonResult);
                                                    }
                                                }
                                                else
                                                {
                                                    jsonResult.code = ApiCode.达到限购;
                                                    jsonResult.msg = "限购";
                                                    return this.MyJson(jsonResult);
                                                }
                                            }
                                            else
                                            {
                                                jsonResult.code = ApiCode.达到限购;
                                                jsonResult.msg = "限购";
                                                return this.MyJson(jsonResult);
                                            }

                                        }
                                        else
                                        {
                                            jsonResult.code = ApiCode.云券通限购失败;
                                            jsonResult.msg = "云券通限购失败";
                                            return this.MyJson(jsonResult);
                                        }
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.接口调用失败;
                                        jsonResult.msg = "GetFormula_info_byformulaid_json接口调用失败";
                                        return this.MyJson(jsonResult);
                                    }
                                }
                            }
                            if (IsPlaceOrder)
                            {
                                totalprice = (decimal.Parse(xdresult.success[0].price) * int.Parse(xdresult.success[0].num)).ToString();
                                var SetOrderDetailsModel = new SetOrderDetailsModel();
                                SetOrderDetailsModel.buycode = ordercode;
                                SetOrderDetailsModel.jgxz = xdresult.success[0].jgxz;
                                SetOrderDetailsModel.linkticketsn = xdresult.success[0].linkticketsn;
                                SetOrderDetailsModel.num = xdresult.success[0].num;
                                SetOrderDetailsModel.oid = result.bosscard;
                                SetOrderDetailsModel.onsalecode = xdresult.success[0].onsalecode;
                                SetOrderDetailsModel.ordercode = ordercode;
                                SetOrderDetailsModel.price = xdresult.success[0].price;
                                SetOrderDetailsModel.sysj = xdresult.success[0].sysj;
                                SetOrderDetailsModel.totalprice = totalprice;
                                SetOrderDetailsModel.buyuseticketsn = xdresult.success[0].linkticketsn;
                                var SetOrderDetailsResult = MallApi.Setorder_details_json(SetOrderDetailsModel);
                            }
                        }
                        else
                        {
                            jsonResult.code = ApiCode.接口调用失败;

                            jsonResult.msg = "Getbuyproduct_single_json接口调用失败";
                            return this.MyJson(jsonResult);
                        }
                    }

                }
                var model = new SetOrderModel();
                model.address = "";
                model.bosscard = result.bosscard;
                model.hotelcode = data.hotelcode;
                model.ispay = "0";
                model.mobile = result.mobile;
                model.name = result.name;
                model.oid = result.bosscard;
                model.ordercode = ordercode;
                model.paycode = data.paycode;
                model.paymoney = totalprice;
                model.remark = "";
                model.sessionid = OrderHelper.GetRandom1("M_" + data.hotelcode);
                model.sex = "";
                model.state = "1";
                model.thfs = "";
                model.totalprice = totalprice;
                model.useraccount = result.cardno;
                model.zipcode = "";
                model.tjcode = data.tjcode;
                var setorderresult = MallApi.Setorder_json(model);
                if (setorderresult.returncode == "true")
                {
                    for (int i = 0; i < spid.Length-1; i++)
                    {
                        var StopBuyProductModel = new StopBuyProductModel();
                        StopBuyProductModel.id = spid[i];
                        StopBuyProductModel.flag = "1";
                        var stop = MallApi.Stopbuyproduct_json(StopBuyProductModel);
                        if (stop.returncode == "true")
                        {
                            jsonResult.code = ApiCode.成功;
                            jsonResult.msg = "成功";
                            jsonResult.data = ordercode;
                        }
                        else
                        {
                            jsonResult.code = ApiCode.接口调用失败;
                            jsonResult.msg = "Stopbuyproduct_json接口调用失败";
                            jsonResult.data = ordercode;
                            return this.MyJson(jsonResult);
                        }
                    }
                 }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "下单失败";
                }
                
            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }



        [HttpPost]
        public JsonResult MakeOrderTest(PlaceOrderModel data)
        {
            var _Lock = new object();
            var ordercode = OrderHelper.GetRandom1(data.hotelcode);
            var totalprice = "";
            var Getbosscard = new GetBosscardModel();
            Getbosscard.openid = data.openid;
            var result = CommonApi.Getbosscard(Getbosscard);
            if (result.returncode != "false")
            {
                var GetFormulaXgModel = new GetFormulaXgModel();
                var GetBuyProductSingleModel = new GetBuyProductSingleModel();
                string[] spid = data.id.Split(',');//，拼接传入的商品id
                for (int i = 0; i < spid.Length - 1; i++)//循环查商品信息
                {
                    GetBuyProductSingleModel.id = spid[i];
                    var xdresult = MallApi.Getbuyproduct_single_json(GetBuyProductSingleModel);
                    if (xdresult.returncode == null)
                    {
                        //云券通限购查询
                        var GetFormulaInfoByFormulaidModel = new GetFormulaInfoByFormulaidModel();
                        GetFormulaInfoByFormulaidModel.hotelcode = data.hotelcode;
                        GetFormulaInfoByFormulaidModel.formulaid = xdresult.success[0].linkticketsn;
                        var yqtxg = TicketApi.GetFormula_info_byformulaid_json(GetFormulaInfoByFormulaidModel);
                        if (yqtxg != null)
                        {
                            if (Convert.ToInt64(xdresult.success[0].num) + Convert.ToInt64(yqtxg.maxnum) <= Convert.ToInt64(yqtxg.fnum))
                            {
                                GetFormulaXgModel.formulaid = xdresult.success[0].linkticketsn;
                                GetFormulaXgModel.hotelcode = xdresult.success[0].hotelcode;
                                GetFormulaXgModel.mobile = result.mobile;
                                GetFormulaXgModel.num = xdresult.success[0].num;
                                var xgresult = TicketApi.Get_formula_xg_json(GetFormulaXgModel);
                                if (xgresult.returncode == "true")
                                {
                                    var GetproductPriceOthModel = new GetproductPriceOthModel();
                                    GetproductPriceOthModel.jgxz = xdresult.success[0].jgxz;
                                    GetproductPriceOthModel.onsalecode = xdresult.success[0].onsalecode;
                                    GetproductPriceOthModel.sysj = xdresult.success[0].sysj;
                                    var stock = MallApi.GetproductPriceOth(GetproductPriceOthModel);

                                    var GetorderByonsalecodeModel = new GetorderByonsalecodeModel();
                                    GetorderByonsalecodeModel.onsalecode = xdresult.success[0].onsalecode;
                                    GetorderByonsalecodeModel.jgxz = xdresult.success[0].jgxz;
                                    GetorderByonsalecodeModel.sysj = xdresult.success[0].sysj;
                                    GetorderByonsalecodeModel.mobile = result.mobile;
                                    GetorderByonsalecodeModel.hotelcode = data.hotelcode;
                                    var scxg = MallApi.Getorder_byonsalecode_json(GetorderByonsalecodeModel);
                                    int number = 0;
                                    if (scxg.returncode != "false")
                                    {
                                        for (int a = 0; a < scxg.success.Count; a++)
                                        {
                                            if (scxg.success[a].state != "3")
                                            {
                                                number = number + 1;
                                            }
                                        }
                                    }
                                    if (number + Convert.ToInt64(xdresult.success[0].num) <= Convert.ToInt64(stock.success[0].xgnum))
                                    {
                                        if (stock.success[0].num >= int.Parse(xdresult.success[0].num))
                                        {
                                            //ordercode = OrderHelper.GetRandom1(xdresult.success[0].hotelcode);
                                            totalprice = (decimal.Parse(xdresult.success[0].price) * int.Parse(xdresult.success[0].num)).ToString();
                                            var SetOrderDetailsModel = new SetOrderDetailsModel();
                                            SetOrderDetailsModel.buycode = ordercode;
                                            SetOrderDetailsModel.jgxz = xdresult.success[0].jgxz;
                                            SetOrderDetailsModel.linkticketsn = xdresult.success[0].linkticketsn;
                                            SetOrderDetailsModel.num = xdresult.success[0].num;
                                            SetOrderDetailsModel.oid = result.bosscard;
                                            SetOrderDetailsModel.onsalecode = xdresult.success[0].onsalecode;
                                            SetOrderDetailsModel.ordercode = ordercode;
                                            SetOrderDetailsModel.price = xdresult.success[0].price;
                                            SetOrderDetailsModel.sysj = xdresult.success[0].sysj;
                                            SetOrderDetailsModel.totalprice = totalprice;
                                            SetOrderDetailsModel.buyuseticketsn = stock.success[0].buyuseticketsn;
                                            var SetOrderDetailsResult = MallApi.Setorder_details_json(SetOrderDetailsModel);
                                        }
                                        else
                                        {
                                            jsonResult.code = ApiCode.库存不足;
                                            jsonResult.msg = "库存不足";
                                            return this.MyJson(jsonResult);
                                        }
                                    }
                                    else
                                    {
                                        jsonResult.code = ApiCode.达到限购;
                                        jsonResult.msg = "限购";
                                        return this.MyJson(jsonResult);
                                    }
                                }
                                else
                                {
                                    jsonResult.code = ApiCode.达到限购;
                                    jsonResult.msg = "限购";
                                    return this.MyJson(jsonResult);
                                }

                            }
                            else
                            {
                                jsonResult.code = ApiCode.云券通限购失败;
                                jsonResult.msg = "云券通限购失败";
                                return this.MyJson(jsonResult);
                            }
                        }
                        else
                        {
                            jsonResult.code = ApiCode.接口调用失败;
                            jsonResult.msg = "GetFormula_info_byformulaid_json接口调用失败";
                            return this.MyJson(jsonResult);
                        }
                    }
                    else
                    {
                        jsonResult.code = ApiCode.接口调用失败;

                        jsonResult.msg = "Getbuyproduct_single_json接口调用失败";
                        return this.MyJson(jsonResult);
                    }

                }
                var model = new SetOrderModel();
                model.address = "";
                model.bosscard = result.bosscard;
                model.hotelcode = data.hotelcode;
                model.ispay = "0";
                model.mobile = result.mobile;
                model.name = result.name;
                model.oid = result.bosscard;
                model.ordercode = ordercode;
                model.paycode = data.paycode;
                model.paymoney = totalprice;
                model.remark = "";
                model.sessionid = OrderHelper.GetRandom1("M_" + data.hotelcode);
                model.sex = "";
                model.state = "1";
                model.thfs = "";
                model.totalprice = totalprice;
                model.useraccount = result.cardno;
                model.zipcode = "";
                var setorderresult = MallApi.Setorder_json(model);
                if (setorderresult.returncode == "true")
                {
                    for (int i = 0; i < spid.Length - 1; i++)
                    {
                        var StopBuyProductModel = new StopBuyProductModel();
                        StopBuyProductModel.id = spid[i];
                        StopBuyProductModel.flag = "1";
                        var stop = MallApi.Stopbuyproduct_json(StopBuyProductModel);
                        if (stop.returncode == "true")
                        {
                            jsonResult.code = ApiCode.成功;
                            jsonResult.msg = "成功";
                            jsonResult.data = ordercode;
                        }
                        else
                        {
                            jsonResult.code = ApiCode.接口调用失败;
                            jsonResult.msg = "Stopbuyproduct_json接口调用失败";
                            jsonResult.data = ordercode;
                            return this.MyJson(jsonResult);
                        }
                    }
                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "下单失败";
                }

            }
            else
            {
                jsonResult.code = ApiCode.非会员;
                jsonResult.msg = "非会员";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetScore(GetMobileScoreModel data)
        {
            string[] pcode = data.pcode.Split(',');
            string[] score = data.score.Split(',');
            string[] remark = data.remark.Split(',');
            string[] jgxz = data.jgxz.Split(',');
            string[] sysj = data.sysj.Split(',');
            for (int i = 0; i < score.Length-1; i++)
            {
                var SetProductScoreModel = new SetProductScoreModel();
                SetProductScoreModel.pcode = pcode[i];
                SetProductScoreModel.score = score[i];
                SetProductScoreModel.remark = remark[i];
                SetProductScoreModel.jgxz = jgxz[i];
                SetProductScoreModel.sysj = sysj[i];
                SetProductScoreModel.mobile = data.mobile;
                SetProductScoreModel.oid = data.oid;
                var setscore = MallApi.SetProductScore(SetProductScoreModel);
                if (setscore.returncode != "true")
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "SetProductScore第"+i+"次接口调用失败";
                    return this.MyJson(jsonResult);
                }
            }
            var SetOrderRemarkModel = new SetOrderRemarkModel();
            SetOrderRemarkModel.user = data.user;
            SetOrderRemarkModel.token = data.token;
            SetOrderRemarkModel.ordercode = data.ordercode;
            SetOrderRemarkModel.ispay = data.ispay;
            SetOrderRemarkModel.state = data.state;
            SetOrderRemarkModel.remark = data.orderremark;
            SetOrderRemarkModel.paymoney = data.paymoney;
            SetOrderRemarkModel.oid = data.oid;
            var result = MallApi.SetOrderRemark(SetOrderRemarkModel);
            if (result.returncode != "true")
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            else
            {
                jsonResult.code = ApiCode.成功;
                jsonResult.msg = "成功";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 加入购物车(整合限制接口)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SetOrUpdateShopCardAll(string postdata)
        {
            var data=JsonConvert.DeserializeObject<SetOrUpdateShopCardModel>(postdata);
            data.Setbuyproduct.user= "";
            data.Setbuyproduct.token = "";
            data.Setbuyproduct.ordercode = "123456";
            var GetBuyProductOther = new GetBuyProductOtherModel();
            GetBuyProductOther.user = data.Setbuyproduct.user;
            GetBuyProductOther.token = data.Setbuyproduct.token;
            GetBuyProductOther.oid = data.Setbuyproduct.oid;
            GetBuyProductOther.onsalecode = data.Setbuyproduct.pcode;
            GetBuyProductOther.sysj = data.Setbuyproduct.sysj;
            GetBuyProductOther.jgxz = data.Setbuyproduct.jgxz;
            var getshopcard = MallApi.Getbuyproduct_other_json(GetBuyProductOther);
            if (getshopcard.returncode != "false")
            {
                data.SetbuyProductSingle.id = getshopcard.success[0].id;
                var result = MallApi.Setbuyproduct_single_json(data.SetbuyProductSingle);
                if (result.returncode != "false")
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";
                    // jsonResult.data = result.success;
                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "接口调用失败";
                }
            }
            else
            {
                var result = MallApi.Setbuyproduct_json(data.Setbuyproduct);
                if (result.returncode != "false")
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";

                }
                else
                {
                    jsonResult.code = ApiCode.接口调用失败;
                    jsonResult.msg = "接口调用失败";
                }
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 商品规格获取,复合接口
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetProductPriceFH(string postdata)
        {
            var data = JsonConvert.DeserializeObject < GetProductPriceFHModel > (postdata);
            var result = MallApi.Getproduct_price_json(data.GetProductPrice);
            if (result.returncode != "false")
            {
                data.Getproduct_xg.formulaid = result.success[0].linkticketsn;
                var xgnum = TicketApi.GetproductXg(data.Getproduct_xg);
                if (xgnum.returncode != "false")
                {
                    result.success[0].xgnum = xgnum.xgnum;
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";
                    jsonResult.data = result.success;
                }
                else
                {
                    jsonResult.code = ApiCode.成功;
                    jsonResult.msg = "接口调用成功";
                    jsonResult.data = result.success;
                }
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }

        /// <summary>
        /// 商城下单使用后台绑定的优惠券
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetOrderTocketSN(string postdata)
        {
            var data = JsonConvert.DeserializeObject<GetOrderTocketSNModel>(postdata);
            var result = MallApi.QueryOrder(data.hotelcode,data.ordercode);
            var isst = "0";
            if (result[0].returncode != "false")
            {
                List<returnData> list = new List<returnData>();
                var ticketdata = TicketApi.Queryticketnew_nowx_bdly_json(data.QueryticketNewNowxBDLY);
                string cpid = "";
                for (int i = 0; i < result[0].success.Count; i++)
                {
                    var GetProductSingleModel = new GetProductSingleModel();
                    GetProductSingleModel.onsalecode = result[0].success[i].onsalecode;
                    var productInfo = MallApi.Getproduct_onsale_single_json(GetProductSingleModel);
                    if(productInfo.success!=null&&productInfo.success.Count>0)
                    {
                        if (productInfo.success[0].isst == "1")
                        {
                            isst ="1";
                        }
                    }
                    cpid += result[0].success[i].buyuseticketsn + ",";
                }
                if (ticketdata != null)
                {
                    for (int a = 0; a < ticketdata.Count; a++)
                    {
                        string ticketxx = ticketdata[a].FormulaId + ":" + ticketdata[a].CategoryId + ":" + "0";
                        if (cpid.IndexOf(ticketxx) >= 0)
                        {
                            if (ticketdata[a].sflag == "3" && ticketdata[a].CategoryId != "7" && ticketdata[a].stateid == "999" && ticketdata[a].categorycode == "LP" && Convert.ToDecimal(ticketdata[a].fmoney) > 1)
                            {
                                list.Add(new returnData
                                {
                                    fmoney = ticketdata[a].fmoney,
                                    CategoryNamebm = ticketdata[a].CategoryNamebm,
                                    ticketsn = ticketdata[a].TicketSNold,
                                    ticket = ticketdata[a].TicketSN,
                                });

                            }
                        }
                    }
                }
                var Da = new rtData
                {
                    returnData = list,
                    QueryOrder = result,
                    isst = isst
                };
                //returndata = returndata.Substring(0, returndata.Length - 1);
                jsonResult.code  = ApiCode.成功;
                jsonResult.msg = "成功";
                jsonResult.data = Da;
               
            }
            else
            {
                jsonResult.code = ApiCode.接口调用失败;
                jsonResult.msg = "接口调用失败";
            }
            return this.MyJson(jsonResult);
        }


        /// <summary>
        /// 核销券，修改订单状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// [HttpPost]
        public JsonResult Setticket_yhquse_json(TicketJLAddTicketSNNewSetModel data)
        {
            var result = TicketApi.Setticket_yhquse_json(data);
            
            bool aa= MallApi.UpdateOrderState(data.resno,data.remark,data.state,data.oid,data.paymoney,data.ispay,data.hotelcode);
            if (result.msg == "success" && aa == true)
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

        public JsonResult Getbuttom_info_json(GetCarouselFigureModel data)
        {
            var result = MallApi.Getbuttom_info_json(data);
            jsonResult.code = ApiCode.成功;
            jsonResult.msg = "接口调用成功";
            jsonResult.data = result.success;
            return this.MyJson(jsonResult);
        }

        
        [HttpPost]
       // [ValidateModel]
        public JsonResult Test(Product p)
        {
          if( ModelState.IsValid)
               
            return this.MyJson(new { s = "ok" });
            else

       
                return this.MyJson(new { s = "false"});
            
        }
    }

}
 