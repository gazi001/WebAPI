using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    //收藏列表Getproduct_sc_search_json
    public class GetProductScSearchModel:ConmmonUserToken
    {
        public string hotelcode { get; set; }
        public string bosscard { get; set; }
        public string useraccount { get; set; }
        public string mobile { get; set; }
        public string flag { get; set; }
    }
}
