using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
  public  class UpdateConsumeLogModel
    {
      public int id { get; set; }
      public int num { get; set; }
      public decimal rate { get; set; }
      public string xfcode { get; set; }
    }
}
