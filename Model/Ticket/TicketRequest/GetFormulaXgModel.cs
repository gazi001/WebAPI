using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    //产品限购查询，判断是否购买上限Get_formula_xg_json
    public class GetFormulaXgModel:ConmmonUserToken
    {
        public string hotelcode { get; set; }
        public string mobile { get; set; }
        public string formulaid { get; set; }
        public string num { get; set; }
    }
}
