using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class QueryticketNewNowxBDLYModel : ConmmonUserToken
    {
        public string hotelGroupId { get; set; }
        public string hotelcode { get; set; }
        public string mode { get; set; }
        public string code { get; set; }
    }
}
