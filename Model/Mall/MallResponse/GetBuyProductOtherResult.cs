using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    public class GetBuyProductOtherSuccessItem
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
        /// 
        /// </summary>
        public string hdcode { get; set; }
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
        public string addtime { get; set; }
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
        public string totalprice { get; set; }
        /// <summary>
        /// 秒杀
        /// </summary>
        public string hdname { get; set; }
        /// <summary>
        /// 限量抢购
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 非周末
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 199元限量抢购
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkticketsn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string kcnum { get; set; }
    }
    public class GetBuyProductOtherResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<GetBuyProductOtherSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
