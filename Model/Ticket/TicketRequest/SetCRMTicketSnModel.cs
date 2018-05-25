using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
   public class SetCRMTicketSnModel
    {
        //ticket_create_new_json
       public string user { get; set; }
       public string token { get; set; }
       public string id { get; set; }
       public string hotelcode { get; set; }
       public int num { get; set; }
       public string FormulaId { get; set; }

        //ticket_tp_create_json
       public string salemode { get; set; }
       public decimal tmoney { get; set; }
       public string oid { get; set; }
       public decimal kpmoney { get; set; }

        //ticket_tpdata_pl_create_json
       public string sessionid { get; set; }

        //SetCRM_ticketsn_pl_json
       public string usertype { get; set; }
       public string bosscard { get; set; }
       public string name { get; set; }
       public string sjhm { get; set; }
       public string birth { get; set; }
       public string ident { get; set; }
       public string cardhotel { get; set; }
       public string sno { get; set; }
       public string saleid { get; set; }
       public string salemodeid { get; set; }
       public string OperatorId { get; set; }
    }
}
