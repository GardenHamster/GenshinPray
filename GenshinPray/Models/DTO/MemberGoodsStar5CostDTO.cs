using GenshinPray.Type;
using System;

namespace GenshinPray.Models.DTO
{
    public class MemberGoodsStar5CostDTO
    {
        public string GoodsName { get; set; }

        public YSGoodsType GoodsType { get; set; }
        public YSGoodsSubType GoodsSubType { get; set; }

        public YSRareType RareType { get; set; }
        public int Cost { get; set; }
        public DateTime CreateDate { get; set; }
    }
}