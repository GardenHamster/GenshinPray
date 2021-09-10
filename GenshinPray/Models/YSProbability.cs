using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSProbability
    {
        /// <summary>
        /// 概率类型
        /// </summary>
        public YSProbabilityType ProbabilityType { get; set; }

        /// <summary>
        /// 概率(百分比)
        /// </summary>
        public decimal Probability { get; set; }


        public YSProbability(decimal probability, YSProbabilityType probabilityType)
        {
            this.ProbabilityType = probabilityType;
            this.Probability = probability;
        }


    }
}
