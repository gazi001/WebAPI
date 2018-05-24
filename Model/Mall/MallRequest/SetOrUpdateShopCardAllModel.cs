using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class SetOrUpdateShopCardAllModel
    {
        //购物车整合接口
        public SetbuyproductModel Setbuyproduct{ get; set; }
        public SetbuyProductSingleModel SetbuyProductSingle { get; set; }
    }
}
