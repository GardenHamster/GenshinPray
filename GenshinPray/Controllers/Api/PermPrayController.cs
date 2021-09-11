using GenshinPray.Attribute;
using GenshinPray.Common;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service.PrayService;
using GenshinPray.Type;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PermPrayController : BasePrayController<PermPrayService>
    {
        /// <summary>
        /// 单抽常祈愿池
        /// </summary>
        /// <param name="memberCode">成员编号(可以传入QQ号)</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayOne(string memberCode, bool toBase64 = false, int imgWidth = 0)
        {
            try
            {
                int prayCount = 1;
                checkNullParam(memberCode);
                checkImgWidth(imgWidth);

                var authorzation = HttpContext.Request.Headers["authorzation"];
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authorzation);

                int prayTimesToday = prayRecordService.GetPrayTimesToday(authorizePO.Id);
                if (prayTimesToday >= authorizePO.DailyCall) return ApiResult.ApiMaximum;

                MemberPO memberInfo = memberService.GetOrInsert(authorizePO.Id, memberCode);
                YSUpItem ySUpItem = SiteConfig.DefaultUpItem[YSPondType.常驻];
                YSPrayResult ySPrayResult = basePrayService.GetPrayResult(memberInfo, ySUpItem, prayCount, imgWidth);

                prayRecordService.AddPrayRecord(authorizePO.Id, memberCode, prayCount);//添加调用记录
                memberGoodsService.AddMemberGoods(ySPrayResult, YSPondType.常驻, authorizePO.Id, memberCode);//添加成员出货记录

                ApiPrayResult prayResult = basePrayService.CreatePrayResult(ySUpItem, ySPrayResult, authorizePO, prayTimesToday, toBase64);
                return ApiResult.Success(prayResult);
            }
            catch (BaseException ex)
            {
                return ApiResult.Error(ex);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult.ServerError;
            }
        }

        /// <summary>
        /// 十连常驻祈愿池
        /// </summary>
        /// <param name="memberCode">成员编号(可以传入QQ号)</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayTen(string memberCode, bool toBase64 = false, int imgWidth = 0)
        {
            try
            {
                int prayCount = 10;
                checkNullParam(memberCode);
                checkImgWidth(imgWidth);

                var authorzation = HttpContext.Request.Headers["authorzation"];
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authorzation);

                int prayTimesToday = prayRecordService.GetPrayTimesToday(authorizePO.Id);
                if (prayTimesToday >= authorizePO.DailyCall) return ApiResult.ApiMaximum;

                MemberPO memberInfo = memberService.GetOrInsert(authorizePO.Id, memberCode);
                YSUpItem ySUpItem = SiteConfig.DefaultUpItem[YSPondType.常驻];
                YSPrayResult ySPrayResult = basePrayService.GetPrayResult(memberInfo, ySUpItem, prayCount, imgWidth);

                prayRecordService.AddPrayRecord(authorizePO.Id, memberCode, prayCount);//添加调用记录
                memberGoodsService.AddMemberGoods(ySPrayResult, YSPondType.常驻, authorizePO.Id, memberCode);//添加成员出货记录

                ApiPrayResult prayResult = basePrayService.CreatePrayResult(ySUpItem, ySPrayResult, authorizePO, prayTimesToday, toBase64);
                return ApiResult.Success(prayResult);
            }
            catch (BaseException ex)
            {
                return ApiResult.Error(ex);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult.ServerError;
            }
        }


    }
}
