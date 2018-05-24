using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
   public class SetProductOnSaleSingleSaleNumModel:ConmmonUserToken
    {
       public string onsalecode { get; set; }
       public string salenum { get; set; }
    }
}
