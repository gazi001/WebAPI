using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    public class MemberBalanceModel
    {
        public string user { get; set; }
        public string token { get; set; }
        public string hotelGroupId { get; set; }
        public string hotelcode { get; set; }
        public string mode { get; set; }
        public string code { get; set; }
    }
}
