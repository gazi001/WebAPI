using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    /// <summary>
    /// 新增收藏并查询是否会员
    /// </summary>
   public class SetCollectionAndGetBossCard:ConmmonUserToken
    {
        public string openid { get; set; }
        public string hotelcode { get; set; }
        public string onsalecode { get; set; }
    }
}
