using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Service;
using GenshinPray.Service.PrayService;
using Microsoft.AspNetCore.Mvc;

namespace GenshinPray.Controllers.Api
{
    public abstract class BasePrayController<T> : ControllerBase where T : BasePrayService, new()
    {
        protected T basePrayService;
        protected AuthorizeService authorizeService;
        protected MemberService memberService;
        protected GoodsService goodsService;
        protected PrayRecordService prayRecordService;
        protected MemberGoodsService memberGoodsService;

        public abstract ApiResult PrayOne(string memberCode, bool toBase64 = false, int imgWidth = 0);

        public abstract ApiResult PrayTen(string memberCode, bool toBase64 = false, int imgWidth = 0);

        public BasePrayController()
        {
            this.basePrayService = new T();
            this.authorizeService = new AuthorizeService();
            this.memberService = new MemberService();
            this.goodsService = new GoodsService();
            this.prayRecordService = new PrayRecordService();
            this.memberGoodsService = new MemberGoodsService();
        }

        protected bool checkImgWidth(int imgWidth)
        {
            if (imgWidth < 0 || imgWidth > 1920) throw new ParamException("图片宽度只能设定在0~1920之间");
            return true;
        }


    }
}
