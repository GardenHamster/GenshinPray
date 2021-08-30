using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.Dto;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace GenshinPray.Controllers
{
    public abstract class BasePrayController<T> : ControllerBase where T : BasePrayService, new()
    {
        protected T basePrayService;
        protected AuthorizeService authorizeService;
        protected MemberService memberService;
        protected GoodsService goodsService;

        public BasePrayController()
        {
            this.basePrayService = new T();
            this.authorizeService = new AuthorizeService();
            this.memberService = new MemberService();
            this.goodsService = new GoodsService();
        }

        [HttpGet]
        public abstract ActionResult<ApiResult<ApiPrayResult>> PrayOne(string authCode, string memberCode);

        [HttpGet]
        public abstract ActionResult<ApiResult<ApiPrayResult>> PrayTen(string authCode, string memberCode);

        [HttpPost]
        public abstract ActionResult<ApiResult<ApiPrayResult>> PrayOne(PrayParmDto prayParm, string authCode, string memberCode);

        [HttpPost]
        public abstract ActionResult<ApiResult<ApiPrayResult>> PrayTen(PrayParmDto prayParm, string authCode, string memberCode);

        protected AuthorizePO CheckAuth(string authCode)
        {
            AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
            if (authorizePO == null || authorizePO.IsDisable || authorizePO.ExpireDate <= DateTime.Now) return null;
            return authorizePO;
        }

        protected YSUpItem CheckCustomUp(CustomUpDto customUp)
        {
            if (customUp == null) return null;
            if (customUp.Star5UpItems == null && customUp.Star4UpItems == null) return null;
            if (customUp.Star5UpItems == null || customUp.Star5UpItems.Count == 0)
            {
                throw new ApiLimitException($"至少要指定一项五星UP");
            }
            if (customUp.Star4UpItems == null || customUp.Star5UpItems.Count == 0)
            {
                throw new ApiLimitException($"至少要指定一项四星UP");
            }
            if (customUp.Star5UpItems.Count > 2 || customUp.Star4UpItems.Count > 4)
            {
                throw new ApiLimitException($"自定义五星up数量不能超出2个，自定义四星up数量不能超出4个");
            }
            return goodsService.GetUpItem(customUp);
        }

        protected YSPrayResult GetPrayResult(MemberPO memberInfo, int prayCount)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int role180Surplus = memberInfo.Role180Surplus;
            int role90Surplus = memberInfo.Role90Surplus;
            int role90SurplusBefore = memberInfo.Role90Surplus;
            int role20Surplus = memberInfo.Role20Surplus;
            int role10Surplus = memberInfo.Role10Surplus;

            YSPrayRecord[] prayRecords = basePrayService.getPrayRecord(null, prayCount, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
            YSPrayRecord[] sortPrayRecords = basePrayService.sortGoods(prayRecords);

            memberInfo.Role180Surplus = role180Surplus;
            memberInfo.Role90Surplus = role90Surplus;
            memberInfo.Role20Surplus = role20Surplus;
            memberInfo.Role10Surplus = role10Surplus;
            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.ParyFileInfo = DrawHelper.drawTenPrayImg(sortPrayRecords);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = basePrayService.getStar5Cost(prayRecords, role90SurplusBefore);
            return ysPrayResult;
        }

        protected YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int role180Surplus = memberInfo.Role180Surplus;
            int role90Surplus = memberInfo.Role90Surplus;
            int role90SurplusBefore = memberInfo.Role90Surplus;
            int role20Surplus = memberInfo.Role20Surplus;
            int role10Surplus = memberInfo.Role10Surplus;

            YSPrayRecord[] prayRecords = basePrayService.getPrayRecord(ysUpItem, prayCount, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
            YSPrayRecord[] sortPrayRecords = basePrayService.sortGoods(prayRecords);

            memberInfo.Role180Surplus = role180Surplus;
            memberInfo.Role90Surplus = role90Surplus;
            memberInfo.Role20Surplus = role20Surplus;
            memberInfo.Role10Surplus = role10Surplus;
            memberInfo.TotalPrayTimes += prayCount;

            ysPrayResult.ParyFileInfo = DrawHelper.drawTenPrayImg(sortPrayRecords);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = basePrayService.getStar5Cost(prayRecords, role90SurplusBefore);
            return ysPrayResult;
        }


    }
}
