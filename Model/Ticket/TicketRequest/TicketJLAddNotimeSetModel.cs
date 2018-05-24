using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    //扣款接受类
    public class TicketJLAddNotimeSetModel
    {
        public string no { get; set; }//卡号
        public string usercode { get; set; }//工号
        public string pwd { get; set; }//密码
        public string ticketsn { get; set; }//票券号
        public string CategoryId { get; set; }//卡号
        public string FormulaId { get; set; }//产品包id
        public string Tp_Id { get; set; }//套票号
        public string usertype { get; set; }//卡号
        public string hotelcode { get; set; }//消费酒店
        public string deptcode { get; set; }//卡号
        public string gdcode { get; set; }//岗点
        public string addname { get; set; }//操作人工号
        public string fmoney { get; set; }//扣款金额
        public string tmoney { get; set; }//扣款金额
        public string xfcode { get; set; }//消费code,订单号
        public string xftype { get; set; }//消费类型
        public string hotelcodenew { get; set; }//区分查哪个库
    }
}
