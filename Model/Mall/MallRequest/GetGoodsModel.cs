using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetGoodsModel:ConmmonUserToken
    {
        //获取所有商品Getproduct_onsale_front_all_json
        public string hotelcode { get; set; }
        public string key { get; set; }
    }
}
