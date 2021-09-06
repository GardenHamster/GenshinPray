using GenshinPray.Attribute;
using GenshinPray.Models;
using GenshinPray.Service;
using Microsoft.AspNetCore.Mvc;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArmPrayController : BasePrayController<ArmPrayService>
    {
        /// <summary>
        /// 单抽武器祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayOne(string memberCode, bool toBase64 = false, int imgWidth = 0)
        {
            return ApiResult.ServerError;
        }

        /// <summary>
        /// 十连武器祈愿池
        /// </summary>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public override ApiResult PrayTen(string memberCode, bool toBase64 = false, int imgWidth = 0)
        {
            return ApiResult.ServerError;
        }


    }
}
