using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSPrayRecord
    {
        /// <summary>
        /// 获得补给项目
        /// </summary>
        public YSGoodsItem GoodsItem { get; set; }

        /// <summary>
        /// 随机区域
        /// </summary>
        public int RandomRegion { get; set; }

        public YSPrayRecord(YSGoodsItem goodsItem, int randomRegion)
        {
            this.RandomRegion = randomRegion;
            this.GoodsItem = goodsItem;
        }

    }
}
