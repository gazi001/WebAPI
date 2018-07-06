using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    public class BatchTransferModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string oldmobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string formulaid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string giftId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string grouptype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// Newmobilelist
        /// </summary>
        public List<Newmobilelist> newmobilelist { get; set; }
    }
    public class Newmobilelist
    {
        /// <summary>
        /// xxx
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// Count
        /// </summary>
        public int count { get; set; }
    }

    

}
