using KS.Model.Ticket.TicketRequest;
using KS.Ticket.SDK.AdvancedAPIs;
using Newtonsoft.Json;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLL.WebApi.Controllers
{
    public class LeagueTicketController : BaseController
    {
        //
        // GET: /LeagueTicket/
        private JsonReturn jsonResult = new JsonReturn();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]//定义产品
        public JsonResult CreateFormula()
        {
            try
            {
                var sr = new StreamReader(Request.InputStream);
                var stream = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<CreateFormulaData>(stream);
                jsonResult = LeagueTicketApi.CreateFormula(data);
            }
            catch (Exception ex)
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = ex.ToString();
            }
            return this.MyJson(jsonResult);
        }
        [HttpPost]//修改
        public JsonResult UpdateFormula()
        {
            try
            {
                var sr = new StreamReader(Request.InputStream);
                var stream = sr.ReadToEnd();
                var data = JsonConvert.DeserializeObject<UpdateFormulaData>(stream);
                jsonResult = LeagueTicketApi.UpdateFormula(data);
            }
            catch (Exception ex)
            {
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = ex.ToString();
            }
            return this.MyJson(jsonResult);
        }
    }
}
