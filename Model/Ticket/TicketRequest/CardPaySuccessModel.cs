using KS.Model.Mall.MallRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    public class CardPaySuccessModel : ConmmonUserToken
    {
        public SetOrderModel orderInfo { get; set; }
        public string notify { get; set; }
        public SetNewHyJsonModel setnewhy { get;set; }
    }
}
