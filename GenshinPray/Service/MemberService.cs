using GenshinPray.Dao;
using GenshinPray.Models.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class MemberService
    {
        private MemberDao memberDao;

        public MemberService()
        {
            this.memberDao =new MemberDao();
        }

        public MemberPO GetOrInsert(int authId, string memberCode)
        {
            MemberPO memberInfo = memberDao.getMember(authId, memberCode);
            if (memberInfo != null) return memberInfo;
            memberInfo = new MemberPO();
            memberInfo.MemberCode = memberCode;
            memberInfo.AuthId = authId;
            memberInfo.Role180Surplus = 180;
            memberInfo.Role90Surplus = 90;
            memberInfo.Role20Surplus = 20;
            memberInfo.Role10Surplus = 10;
            memberInfo.Arm180Surplus = 180;
            memberInfo.Arm90Surplus = 90;
            memberInfo.Arm20Surplus = 20;
            memberInfo.Arm10Surplus = 10;
            memberInfo.Perm180Surplus = 180;
            memberInfo.Perm90Surplus = 90;
            memberInfo.Perm20Surplus = 20;
            memberInfo.Perm10Surplus = 10;
            return memberDao.Insert(memberInfo);
        }

        public int UpdateMemberInfo(MemberPO memberInfo)
        {
            return memberDao.Update(memberInfo);
        }

    }
}
