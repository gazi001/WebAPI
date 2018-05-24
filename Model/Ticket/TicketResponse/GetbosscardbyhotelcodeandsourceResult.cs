using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketResponse
{
    public class GetbosscardbyhotelcodeandsourceResult
    {
        public List<Getbosscardbyhotelcodeandsource> success { get; set; }
    }
    public class Getbosscardbyhotelcodeandsource
    {
        /// <summary>
        /// KS100099
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// VZ100099
        /// </summary>
        public string cardno { get; set; }
        /// <summary>
        /// 13575794490
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 330621198904275945
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// VIP
        /// </summary>
        public string usertype { get; set; }
        /// <summary>
        /// SSJDJTG
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 2016/10/31 17:04:08
        /// </summary>
        public DateTime addtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalscore { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xfscore { get; set; }
        /// <summary>
        /// 1900/1/1 0:00:00
        /// </summary>
        public string birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string hy_sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tjcode { get; set; }
        /// <summary>
        /// 89
        /// </summary>
        public string id { get; set; }
    }
}
