using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //删除购物车商品Stopbuyproduct_json
    public class StopBuyProductModel:ConmmonUserToken
    {
        
        public string id { get; set; }
        public string flag { get; set; }
    }
}
