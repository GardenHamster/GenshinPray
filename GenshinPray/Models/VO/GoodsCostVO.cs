using System;

namespace GenshinPray.Models.VO
{
    public class GoodsCostVO
    {
        public string GoodsName { get; set; }

        public string GoodsType { get; set; }

        public string GoodsSubType { get; set; }

        public string RareType { get; set; }

        public int Cost { get; set; }
        public DateTime Datetime { get; set; }

    }
}