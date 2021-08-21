using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSSupplyRecord
    {
        /// <summary>
        /// 获得补给项目
        /// </summary>
        public YSSupplyItem SupplyItem { get; set; }

        public YSSupplyRecord() { }

        public YSSupplyRecord(YSSupplyItem supplyItem)
        {
            this.SupplyItem = supplyItem;
        }

    }
}
