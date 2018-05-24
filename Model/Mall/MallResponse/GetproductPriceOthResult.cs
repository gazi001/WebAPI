using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
   public class GetproductPriceOthResult
    {
       public List<GetproductPriceOthItem> success { get; set; }
    }
   public class GetproductPriceOthItem
   {
       /// <summary>
       /// 815
       /// </summary>
       public string id { get; set; }
       /// <summary>
       /// KSHZ201712292854
       /// </summary>
       public string onsalecode { get; set; }
       /// <summary>
       /// 1
       /// </summary>
       public string  jgxz { get; set; }
       /// <summary>
       /// 1
       /// </summary>
       public string  sysj { get; set; }
       /// <summary>
       /// 0.01
       /// </summary>
       public string price { get; set; }
       /// <summary>
       /// -1
       /// </summary>
       public int num { get; set; }
       /// <summary>
       /// 411
       /// </summary>
       public string linkticketsn { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string starttime { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string endtime { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string xgnum { get; set; }
       /// <summary>
       /// 
       /// </summary>
       public string buyuseticketsn { get; set; }
   }
}
