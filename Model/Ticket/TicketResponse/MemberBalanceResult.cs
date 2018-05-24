using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketResponse
{
   public class MemberBalanceResult
    {
        /// <summary>
        /// success
        /// </summary>
        public string returncode { get; set; }
        /// <summary>
        /// SS101487
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// 赵正豪
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 18989841642
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 10.00
        /// </summary>
        public decimal tmoney { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public decimal tmoney_dj { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public decimal tmoney_xf { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public decimal tmoney_gq { get; set; }
        /// <summary>
        /// 10.00
        /// </summary>
        public decimal tmoney_kp { get; set; }
        /// <summary>
        /// 9
        /// </summary>
        public int yhqnum { get; set; }
        public string realbosscard { get; set; }
    }
}
