using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
   public class CreateCategoryData
    {
        /// <summary>
        /// 1,3,
        /// </summary>
        public string range { get; set; }
        /// <summary>
        /// name3
        /// </summary>
        public string cate_name { get; set; }
        /// <summary>
        /// name4
        /// </summary>
        public string cate_bm { get; set; }
        /// <summary>
        /// KF
        /// </summary>
        public string cate_code { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string cate_xz { get; set; }
        /// <summary>
        /// 1
        /// </summary>
        public string cate_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string  cate_price { get; set; }
        /// <summary>
        /// 0
        /// </summary>
        public int cate_start_date { get; set; }
        /// <summary>
        /// 99
        /// </summary>
        public int cate_end_date { get; set; }
        /// <summary>
        /// nnsss
        /// </summary>
        public string cate_summary { get; set; }

        public string cate_fapiao { get; set; }

        public string iszs { get; set; }
        public string isbool { get; set; }
    }
}
