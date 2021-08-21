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
    public class YSSupplyItem
    {
        /// <summary>
        /// 概率(百分比)
        /// </summary>
        public decimal Probability { get; set; }

        /// <summary>
        /// 补给项目名
        /// </summary>
        public string SupplyName { get; set; }

        /// <summary>
        /// 稀有类型
        /// </summary>
        public YSRareType RareType { get; set; }

        /// <summary>
        /// 类型,武器/角色
        /// </summary>
        public YSGoodsType GoodsType { get; set; }

        public YSSupplyItem() { }

        public YSSupplyItem(decimal probability, YSGoodsType goodsType, YSRareType rareType, string supplyName)
        {
            this.RareType = rareType;
            this.Probability = probability;
            this.SupplyName = supplyName;
            this.GoodsType = goodsType;
        }

    }
}
