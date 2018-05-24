using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetProductImgModel:ConmmonUserToken
    {
        //获取单个商品轮播图Getproduct_img_json
        public string flag { get; set; }
        public string onsalecode { get; set; }
    }
}
