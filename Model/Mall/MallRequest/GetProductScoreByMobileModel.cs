using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetProductScoreByMobileModel:ConmmonUserToken
    {
        //Getproduct_score_bymobile_json根据手机号获取所有评价
        public string hotelcode { get; set; }
        public string pcode { get; set; }
        public string mobile { get; set; }
    }
}
