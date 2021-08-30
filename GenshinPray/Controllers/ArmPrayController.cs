using GenshinPray.Models;
using GenshinPray.Models.Dto;
using GenshinPray.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArmPrayController : BasePrayController<ArmPrayService>
    {
        /// <summary>
        /// 单抽武器祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayOne(string authCode, string memberCode)
        {
            return ApiResult<ApiPrayResult>.Error("缺少相关素材");
        }

        /// <summary>
        /// 十连武器祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayTen(string authCode, string memberCode)
        {
            return ApiResult<ApiPrayResult>.Error("缺少相关素材");
        }

        /// <summary>
        /// 单抽武器祈愿池(自定义)
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayOne(PrayParmDto prayParm, string authCode, string memberCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 十连武器祈愿池(自定义)
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberCode">玩家编号(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpPost]
        public override ActionResult<ApiResult<ApiPrayResult>> PrayTen(PrayParmDto prayParm, string authCode, string memberCode)
        {
            throw new NotImplementedException();
        }


    }
}
