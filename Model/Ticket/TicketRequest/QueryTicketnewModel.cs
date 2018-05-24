using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KS.Model.Mall.MallRequest;

namespace KS.Model.Ticket.TicketRequest
{
    //查询可用优惠券Queryticketnew_json
    public class QueryTicketnewModel : ConmmonUserToken
    {
        public string hotelGroupId { get; set; }
        public string hotelcode { get; set; }
        public string mode { get; set; }
        public string code { get; set; }
        //批量核销用到的参数
        public string addname { get; set; }
        public string gdcode { get; set; }
        public string xfcode { get; set; }
        public string hotelcodenew { get; set; }
    }
}
