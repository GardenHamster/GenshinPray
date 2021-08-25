using GenshinPray.Common;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    /// <summary>
    /// 补给项目
    /// </summary>
    public class YSGoodsItem
    {
        /// <summary>
        /// 概率(百分比)
        /// </summary>
        public decimal Probability { get; set; }

        /// <summary>
        /// 补给项目名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 稀有类型
        /// </summary>
        public YSRareType RareType { get; set; }

        /// <summary>
        /// 物品类型(角色/武器)
        /// </summary>
        public YSGoodsType GoodsType { get; set; }

        /// <summary>
        /// 物品子类型(详细类型)
        /// </summary>
        public YSGoodsSubType GoodsSubType { get; set; }


        public YSGoodsItem()
        {
            this.Probability = SiteConfig.DefaultPR;
        }

        public YSGoodsItem(decimal probability, YSGoodsType goodsType, YSRareType rareType, string goodsName)
        {
            this.RareType = rareType;
            this.Probability = probability;
            this.GoodsName = goodsName;
            this.GoodsType = goodsType;
        }

    }
}
