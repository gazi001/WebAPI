using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLL.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        public string Index()
        {
            return "Hello World";
        }
      
    }
}
