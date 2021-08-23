using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Dao
{
    public class MemberDao : DbContext<MemberPO>
    {
        public MemberPO getMember(long authId, long memberId)
        {
            return Db.Queryable<MemberPO>().Where(o => o.AuthId == authId && o.MemberId == memberId).Single();
        }

    }



}
