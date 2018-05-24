using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    ////获取单个商品详情Getproduct_onsale_single_json返回类
    public class ProductSingleSuccessItem
    {
        public string pdes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onsalecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hdcode { get; set; }
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
        public string price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yhprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 

        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ontop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string allnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sypp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sycc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pscjs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string yllx { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tgfzs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pscj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 杭州景澜·云台印象酒店云景大床房2间夜
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string salenum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 新品
        /// </summary>
        public string hdname { get; set; }
        /// <summary>
        /// 套餐
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string minprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string maxprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scorenum { get; set; }
        public string iswxpay { get; set; }
        public string canuseticket { get; set; }
    }
    public class GetProductSingleResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ProductSingleSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
