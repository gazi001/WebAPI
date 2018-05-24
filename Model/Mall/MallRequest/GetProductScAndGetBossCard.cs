using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{

    /// <summary>
    /// 合并收藏列表和Getbosscard
    /// </summary>
    public class GetProductScAndGetBossCard:ConmmonUserToken
    {
        public string openid { get; set; }
        public string hotelcode { get; set; }
        
    }
}
