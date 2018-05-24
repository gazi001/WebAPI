using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Common.SDK
{
    public class CommonFunction
    {
        //王师傅的接口外头有两个圆括号
        public static string Replacebracket(string str)
        {
            str = str.Replace("(", "").Replace(")", "");
            return str;
        }
    }
}
