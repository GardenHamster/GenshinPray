using GenshinPray.Attribute;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolePrayController : BasePrayController<RolePrayService>
    {
        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayOne(string memberCode)
        {
            try
            {
                int prayCount = 1;
                var authorzation = HttpContext.Request.Headers["authorzation"];
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authorzation);
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayTen(string memberCode)
        {
            try
            {
                int prayCount = 10;
                var authorzation = HttpContext.Request.Headers["authorzation"];
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authorzation);
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult.ServerError;
            }
        }





    }

}
