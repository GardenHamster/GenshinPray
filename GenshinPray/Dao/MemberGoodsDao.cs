using GenshinPray.Models.PO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class MemberGoodsDao : DbContext<MemberGoodsPO>
    {
        public int CountByMember(int authId, string memberCode, YSRareType rareType)
        {
            return Db.Queryable<MemberGoodsPO>().Where(o => o.AuthId == authId && o.MemberCode == memberCode && o.RareType == rareType).Count();
        }

        public int CountByMember(int authId, string memberCode, YSPondType pondType, YSRareType rareType)
        {
            return Db.Queryable<MemberGoodsPO>().Where(o => o.AuthId == authId && o.MemberCode == memberCode && o.PondType == pondType && o.RareType == rareType).Count();
        }
    }
}
