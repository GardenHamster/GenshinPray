using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSSupplyRegion
    {
        public int StartRegion { get; set; }

        public int EndRegion { get; set; }

        public YSSupplyItem SupplyItem { get; set; }

        public YSSupplyRegion(YSSupplyItem supplyItem, int startRegion, int endRegion)
        {
            this.StartRegion = startRegion;
            this.EndRegion = endRegion;
            this.SupplyItem = supplyItem;
        }
    }

}
