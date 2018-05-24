using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //根据手机号查询该会员所有订单
    public class GetOrderByMobileModel : ConmmonUserToken
    {
        public string mobile { get; set; }
        public string hotelcode { get; set; }
      
    }
}
