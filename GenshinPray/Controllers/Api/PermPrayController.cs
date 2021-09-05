using GenshinPray.Attribute;
using GenshinPray.Models;
using GenshinPray.Service;
using Microsoft.AspNetCore.Mvc;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PermPrayController : BasePrayController<PermPrayService>
    {
        /// <summary>
        /// 单抽常祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayOne(string memberCode)
        {
            return ApiResult.Error("缺少相关素材");
        }

        /// <summary>
        /// 十连常驻祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayTen(string memberCode)
        {
            return ApiResult.Error("缺少相关素材");
        }


    }
}
