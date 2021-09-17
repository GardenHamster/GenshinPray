using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.DTO
{
    public class LuckRankingDTO
    {
        public int AuthId { get; set; }

        public string MemberCode { get; set; }

        public YSRareType RareType { get; set; }

        public int RareCount { get; set; }

        public int TotalPrayTimes { get; set; }

        public double RareRate { get; set; }

    }
}
