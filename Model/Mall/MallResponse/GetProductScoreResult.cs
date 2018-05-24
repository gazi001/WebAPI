using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //商品评论Getproduct_score_json
    public class GetProductScoreSuccessItem
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
        /// 
        /// </summary>
        public string allnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hf { get; set; }
    }
    public class GetProductScoreResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetProductScoreSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
