using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    /// <summary>
    /// 获取首页商品列表并且获取商品下的规格
    /// </summary>
   public  class GetIndexProductList
    {
        public GoodsSuccessItem goodItem { get; set; }
        public List<GetProductPriceSuccessItem> priceList { get; set; }
    }
}
