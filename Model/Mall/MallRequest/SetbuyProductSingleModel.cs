using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //修改购物车内容Setbuyproduct_single_json
    public class SetbuyProductSingleModel:ConmmonUserToken
    {
        public string ordercode { get; set; }
        public string pcode { get; set; }
        //public string onsalecode { get; set; }
        public string price { get; set; }
        public string yhprice { get; set; }
        public string num { get; set; }
        public string totalprice { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string linkticketsn { get; set; }
        public string id { get; set; }
    }
}
