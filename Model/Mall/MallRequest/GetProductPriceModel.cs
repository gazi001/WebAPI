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
    }
}
