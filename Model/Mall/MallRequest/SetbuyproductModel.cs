using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //加入购物车Setbuyproduct_json
    public class SetbuyproductModel:ConmmonUserToken
    {
        public string ordercode { get; set; }
        public string onsalecode { get; set; }
        public string price { get; set; }
        public string yhprice { get; set; }
        public string num { get; set; }
        public string totalprice { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string linkticketsn { get; set; }
        public string pcode { get; set; }
        public string oid { get; set; }
    }
}
