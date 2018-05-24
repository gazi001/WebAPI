using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallRequest
{
    public class GetClassModel:ConmmonUserToken
    {
        //获取所有分类Gettype_json
        public string hotelcode { get; set; }
        public string type { get; set; }
    }
}
