using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Converters;
using System.Net;
using System.IO;

namespace RM.Common.DotNetHttp
{
  public  class HttpHepler
    {
      /// <summary>
      /// 获取Get请求参数
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string Get(string name)
      {
          string value = HttpContext.Current.Request.QueryString[name];
          return value == null ? string.Empty : value.Trim();
      }
      /// <summary>
      /// 获取post请求参数
      /// </summary>
      /// <param name="name"></param>
      /// <returns></returns>
      public static string GetPost(string name)
      {
          string value = HttpContext.Current.Request.Form[name];
          //string value = HttpContext.Current.Request.QueryString[name];
          return value == null ? string.Empty : value.Trim();
      }
      /// <summary>
      /// 返回接口调用结果
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="jsonEntity"></param>
      /// <param name="context"></param>
      public static void ReturnJson<T>(T jsonEntity,HttpContext context)
      {
          context.Response.AddHeader("Access-Control-Allow-Origin", "*");
          var jSetting = new JsonSerializerSettings { 
              NullValueHandling = NullValueHandling.Ignore ,
              DateFormatString = "yyyy-MM-dd HH:mm:ss",
          };
          var jsonReult = JsonConvert.SerializeObject(jsonEntity, Formatting.Indented, jSetting);
          context.Response.Write(jsonReult);
      }
      /// <summary>
      /// post请求(键值对)
      /// </summary>
      /// <param name="SubmitUrl"></param>
      /// <param name="PostData"></param>
      /// <returns></returns>
      public static string SendPost(string SubmitUrl, string PostData)
      {
          try
          {
              byte[] postData = Encoding.UTF8.GetBytes(PostData);
              HttpWebRequest request = WebRequest.Create(SubmitUrl) as HttpWebRequest;
              request.Method = "POST";
              request.KeepAlive = false;
              request.AllowAutoRedirect = true;
              request.ContentType = "application/x-www-form-urlencoded";
              request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR  3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
              request.ContentLength = postData.Length;

              System.IO.Stream outputStream = request.GetRequestStream();
              outputStream.Write(postData, 0, postData.Length);
              outputStream.Close();

              HttpWebResponse response;
              Stream responseStream;
              StreamReader reader;
              string srcString;
              response = request.GetResponse() as HttpWebResponse;
              responseStream = response.GetResponseStream();
              reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
              srcString = reader.ReadToEnd();
              string result = srcString;
              reader.Close();
              return result;
          }
          catch
          {
              return "ERROR_404";
          }
      }
      /// <summary>
      /// po
      /// </summary>
      /// <param name="SubmitUrl"></param>
      /// <param name="PostData"></param>
      /// <returns></returns>
      public static string SendPostJson(string SubmitUrl, string PostData)
      {
          HttpWebRequest request = (HttpWebRequest)WebRequest.Create(SubmitUrl);
          request.Method = "POST";
          request.ContentType = "application/json";
          request.ContentLength = Encoding.UTF8.GetByteCount(PostData);
          Stream myRequestStream = request.GetRequestStream();
          StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
          myStreamWriter.Write(PostData);
          myStreamWriter.Close();
          HttpWebResponse response = (HttpWebResponse)request.GetResponse();
          Stream myResponseStream = response.GetResponseStream();
          StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
          string retString = myStreamReader.ReadToEnd();
          myStreamReader.Close();
          myResponseStream.Close();
          return retString;
      }
    }
}
