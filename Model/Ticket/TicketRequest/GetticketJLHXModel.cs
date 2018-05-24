using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    public class GetticketJLHXModel : ConmmonUserToken
    {
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string hotelcode { get; set; }
        public string type { get; set; }//区分年1月2周3日4
    }
}
