using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
   public class AuthorizedLogModel
    {
        public int id { get; set; }
        public int formulaid { get; set; }
        public string hotelcode { get; set; }
        public string roomcode { get; set; }
        public string admin { get; set; }
        public string mobile { get; set; }
        public string returncode { get; set; }
        public string tp_id { get; set; }
        public int status { get; set; }
    }
}
