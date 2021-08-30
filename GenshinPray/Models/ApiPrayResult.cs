using GenshinPray.Models.PO;
using GenshinPray.Models.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class ApiPrayResult
    {
        /// <summary>
        /// 祈愿次数
        /// </summary>
        public int PrayCount { get; set; }

        /// <summary>
        /// 剩余多少抽五星大保底
        /// </summary>
        public int Surplus180 { get; set; }

        /// <summary>
        /// 剩余多少抽五星小保底
        /// </summary>
        public int Surplus90 { get; set; }

        /// <summary>
        /// 获得5星物品时累计消耗多少抽
        /// </summary>
        public int Star5Cost { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgHttpUrl { get; set; }

        /// <summary>
        /// 图片相对路径
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// base64
        /// </summary>
        public string ImgBase64 { get; set; }

        /// <summary>
        /// 获得的物品列表
        /// </summary>
        public List<GoodsVO> Goods { get; set; }

        /// <summary>
        /// 获得的5星物品列表
        /// </summary>
        public List<GoodsVO> Star5Goods { get; set; }

        /// <summary>
        /// 获得的4星物品列表
        /// </summary>
        public List<GoodsVO> Star4Goods { get; set; }

    }
}
