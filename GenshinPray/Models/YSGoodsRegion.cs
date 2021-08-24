using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSGoodsRegion
    {
        public int StartRegion { get; set; }

        public int EndRegion { get; set; }

        public YSGoodsItem GoodsItem { get; set; }

        public YSGoodsRegion(YSGoodsItem goodsItem, int startRegion, int endRegion)
        {
            this.StartRegion = startRegion;
            this.EndRegion = endRegion;
            this.GoodsItem = goodsItem;
        }
    }

}
