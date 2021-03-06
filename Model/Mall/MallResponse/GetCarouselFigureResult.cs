﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS.Model.Mall.MallResponse
{
    //获取轮播图Getlp_img_json返回类
    public class CarouselFigure
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 测试1
        /// </summary>
        public string imgname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string httpurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string imgpath { get; set; }
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
    }

  

    /// <summary>
    /// 轮播图数据返回值
    /// </summary>
    public class GetCarouselFigureResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<CarouselFigure> success { get; set; }
        public string returncode { get; set; }
    }
}
