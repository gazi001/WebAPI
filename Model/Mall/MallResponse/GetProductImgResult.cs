using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //获取单个商品轮播图Getproduct_img_json
    public class ProductImgSuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onsalecode { get; set; }
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
        public string httpurl { get; set; }
    }
    public class GetProductImgResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ProductImgSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
