using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("pond_goods")]
    public class PondGoodsPO : BasePO
    {
        [SugarColumn(IsNullable = false, ColumnDescription = "授权码ID")]
        public int AuthId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "蛋池类型")]
        public YSPondType PondType { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "物品ID")]
        public int GoodsId { get; set; }

    }
}
