using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
   public class GetConsumeModel
    {
       public string starttime { get; set; }
       public string endtime { get; set; }
       public string hotelcode { get; set; }
       public string scene { get; set; }
       public string type { get; set; }
       public string sflag { get; set; }
    }
}
