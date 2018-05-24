using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //商品规格获取Getproduct_price_json
    public class GetProductPriceSuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onsalecode { get; set; }
        /// <summary>
        /// 套餐1
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 周末
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkticketsn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string canuseticketsn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gzcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string starttime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xgnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buyuseticketsn { get; set; }
        public string oldprice { get; set; }
    }
    public class GetProductPriceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetProductPriceSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
