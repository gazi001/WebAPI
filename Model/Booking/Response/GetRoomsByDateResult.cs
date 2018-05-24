using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KS.Model.Booking.Response
{
   public class GetRoomsByDateResult
    {
       public string code { get; set; }
       public string msg { get; set; }
       public List<info> data  { get; set; }

    }
   public class info
   {
       [JsonIgnore]
       public string pic { get; set; }
       public string roomtype { get; set; }
       public string roomname { get; set; }
       public string price { get; set; }
       public Nullable<decimal> minprice { get; set; }
       [JsonIgnore]
       public List<xz> xz { get; set; }
       // public List<rateroom_xz> xz { get; set; }
   }
   public class xz
   {
       public string xz_name { get; set; }
       public string xz_code { get; set; }
       public Nullable<decimal> price { get; set; }
       public Nullable<int> package { get; set; }
   }
}
