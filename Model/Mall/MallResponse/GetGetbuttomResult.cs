using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
   public class GetGetbuttomResult
    {
       public List<ButtomInfo> success { get; set; }
    }
    public class ButtomInfo
    {
         /// <summary>
    /// 1
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 1
    /// </summary>
    public string buttomshow { get; set; }
    /// <summary>
    /// 0571-86070575
    /// </summary>
    public string hotline { get; set; }
    /// <summary>
    /// 0
    /// </summary>
    public string hotlineshow { get; set; }
    /// <summary>
    /// 0
    /// </summary>
    public string technologyshow { get; set; }
    /// <summary>
    /// 0571-86070575
    /// </summary>
    public string serviceline { get; set; }
    /// <summary>
    /// 1
    /// </summary>
    public string serviceshow { get; set; }
    /// <summary>
    /// 浙江省杭州市滨江区聚业路26号金绣国际科技中心
    /// </summary>
    public string address { get; set; }
    /// <summary>
    /// 30.185190,120.215990
    /// </summary>
    public string ll { get; set; }
    /// <summary>
    /// KS-PHP-004
    /// </summary>
    public string oid { get; set; }
    /// <summary>
    /// 1
    /// </summary>
    public string allnum { get; set; }
    }
}
