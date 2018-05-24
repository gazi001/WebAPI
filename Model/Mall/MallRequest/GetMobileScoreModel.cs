using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //评论整合接口
    public class GetMobileScoreModel:ConmmonUserToken
    {
        public string pcode { get; set; }
        public string score { get; set; }
        public string remark { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string mobile { get; set; }
        public string oid { get; set; }

        public string ordercode { get; set; }
        public string ispay { get; set; }
        public string state { get; set; }
        public string paymoney { get; set; }
        public string orderremark { get; set; }

    }
}
