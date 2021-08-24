using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("goods")]
    public class GoodsPO : BasePO
    {
        public string GoodsName { get; set; }

        public YSGoodsType GoodsType { get; set; }

        public YSRareType RareType { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
