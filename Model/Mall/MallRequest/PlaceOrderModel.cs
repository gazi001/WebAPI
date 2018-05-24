using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    /// <summary>
    /// 商城下单
    /// </summary>
   public class PlaceOrderModel:ConmmonUserToken
    {
       //onsalecode,price,yhprice,num,jgxz,sysj,linkticketsn,hotelcode,openid
        public string onsalecode { get; set; }
        public string price { get; set; }
        public string yhprice { get; set; }
        public string num { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string linkticketsn { get; set; }
        public string hotelcode { get; set; }
        public string openid { get; set; }
        public string paycode { get; set; }
        public string id { get; set; }
        public string formulaid { get; set; }
        public string xgnum { get; set; }
        public string buyuseticketsn { get; set; }
    }
}
