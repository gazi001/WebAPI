using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Common.DotNetModel
{
    public class JsonReturn
    {
        /// <summary>
        /// 接口状态返回值
        /// </summary>
        public ApiCode code { get; set; }                                   
        /// <summary>
        /// 接口状态返回信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 接口返回数据
        /// </summary>
        public object data { get; set; }
    }
}
