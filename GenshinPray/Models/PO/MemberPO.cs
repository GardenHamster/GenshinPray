using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models.PO
{
    [SugarTable("member")]
    public class MemberPO : BasePO
    {
        [SugarColumn(IsNullable = false, ColumnDescription = "授权码ID")]
        public int AuthId { get; set; }

        [SugarColumn(IsNullable = false, Length = 32, ColumnDescription = "成员编号")]
        public string MemberCode { get; set; }

        [SugarColumn(IsNullable = false, DefaultValue = "0", ColumnDescription = "武器定轨Id，0表示无定轨")]
        public int ArmAssignId { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "角色池剩余多少发五星大保底")]
        public int Role180Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "角色池剩余多少发五星保底")]
        public int Role90Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "角色池剩余多少发十连大保底")]
        public int Role20Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "角色池剩余多少发十连保底")]
        public int Role10Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "武器池剩余多少发五星大保底")]
        public int Arm180Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "武器池剩余多少发五星保底")]
        public int Arm90Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "武器池剩余多少发十连大保底")]
        public int Arm20Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "武器池剩余多少发十连保底")]
        public int Arm10Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "常驻池剩余多少发五星大保底")]
        public int Perm180Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "常驻池剩余多少发五星保底")]
        public int Perm90Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "常驻池剩余多少发十连大保底")]
        public int Perm20Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "常驻池剩余多少发十连保底")]
        public int Perm10Surplus { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "角色池祈愿次数")]
        public int RolePrayTimes { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "武器池祈愿次数")]
        public int ArmPrayTimes { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "常驻池祈愿次数")]
        public int PermPrayTimes { get; set; }

        [SugarColumn(IsNullable = false, ColumnDescription = "总祈愿次数")]
        public int TotalPrayTimes { get; set; }
    }
}
