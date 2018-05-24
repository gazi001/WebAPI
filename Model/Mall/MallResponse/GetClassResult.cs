using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //获取所有分类Gettype_json返回类
    public class ClassSuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 权益
        /// </summary>
        public string typename { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string typecode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hotelcode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string allnum { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img { get; set; }
    }
    public class GetClassResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ClassSuccessItem> success { get; set; }
        public string returncode { get; set; }
    }
}
