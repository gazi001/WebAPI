using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.ApiResultModel
{
    public class QueryOrderModel
    {
        public List<Success> success { get; set; }
        public string returncode { get; set; }
    }
    
    public class Success
    {
        /// <summary>
        /// 407
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 快顺科技
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 15382322857
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zipcode { get; set; }
        /// <summary>
        /// LDSPT201711061542123859
        /// </summary>
        public string ordercode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 2017/11/6 15:45:18
        /// </summary>
        public string addtime { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// KS101944
        /// </summary>
        public string addname { get; set; }
        /// <summary>
        /// KS101944
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// VZ101944
        /// </summary>
        public string useraccount { get; set; }
        /// <summary>
        /// WX
        /// </summary>
        public string paycode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paymoney { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public string ispay { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thfs { get; set; }
        /// <summary>
        /// LDSPT201711062592
        /// </summary>
        public string sessionid { get; set; }
        /// <summary>
        /// LDSPT
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// LDSPT201711028859
        /// </summary>
        public string onsalecode { get; set; }
        /// <summary>
        /// 0.01
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 3
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 0.03
        /// </summary>
        public string totalprice { get; set; }
        /// <summary>
        /// 普通标准房
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 非周末（周日至周四）
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 306
        /// </summary>
        public string linkticketsn { get; set; }
        /// <summary>
        /// MuXi2017_02-24-36_r3e3.jpg
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 普通标准房限时抢购
        /// </summary>
        public string pname { get; set; }
        /// <summary>
        /// 59
        /// </summary>
        public string allnum { get; set; }
        /// <summary>
        /// 0.03
        /// </summary>
        public string fmoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string buyuseticketsn { get; set; }
        public string canuseticket { get; set; }
        public string iswxpay { get; set; }
    }
}
