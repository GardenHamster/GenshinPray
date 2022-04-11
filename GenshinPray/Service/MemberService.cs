using GenshinPray.Dao;
using GenshinPray.Models.PO;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public class MemberService : BaseService
    {
        private MemberDao memberDao;

        public MemberService()
        {
            this.memberDao =new MemberDao();
        }

        /// <summary>
        /// 通过编号查找成员
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="memberCode"></param>
        /// <returns></returns>
        public MemberPO GetByCode(int authId, string memberCode)
        {
            return memberDao.getMember(authId, memberCode);
        }

        /// <summary>
        /// 根据编号获取成员,成员不存在时,新增并返回一个新成员
        /// </summary>
        /// <param name="authId"></param>
        /// <param name="memberCode"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public MemberPO GetOrInsert(int authId, string memberCode, string memberName = "")
        {
            MemberPO memberInfo = memberDao.getMember(authId, memberCode);
            if (memberName != null) memberName = memberName.CutString(20);
            if (memberInfo == null)
            {
                memberInfo = new MemberPO();
                memberInfo.MemberCode = memberCode;
                memberInfo.MemberName = memberName;
                memberInfo.AuthId = authId;
                memberInfo.Role180Surplus = 180;
                memberInfo.Role90Surplus = 90;
                memberInfo.Role20Surplus = 20;
                memberInfo.Role10Surplus = 10;
                memberInfo.Arm80Surplus = 80;
                memberInfo.Arm20Surplus = 20;
                memberInfo.Arm10Surplus = 10;
                memberInfo.Perm90Surplus = 90;
                memberInfo.Perm10Surplus = 10;
                memberInfo.FullRole90Surplus = 90;
                memberInfo.FullRole10Surplus = 10;
                memberInfo.FullArm80Surplus = 80;
                memberInfo.FullArm10Surplus = 90;
                return memberDao.Insert(memberInfo);
            }
            if (string.IsNullOrEmpty(memberName) == false && memberInfo.MemberName != memberName)
            {
                memberInfo.MemberName = memberName;
                memberDao.Update(memberInfo);
            }
            if (memberInfo.FullRole90Surplus + memberInfo.FullRole10Surplus + memberInfo.FullArm80Surplus + memberInfo.FullArm10Surplus == 0)
            {
                memberInfo.FullRole90Surplus = 90;
                memberInfo.FullRole10Surplus = 10;
                memberInfo.FullArm80Surplus = 80;
                memberInfo.FullArm10Surplus = 90;
                memberDao.Update(memberInfo);
            }

            return memberInfo;
        }

        /// <summary>
        /// 武器定轨
        /// </summary>
        /// <param name="goodsInfo"></param>
        /// <param name="authId"></param>
        /// <param name="memberCode"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public MemberPO SetArmAssign(GoodsPO goodsInfo, int authId, string memberCode, string memberName = "")
        {
            MemberPO memberInfo = GetOrInsert(authId, memberCode, memberName);
            memberInfo.ArmAssignId = goodsInfo.Id;
            memberInfo.ArmAssignValue = 0;//更换或取消当前定轨武器时，命定值将会重置为0，重新累计
            memberDao.Update(memberInfo);
            return memberInfo;
        }

    }
}
