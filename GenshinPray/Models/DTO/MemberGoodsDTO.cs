using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.DTO
{
    public class MemberGoodsDTO
    {
        public string GoodsName { get; set; }

        public int Count { get; set; }

        public YSGoodsType GoodsType { get; set; }

        public YSRareType RareType { get; set; }

    }
}
