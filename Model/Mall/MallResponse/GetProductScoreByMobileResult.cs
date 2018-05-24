using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //Getproduct_score_bymobile_json根据手机号获取所有评价
    public class ProductScoreByMobileSuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 一般般
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// zhu+测试
        /// </summary>
        public string addname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addtime { get; set; }
        /// <summary>
        /// 优惠
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 秒杀
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 测试2212
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
    }
    public class ProductScoreByMobileResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ProductScoreByMobileSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
