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

        public int GoodsType { get; set; }

        public int RareType { get; set; }

        public DateTime CreateDate { get; set; }

    }
}
