using GenshinPray.Type;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("pray_record")]
    public class PrayRecordPO : BasePO
    {
        [SugarColumn(IsNullable = false, ColumnDescription = "授权码ID")]
        public int AuthId { get; set; }

        [SugarColumn(IsNullable = false, Length = 32, ColumnDescription = "成员编号")]
        public string MemberCode { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "祈愿次数")]
        public int PrayCount { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "添加日期")]
        public DateTime CreateDate { get; set; }
    }
}
