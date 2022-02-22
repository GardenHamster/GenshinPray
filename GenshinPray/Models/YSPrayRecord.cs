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
        /// 拥有数量
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// 已拥有数量
        /// </summary>
        public int OwnCountBefore { get; set; }

        /// <summary>
        /// 在一次保底中消耗多少抽
        /// </summary>
        public int Cost { get; set; }


        public YSPrayRecord(YSGoodsItem goodsItem)
        {
            this.GoodsItem = goodsItem;
        }



    }
}
