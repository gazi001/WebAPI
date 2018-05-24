using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class SetProductScoreModel : ConmmonUserToken
    {
        ////新增评价
        public string pcode { get; set; }
        public string score { get; set; }
        public string remark { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string mobile { get; set; }
        public string oid { get; set; }      
    }
}
