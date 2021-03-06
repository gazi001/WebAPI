﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //根据手机号查询该会员所有订单
    public class GetOrderByMobileResult
    {
        public List<GetOrderByMobileItem> success { get; set; }
        public string returncode { get; set; }
    }
    public class GetOrderByMobileItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// zhu+测试
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zipcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordercode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addtime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string addname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string useraccount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paycode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paymoney { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ispay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string thfs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sessionid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string onsalecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string totalprice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string jgxz { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sysj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string linkticketsn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// 测试 
        /// </summary>
        public string pname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fmoney { get; set; }
    }
  
}
