using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Ticket.TicketRequest
{
    public class UpdateFormulaData
    {

        /// <summary>
        /// KS-PHP-001
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// KSHZ
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// youhui
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// ks-cs2,ks-cs,KSHZ,
        /// </summary>
        public string pro_arr { get; set; }
        /// <summary>
        /// Data_old_val
        /// </summary>
        public List<string> data_old_val { get; set; }
        /// <summary>
        /// Data_old_key
        /// </summary>
        public List<string> data_old_key { get; set; }
        /// <summary>
        /// Data_new
        /// </summary>
        public List<string> data_new { get; set; }
        /// <summary>
        /// Arr
        /// </summary>
        public List<CreateCategoryData> arr { get; set; }
        /// <summary>
        /// 测试产品优惠卡
        /// </summary>
        public string pro_name { get; set; }
        /// <summary>
        /// 12
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 11
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 12
        /// </summary>
        public string pro_num { get; set; }
    }
}
