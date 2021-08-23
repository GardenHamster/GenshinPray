using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class PrayResult
    {
        public int Cost { get; set; }

        public List<GoodsPO> Goods { get; set; }

        public string ImgUrl { get; set; }
    }
}
