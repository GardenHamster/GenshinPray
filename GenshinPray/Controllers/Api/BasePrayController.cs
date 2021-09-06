using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GenshinPray.Controllers.Api
{
    public abstract class BasePrayController<T> : ControllerBase where T : BasePrayService, new()
    {
        protected T basePrayService;
        protected AuthorizeService authorizeService;
        protected MemberService memberService;
        protected GoodsService goodsService;
        protected PrayRecordService prayRecordService;

        public abstract ApiResult PrayOne(string memberCode, bool toBase64 = false, int imgWidth = 0);

        public abstract ApiResult PrayTen(string memberCode, bool toBase64 = false, int imgWidth = 0);

        public BasePrayController()
        {
            this.basePrayService = new T();
            this.authorizeService = new AuthorizeService();
            this.memberService = new MemberService();
            this.goodsService = new GoodsService();
            this.prayRecordService = new PrayRecordService();
        }

        protected YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, int prayCount, int imgWidth)
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

            ysPrayResult.ParyFileInfo = prayCount == 1 ? DrawHelper.drawOnePrayImg(sortPrayRecords.First(), imgWidth) : DrawHelper.drawTenPrayImg(sortPrayRecords, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = basePrayService.getStar5Cost(prayRecords, role90SurplusBefore);
            return ysPrayResult;
        }

        protected bool checkImgWidth(int imgWidth)
        {
            if (imgWidth < 0 || imgWidth > 1920) throw new ParamException("图片宽度只能设定在0~1920之间");
            return true;
        }


    }
}
