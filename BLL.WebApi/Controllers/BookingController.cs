using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KS.Model;
using KS.Model.Booking.Response;
using Newtonsoft.Json;
using RM.Common.DotNetHttp;
using RM.Common.DotNetModel;
using BLL.WebApi.Model;
using KS.Ticket.SDK.AdvancedAPIs;
using KS.Mall.SDK.AdvancedAPIs;

namespace BLL.WebApi.Controllers
{
    public class zzhs
    {
        public string aa { get; set; }
    }
    public class bbb
    {
        public List<zzhs> bsbb { get; set; }
    }
    public class BookingController : BaseController
    {
        //
        // GET: /Booking/
         private JsonReturn jsonResult = new JsonReturn();
        public JsonResult Index(string bb)
        {
            MallApi.Test(bb);
            return Json(new { },JsonRequestBehavior.AllowGet);
           
        }
        ///1、获取房型列表
        ///2、获取房价列表
        ///3、


        [HttpPost]
        public string GetRoomsByDate(string date,string hotelcode)
        {
            var url = "https://interface.hanibabyppo.com/Booking/API/Reserve.ashx?action=GetRoomsByDate";
            var postData = "hotelcode=" + hotelcode + "&date=" + date + "&type=0";
            var result = HttpHepler.SendPost(Config.BookingUrl + "?action=GetRoomsByDate", postData);
            var obj = JsonConvert.DeserializeObject<GetRoomsByDateResult>(result); 
            return JsonConvert.SerializeObject(obj);
        }
        [HttpPost]
        public string GetRateCodexzByRoomtype(string roomtype, string starttime, string endtime,string hotelcode)
        {
            var url = "https://interface.hanibabyppo.com/Booking/API/Reserve.ashx?action=GetRateCodexzByRoomtype";
            var postData = "hotelcode=" + hotelcode + "&starttime=" + starttime +"&roomtype="+roomtype+ "&endtime="+endtime+"&type=0";
            var result = HttpHepler.SendPost(Config.BookingUrl + "?action=GetRateCodexzByRoomtype", postData);
            if(result !="ERROR_404")
            {
               // var obj = JsonConvert.DeserializeObject<>
            }
            return HttpHepler.SendPost(url, postData);
        }

        public string test()
        {
            var db = new MemberEntities();
            var list = db.bindopenidlogs.ToList();
            foreach (var item in list)
            {
                
               var o = TicketApi.Querymember_json( "G000001","mobile", item.mobile);
               if (o[0] != null)
               {
                   item.realbosscard = o[0].realbosscard;
                   item.bosscard = o[0].CardNo;
               }
            }
            db.SaveChanges();
            return "";
        }
        
        //public string SubmitNewHotelOrder()
        //{

 
        //}
    }
}
