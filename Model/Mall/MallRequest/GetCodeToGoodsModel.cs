using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetCodeToGoodsModel:ConmmonUserToken
    {
        //根据活动code获取商品Getproduct_onsale_byhdcode_json
        public string activitycode { get; set; }
        public string hotelcode { get; set; }
    }
}
