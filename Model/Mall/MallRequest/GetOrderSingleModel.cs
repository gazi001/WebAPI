using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
   public class GetOrderSingleModel:ConmmonUserToken
    {
       //获取单个商品详情Getproduct_onsale_single_json
        public string  ordercode{get;set;}
        public string hotelcode { get; set; }
    }
}
