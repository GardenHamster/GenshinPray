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
