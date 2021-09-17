using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<LuckRankingDTO> getLuckRanking(int authId, int top, YSRareType rareType, DateTime startDate, DateTime endDate)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" select temp.AuthId, temp.MemberCode, temp.RareType, temp.RareCount,");
            sqlBuilder.Append(" m.TotalPrayTimes, temp.rareCount/m.TotalPrayTimes as RareRate from member m");
            sqlBuilder.Append(" inner join (");
            sqlBuilder.Append(" 	select AuthId,MemberCode,RareType,count(RareType) RareCount from member_goods");
            sqlBuilder.Append(" 	where AuthId=@AuthId and RareType=@RareType and CreateDate>=@StartDate and CreateDate<@EndDate");
            sqlBuilder.Append(" 	group by AuthId,MemberCode,RareType limit @Top");
            sqlBuilder.Append(" ) temp on m.MemberCode=m.MemberCode");
            sqlBuilder.Append(" where m.AuthId=@AuthId order by temp.RareType desc,rareRate desc");
            return Db.Ado.SqlQuery<LuckRankingDTO>(sqlBuilder.ToString(), new { AuthId = authId, Top = top, RareType = (int)rareType, StartDate = startDate, EndDate = endDate });
        }


    }
}
