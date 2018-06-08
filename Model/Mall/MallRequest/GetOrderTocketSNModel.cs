using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.ApiResultModel;
using KS.Model.Ticket.TicketRequest;
using KS.Model.Ticket.TicketResponse;

namespace KS.Model.Mall.MallRequest
{
    //商城下单使用后台绑定的优惠券

    public class GetOrderTocketSNModel
    {
        public string hotelcode { get; set; }
        public string ordercode { get; set; }
        public QueryticketNewNowxBDLYModel QueryticketNewNowxBDLY { get; set; }
    }
    public class returnData
    {
        public string ticket { get; set; }
        public string ticketsn { get; set; }
        public string fmoney { get; set; }
        public string CategoryNamebm { get; set; }
    }
    public class rtData
    {
        public string isst { get; set; }
        public List<returnData> returnData { get; set; }
        public List<QueryOrderModel> QueryOrder { get; set; }
    }
}
