using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.Dto;
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
    public class RolePrayController : BasePrayController<RolePrayService>
    {
        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayOne(string authCode, string memberCode)
        {
            try
            {
                int prayCount = 1;
                AuthorizePO authorizePO = CheckAuth(authCode);
                if (authorizePO == null) return ApiResult<ApiPrayResult>.Unauthorized;
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult<ApiPrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<ApiPrayResult>.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayTen(string authCode, string memberCode)
        {
            try
            {
                int prayCount = 10;
                AuthorizePO authorizePO = CheckAuth(authCode);
                if (authorizePO == null) return ApiResult<ApiPrayResult>.Unauthorized;
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult<ApiPrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<ApiPrayResult>.ServerError;
            }
        }


        /// <summary>
        /// 十连角色祈愿池(自定义)
        /// </summary>
        /// <param name="prayParm">自定义参数</param>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayOne(PrayParmDto prayParm, string authCode, string memberCode)
        {
            try
            {
                int prayCount = 1;
                AuthorizePO authorizePO = CheckAuth(authCode);
                if (authorizePO == null) return ApiResult<ApiPrayResult>.Unauthorized;
                YSUpItem ysUpItem = CheckCustomUp(prayParm?.CustomUp);
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, ysUpItem, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult, prayParm);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult<ApiPrayResult>.Success(prayResult);
            }
            catch (ApiLimitException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch (GoodsNotFoundException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch(ParamException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<ApiPrayResult>.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池(自定义)
        /// </summary>
        /// <param name="prayParm">自定义参数</param>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayTen(PrayParmDto prayParm, string authCode, string memberCode)
        {
            try
            {
                int prayCount = 10;
                AuthorizePO authorizePO = CheckAuth(authCode);
                if (authorizePO == null) return ApiResult<ApiPrayResult>.Unauthorized;
                YSUpItem ysUpItem = CheckCustomUp(prayParm?.CustomUp);
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberCode);
                YSPrayResult ySPrayResult = GetPrayResult(memberInfo, ysUpItem, prayCount);
                memberInfo.RolePrayTimes += prayCount;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                ApiPrayResult prayResult = basePrayService.createPrayResult(ySPrayResult, prayParm);
                prayResult.Surplus180 = memberInfo.Role180Surplus;
                prayResult.Surplus90 = memberInfo.Role90Surplus;
                return ApiResult<ApiPrayResult>.Success(prayResult);
            }
            catch (ApiLimitException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch (GoodsNotFoundException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch (ParamException ex)
            {
                return ApiResult<ApiPrayResult>.Error(ex.Message);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<ApiPrayResult>.ServerError;
            }
        }



    }

}
