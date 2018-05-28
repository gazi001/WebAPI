using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //商品规格获取Getproduct_price_json
    public class GetProductPriceModel:ConmmonUserToken
    {
        public string onsalecode { get; set; }
        public string pname { get; set; }
        public string hotelcode { get; set; }
        public string sysj { get; set; }
        public string jgxz { get; set; }
    }
}
