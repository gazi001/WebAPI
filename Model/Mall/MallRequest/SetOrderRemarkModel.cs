using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
   public class SetOrderRemarkModel:ConmmonUserToken
    {
        //修改订单状态为4已评价
        public string ordercode { get; set; }
        public string ispay { get; set; }
        public string state { get; set; }
        public string remark { get; set; }
        public string paymoney { get; set; }
        public string oid { get; set; }
       
    }
}
