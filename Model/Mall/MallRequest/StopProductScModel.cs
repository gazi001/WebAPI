using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class StopProductScModel:ConmmonUserToken
    {
        //取消收藏Stopproduct_sc_json
        public string hotelcode { get; set; }
        public string mobile { get; set; }
        public string onsalecode { get; set; }
        public string flag { get; set; }
    }
}
