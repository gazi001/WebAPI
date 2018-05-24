using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //商品评论Getproduct_score_json
    public class GetProductScoreModel:ConmmonUserToken
    {
        public string onsalecode { get; set; }
        public string hotelcode { get; set; }
    }
}
