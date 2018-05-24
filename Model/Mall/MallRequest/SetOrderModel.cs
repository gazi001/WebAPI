using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //订单生成Setorder_json
    public class SetOrderModel : ConmmonUserToken
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string ordercode { get; set; }
        public string sex { get; set; }
        public string bosscard { get; set; }
        public string useraccount { get; set; }
        public string paycode { get; set; }
        public string paymoney { get; set; }
        public string ispay { get; set; }
        public string state { get; set; }
        public string remark { get; set; }
        public string thfs { get; set; }
        public string hotelcode { get; set; }
        public string sessionid { get; set; }
        public string totalprice { get; set; }
        public string oid { get; set; }
    }
}
