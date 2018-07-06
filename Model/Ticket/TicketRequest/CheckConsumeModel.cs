using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    public class CheckConsumeModel
    {
        public string xfcode { get; set; }
        public string hotelcode { get; set; }
        public decimal rate { get; set; }
        public int num { get; set; }
        public string  time { get; set; }
    }
}
