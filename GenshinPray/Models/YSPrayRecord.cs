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

        public YSPrayRecord(YSGoodsItem goodsItem)
        {
            this.GoodsItem = goodsItem;
        }

    }
}
