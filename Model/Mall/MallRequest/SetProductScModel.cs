using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class SetProductScModel:ConmmonUserToken
    {
        //新增收藏Setproduct_sc_json
        public string bosscard { get; set; }
        public string useraccount { get; set; }
        public string hotelcode { get; set; }
        public string mobile { get; set; }
        public string onsalecode { get; set; }
        public string oid { get; set; }
    }
}
