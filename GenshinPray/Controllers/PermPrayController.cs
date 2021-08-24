using GenshinPray.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PermPrayController : BasePrayController
    {
        /// <summary>
        /// 单抽常祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<PrayResult>> PrayOne(string authCode, long memberId)
        {
            return ApiResult<PrayResult>.Error("缺少相关素材");
        }

        /// <summary>
        /// 十连常驻祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<PrayResult>> PrayTen(string authCode, long memberId)
        {
            return ApiResult<PrayResult>.Error("缺少相关素材");
        }

    }
}
