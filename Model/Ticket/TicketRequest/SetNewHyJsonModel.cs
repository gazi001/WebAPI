using KS.Model.Mall.MallRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    /// <summary>
    /// 发产品
    /// </summary>
   public class SetNewHyJsonModel : ConmmonUserToken
    {

        public string hotelcode { get; set; }
        public string FormulaId { get; set; }
        public string mobile { get; set; }
        public string salehotelcode { get; set; }
    }
}
