using GenshinPray.Attribute;
using GenshinPray.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PrayInfoController
    {
        /// <summary>
        /// 获取当前所有祈愿池的up内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public ApiResult GetPrayGoods()
        {
            return ApiResult.Success("hello word");
        }

        /// <summary>
        /// 获取群员祈愿详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public ApiResult GetMemberPrayDetail(string memberCode)
        {
            return ApiResult.Success("hello word");
        }

        /// <summary>
        /// 获取群内抽卡统计排行
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthCode]
        public ApiResult GetRankingByAuth(string authCode)
        {
            return ApiResult.Success("hello word");
        }

    }
}
