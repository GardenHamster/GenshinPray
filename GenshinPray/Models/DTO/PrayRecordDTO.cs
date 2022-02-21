using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.DTO
{
    public class PrayRecordDTO
    {
        public int GoodsId { get; set; }

        public string GoodsName { get; set; }

        public YSGoodsType GoodsType { get; set; }

        public YSGoodsSubType GoodsSubType { get; set; }

        public YSRareType RareType { get; set; }

        public YSPondType PondType { get; set; }

        public int Cost { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
