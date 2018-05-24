using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //获取用户购物车信息Getbuyproduct_json
    public class GetBuyProductModel:ConmmonUserToken
    {
        public string oid { get; set; }
    }
}
