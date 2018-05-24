using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    /// <summary>
    /// 批量删除购物车
    /// </summary>
   public class DelCarModel:ConmmonUserToken
    {
       public string flag { get; set; }
       public List<string> list { get; set; }
    }
}
