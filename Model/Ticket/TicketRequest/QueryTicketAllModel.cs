using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    public class QueryTicketAllModel : ConmmonUserToken
    {
        public string hotelgroupid { get; set; }
        public string hotelcode { get; set; }
        public string mode { get; set; }
        public string code { get; set; }
    }
}
