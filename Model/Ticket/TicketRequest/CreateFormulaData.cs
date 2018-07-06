using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KS.Model.Ticket.TicketRequest
{
   public class CreateFormulaData
    {
       public string usercode { get; set; }
        public string type { get; set; }
        public string sflag { get; set; }
        public string pro_arr { get; set; }
        public List<CreateCategoryData> arr { get; set; }
        public List<string> data { get; set; }
        public string pro_name { get; set; }
        public string price { get; set; }
        public string summary { get; set; }
        public string pro_num { get; set; }
        [Required(ErrorMessage = "酒店代码不能为空")]
        public string hotelcode { get; set; }
        public string hdcode { get; set; }
        public string hotelcode_fw { get; set; }
     
    }
}
