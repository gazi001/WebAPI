using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.ApiResultModel
{
  public  class GetServiceListModel
    {
        /// <summary>
        /// 200
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 获取成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        public List<Data> data { get; set; }
    }
    public class Data
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// oypMdv5cK1i3a2xOKpnYGs4L1x2E
        /// </summary>
        public string openid1 { get; set; }
        /// <summary>
        /// KSHZ
        /// </summary>
        public string hotelid { get; set; }
        /// <summary>
        /// 15658105525
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 朱
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 2017-11-09 09:36:32
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public int type { get; set; }
    }

  

}
