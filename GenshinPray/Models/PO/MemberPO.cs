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
        public int AuthId { get; set; }

        public long MemberId { get; set; }

        public int Role180Surplus { get; set; }

        public int Role90Surplus { get; set; }

        public int Role20Surplus { get; set; }

        public int Role10Surplus { get; set; }

        public int Arm180Surplus { get; set; }

        public int Arm90Surplus { get; set; }

        public int Arm20Surplus { get; set; }

        public int Arm10Surplus { get; set; }

        public int Perm180Surplus { get; set; }

        public int Perm90Surplus { get; set; }

        public int Perm20Surplus { get; set; }

        public int Perm10Surplus { get; set; }

        public int RolePrayTimes { get; set; }

        public int ArmPrayTimes { get; set; }

        public int PermPrayTimes { get; set; }

        public int TotalPrayTimes { get; set; }
    }
}
