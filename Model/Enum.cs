using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model
{
    public enum StatusCode
    {
        成功=0,
        微信订单号不存在=404001,
        参数不全=404002,
        程序异常=500001,
    }
}
