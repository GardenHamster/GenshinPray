using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.VO
{
    public class LuckRankingVO
    {
        public List<RareRankingVO> Star5Ranking { get; set; }

        public List<RareRankingVO> Star4Ranking { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CacheDate { get; set; }

        public int Top { get; set; }

        public int CountDay { get; set; }
    }
}
