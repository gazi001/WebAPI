using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Mall
{
    public class ProductScSearchModel:ConmmonUserToken
    {
        //根据手机号商品是否收藏Getproduct_sc_search_bymobile_json
        public string flag { get; set; }
        public string hotelcode { get; set; }
        public string onsalecode { get; set; }
        public string mobile { get; set; }
    }
}
