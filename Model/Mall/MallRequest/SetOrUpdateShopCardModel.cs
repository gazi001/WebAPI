using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class SetOrUpdateShopCardModel
    {
        //购物车添加整合接口
        public SetbuyProductSingleModel SetbuyProductSingle { get; set; }
        public SetbuyproductModel Setbuyproduct { get; set; }
    }
}
