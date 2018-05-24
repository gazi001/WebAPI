using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetBuyProductOtherModel:ConmmonUserToken
    {
        //Getbuyproduct_other_json
        public string oid { get; set; }
        public string onsalecode { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
    }
}
