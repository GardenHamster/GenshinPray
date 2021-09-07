using GenshinPray.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSUpItem
    {
        /// <summary>
        /// 5星列表
        /// </summary>
        public List<YSGoodsItem> Star5AllList { get; set; }

        /// <summary>
        /// 4星列表
        /// </summary>
        public List<YSGoodsItem> Star4AllList { get; set; }

        /// <summary>
        /// 3星列表
        /// </summary>
        public List<YSGoodsItem> Star3AllList { get; set; }

        /// <summary>
        /// 5星角色up
        /// </summary>
        public List<YSGoodsItem> Star5UpList { get; set; }

        /// <summary>
        /// 4星角色up
        /// </summary>
        public List<YSGoodsItem> Star4UpList { get; set; }

        /// <summary>
        /// 5星角色非up
        /// </summary>
        public List<YSGoodsItem> Star5NonUpList { get; set; }

        /// <summary>
        /// 4星角色非up
        /// </summary>
        public List<YSGoodsItem> Star4NonUpList { get; set; }

        
        

    }
}
