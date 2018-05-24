using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    public class GetFormulaInfoByFormulaidModel:ConmmonUserToken
    {
        //GetFormula_info_byformulaid_json
        public string formulaid { get; set; }
        public string hotelcode { get; set; }
    }
}
