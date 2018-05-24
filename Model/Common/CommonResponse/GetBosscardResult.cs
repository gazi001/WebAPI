using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace KS.Model.Mall.MallResponse
{
    public class GetBosscardResult
    {
        /// <summary>
        /// 
        /// </summary>
     
        public string returncode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bosscard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cardno { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string idno { get; set; }
        /// <summary>
        /// Retrospection丶
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string usertype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hy_img { get; set; }
    }
}
