using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{//核销接口
    public class TicketJLAddTicketSNNewSetModel : ConmmonUserToken
    {
        public string ticketsn { get; set; }
        public string resno { get; set; }
        public string oid { get; set; }
        public string tmoney { get; set; }
        public string gdcode { get; set; }
        public string hotelcode { get; set; }

       

        public string remark { get; set; }
        public string state { get; set; }
        public string paymoney { get; set; }
        public string ispay { get; set; }
    }
}
