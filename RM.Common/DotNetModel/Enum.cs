using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.Common.DotNetModel
{
    public enum PicType
    {
        封面图 = 0,
        轮播图 = 1,
    }
    public enum UserLevel
    {
        管理员 = 0,
        普通用户 = 1,
    }
    public enum BusinessState
    {

        发布 = 0,
        下单 = 1,
        流转 = 2, //购买
        取消=3,
        核销使用 = 4,
        下架 = 5
       
       
    }
    public enum ApiCode
    {
        成功 = 200,
        没有实体数据 = 201,
        更新失败 = 202,
        接口调用失败=203,
        非会员=204,
        达到限购=205,
        订单生成失败=206,
        没有找到微信支付订单=207,
        发送产品失败=208,
        云券通限购失败=209,
        注册失败=210,
        绑定数量到达上限=211,
        参数不全=212,
        木有这个接口 = 404,
        程序异常 = 500,
        库存不足=210,
        数据正在生成=213,
        产品定义失败=214,
        子券定义失败=215,

    }
    public enum Flag
    {
        启用=0,
        停用=1,
    }
}
