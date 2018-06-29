using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
  public  class GetAuthorizedListModel
    {
        public string hotelcode { get; set; }
        public DateTime  starttime { get; set; }
        public DateTime endtime { get; set; }

    }
}
