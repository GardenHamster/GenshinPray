using GenshinPray.Business;
using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace GenshinPray.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolePrayController : BasePrayController
    {
        private AuthorizeService authorizeService;
        private RolePrayService rolePrayService;
        private MemberService memberService;

        public RolePrayController()
        {
            this.authorizeService = new AuthorizeService();
            this.rolePrayService = new RolePrayService();
            this.memberService = new MemberService();
        }

        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<PrayResult>> PrayOne(string authCode, string memberCode)
        {
            try
            {
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
                if (authorizePO == null || authorizePO.IsDisable || authorizePO.ExpireDate <= DateTime.Now) return ApiResult<PrayResult>.Unauthorized;

                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                int role180Surplus = memberInfo.Role180Surplus;
                int role90Surplus = memberInfo.Role90Surplus;
                int role20Surplus = memberInfo.Role20Surplus;
                int role10Surplus = memberInfo.Role10Surplus;

                YSPrayRecord[] prayRecords = rolePrayService.getPrayRecord(1, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
                int Star5Cost = rolePrayService.getStar5Cost(prayRecords, memberInfo.Role90Surplus);

                memberInfo.Role180Surplus = role180Surplus;
                memberInfo.Role90Surplus = role90Surplus;
                memberInfo.Role20Surplus = role20Surplus;
                memberInfo.Role10Surplus = role10Surplus;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                FileInfo paryFileInfo = DrawHelper.drawOnePrayImg(prayRecords.First());
                PrayResult prayResult = rolePrayService.createPrayResult(prayRecords, paryFileInfo, Star5Cost);
                return ApiResult<PrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<PrayResult>.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<PrayResult>> PrayTen(string authCode, string memberCode)
        {
            try
            {
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
                if (authorizePO == null || authorizePO.IsDisable || authorizePO.ExpireDate <= DateTime.Now) return ApiResult<PrayResult>.Unauthorized;
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);

                int role180Surplus = memberInfo.Role180Surplus;
                int role90Surplus = memberInfo.Role90Surplus;
                int role20Surplus = memberInfo.Role20Surplus;
                int role10Surplus = memberInfo.Role10Surplus;

                YSPrayRecord[] prayRecords = rolePrayService.getPrayRecord(10, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
                YSPrayRecord[] sortPrayRecords = rolePrayService.sortGoods(prayRecords);

                int Star5Cost = rolePrayService.getStar5Cost(prayRecords, memberInfo.Role90Surplus);
                memberInfo.Role180Surplus = role180Surplus;
                memberInfo.Role90Surplus = role90Surplus;
                memberInfo.Role20Surplus = role20Surplus;
                memberInfo.Role10Surplus = role10Surplus;
                memberService.updateMemberInfo(memberInfo);//更新保底信息

                FileInfo paryFileInfo = DrawHelper.drawTenPrayImg(sortPrayRecords);
                PrayResult prayResult = rolePrayService.createPrayResult(prayRecords, paryFileInfo, Star5Cost);
                return ApiResult<PrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<PrayResult>.ServerError;
            }
        }


    }

}
