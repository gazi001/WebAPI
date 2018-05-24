using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetProducttypeModel:ConmmonUserToken
    {
        //查询单个分类下商品Getproduct_onsale_bytype_json
        public string type { get; set; }
        public string hotelcode { get; set; }
    }
}
