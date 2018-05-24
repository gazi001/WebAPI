using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetActivityModel:ConmmonUserToken
    {
        //获取活动Gethd_json
        public string flag { get; set; }
        public string hotelcode { get; set; }
        public string key { get; set; }
    }
}
