using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //收藏列表Getproduct_sc_search_json
    public class GetProductScSearchSuccessItem
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
        /// 所有商品
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
        /// <summary>
        /// 测试2212
        /// </summary>
        public string name { get; set; }
    }
    public class GetProductScSearchResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetProductScSearchSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
