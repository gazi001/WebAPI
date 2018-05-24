using RM.Common.DotNetCode;
using RM.Common.DotNetHttp;
using RM.Common.DotNetModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BLL.WebApi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected LogHelper Logger = new LogHelper("ErrorLog");
        protected void Application_Error(Object sender, EventArgs e)
        {
            JsonReturn jsonResult = new JsonReturn();
            Exception lastError = Server.GetLastError();
            if (lastError != null)
            {
                //异常信息
                string strExceptionMessage = string.Empty;

                //对HTTP 404做额外处理，其他错误全部当成500服务器错误
                HttpException httpError = lastError as HttpException;
                if (httpError != null)
                {
                    //获取错误代码
                    int httpCode = httpError.GetHttpCode();
                    strExceptionMessage = httpError.Message;
                    if (httpCode == 400 || httpCode == 404)
                    {
                        Response.StatusCode = 404;
                        //跳转到指定的静态404信息页面，根据需求自己更改URL
                       // Response.WriteFile("~/HttpError/404.html");
                        jsonResult.code = ApiCode.程序异常;
                        jsonResult.msg = "找不到相关接口";
                        
                        Server.ClearError();
                        return;
                    }
                }
                strExceptionMessage = lastError.Message;

                /*-----------------------------------------------------
                 * 此处代码可根据需求进行日志记录，或者处理其他业务流程
                 * ---------------------------------------------------*/

                this.Logger.WriteLog(string.Concat(new string[]
				{
					"----------- 记录程序日志 Log-----------\r\n",
					strExceptionMessage,
					"\r\n",
                    "错误地址：\r\n",
                     Request.Url.ToString(),
                     "\r\n",

				}));

                /*
                 * 跳转到指定的http 500错误信息页面
                 * 跳转到静态页面一定要用Response.WriteFile方法                 
                 */
                Response.StatusCode = 500;
               // Response.WriteFile("~/HttpError/500.html");
                jsonResult.code = ApiCode.程序异常;
                jsonResult.msg = strExceptionMessage;
                HttpHepler.ReturnJson<JsonReturn>(jsonResult, HttpContext.Current);
                //一定要调用Server.ClearError()否则会触发错误详情页（就是黄页）
                Server.ClearError();
                //Server.Transfer("~/HttpError/500.aspx");
            }
        }
    }
}