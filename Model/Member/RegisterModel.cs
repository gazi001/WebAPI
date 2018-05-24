using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace KS.Model.Member
{
    public class RegisterModel
    {
        public string user { get; set; }
        public string token { get; set; }
        [Required(ErrorMessage = "mobile不能为空")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "hotelcode不能为空")]
        public string hotelcode { get; set; }
        public string hy_name { get; set; }
        public string hy_kh { get; set; }
        public string bosscard { get; set; }
        public string cardhotel { get; set; }
        public string usertype { get; set; }
        public string cardtype { get; set; }
        public string ratecode { get; set; }
        public string hy_sjhm { get; set; }
        public string hy_zjlx { get; set; }
        public string hy_zjhm { get; set; }
        public string hy_sex { get; set; }
        public string bosshk { get; set; }
        public string saleid { get; set; }
        public string arr { get; set; }
        public string hy_qq_msn { get; set; }
        public string weixinnumber { get; set; }
        public string nation { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string remark { get; set; }
        public string birthday { get; set; }
        public string hy_email { get; set; }
        public string id { get; set; }
       
        public string grouptype { get; set; }
        public string tjcode { get; set; }
        public string source { get; set; }
        public string address { get; set; }
        public string openid { get;set; }

      
    }
}
