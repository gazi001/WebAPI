using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
   public class batchConsumeTicketModel
    {
      
       public string user { get; set; }
      
       public string token { get; set; }

       public string mode { get; set; }

       public string xftype { get; set; }
       
       public string hotelGroupId { get; set; }

       public string code { get; set; }

       public decimal rate { get; set; }
        [Required(ErrorMessage = "hotelcode不能为空")]
       public string hotelcode { get; set; }
        [Required(ErrorMessage = "gdcode不能为空")]
       public string gdcode { get; set; }
        [Required(ErrorMessage = "addname不能为空")]
       public string addname { get; set; }
        [Required(ErrorMessage = "xfcode不能为空")]
       public string xfcode { get; set; }
        [Required(ErrorMessage = "hotelcodenew不能为空")]
       public string hotelcodenew { get; set; }
        //[Required(ErrorMessage = "scene不能为空")]
       public string scene { get; set; }
       [Required(ErrorMessage = "ticketsnList不能为空")]
       public string ticketsnList { get; set; }
       //public string 
    }
}
