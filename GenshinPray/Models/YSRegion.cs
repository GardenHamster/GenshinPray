using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class YSRegion<T>
    {
        public int StartRegion { get; set; }

        public int EndRegion { get; set; }

        public T Item { get; set; }

        public YSRegion(T item, int startRegion, int endRegion)
        {
            this.Item = item;
            this.StartRegion = startRegion;
            this.EndRegion = endRegion;
        }
    }

}
