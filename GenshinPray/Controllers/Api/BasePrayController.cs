using GenshinPray.Exceptions;
using GenshinPray.Service;
using GenshinPray.Service.PrayService;

namespace GenshinPray.Controllers.Api
{
    public abstract class BasePrayController<T> : BaseController where T : BasePrayService, new()
    {
        protected T basePrayService;
        protected AuthorizeService authorizeService;
        protected MemberService memberService;
        protected GoodsService goodsService;
        protected PrayRecordService prayRecordService;
        protected MemberGoodsService memberGoodsService;
        protected static readonly object PrayLock = new object();

        public BasePrayController()
        {
            this.basePrayService = new T();
            this.authorizeService = new AuthorizeService();
            this.memberService = new MemberService();
            this.goodsService = new GoodsService();
            this.prayRecordService = new PrayRecordService();
            this.memberGoodsService = new MemberGoodsService();
        }

        protected void CheckImgWidth(int imgWidth)
        {
            if (imgWidth < 0 || imgWidth > 1920) throw new ParamException("图片宽度只能设定在0~1920之间");
        }

       

    }
}
