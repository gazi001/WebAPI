using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetorderByonsalecodeModel:ConmmonUserToken
    {
        public string onsalecode { get; set; }
        public string jgxz { get; set; }
        public string sysj { get; set; }
        public string hotelcode { get; set; }
        public string mobile { get; set; }
    }
}
