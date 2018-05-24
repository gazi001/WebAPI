using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //商品是否收藏Getproduct_sc_search_bymobile_json
    public class ProductScSearchSuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string useraccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
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
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onsalecode { get; set; }
        /// <summary>
        /// 权益
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hdcode { get; set; }
        /// <summary>
        /// 特惠
        /// </summary>
        public string hdname { get; set; }
    }
    public class ProductScSearchResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ProductScSearchSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
