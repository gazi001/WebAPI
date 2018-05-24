using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Common.CommonRequest
{
  public   class GetOpenIdForWxOpenModel
    {
      public string appid { get; set; }
      public string secret { get; set; }
      public string code { get; set; }

    }
}
