using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    public class GetproductXgModel: ConmmonUserToken
    {
        //Getproduct_xg_json接口
        public string hotelcode { get; set; }
        public string formulaid { get; set; }
    }
}
