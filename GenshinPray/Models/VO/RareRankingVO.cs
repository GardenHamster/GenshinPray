using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.VO
{
    public class RareRankingVO
    {
        public string MemberCode { get; set; }

        public int Count { get; set; }

        public int TotalPrayTimes { get; set; }

        public double Rate { get; set; }
    }
}
