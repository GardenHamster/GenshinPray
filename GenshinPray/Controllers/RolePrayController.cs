using GenshinPray.Business;
using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using GenshinPray.Type;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolePrayController : BasePrayController
    {
        private AuthorizeService authorizeService;
        private MemberService memberService;

        public RolePrayController()
        {
            this.authorizeService = new AuthorizeService();
            this.memberService = new MemberService();
        }

        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<string>> RolePrayOne(long memberId)
        {
            return ApiResult<string>.Success("hello word");
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<PrayResult>> RolePrayTen(string authCode,long memberId)
        {
            try
            {
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
                if (authorizePO == null) return ApiResult<PrayResult>.Unauthorized;
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id,memberId);
                int role180Surplus = memberInfo.Role180Surplus;
                int role90Surplus = memberInfo.Role90Surplus;
                int role20Surplus = memberInfo.Role20Surplus;
                int role10Surplus = memberInfo.Role10Surplus;

                YSSupplyRecord[] supplyRecords = getPrayRecord(10, ref role180Left, ref role90Left, ref role20Left, ref role10Left);
                YSSupplyRecord[] sortSupplyRecords = sortSupply(supplyRecords);

                int Star5Cost = getStar5Cost(supplyRecords, memberInfo.Role90Left);
                memberInfo.Role180Left = role180Left;
                memberInfo.Role90Left = role90Left;
                memberInfo.Role20Left = role20Left;
                memberInfo.Role10Left = role10Left;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                FileInfo paryFileInfo = drawTenPrayImg(sortSupplyRecords);
                PrayResult prayResult = new PrayResult();
                return ApiResult<PrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex);
                return ApiResult<PrayResult>.ServerError;
            }
        }


        /// <summary>
        /// 随机一个十连记录
        /// </summary>
        /// <param name="role180Left"></param>
        /// <param name="role90Left"></param>
        /// <param name="role20Left"></param>
        /// <param name="role10Left"></param>
        /// <returns></returns>
        public YSSupplyRecord[] getPrayRecord(int prayCount, ref int role180Left, ref int role90Left, ref int role20Left, ref int role10Left)
        {
            YSSupplyRecord[] records = new YSSupplyRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                role180Left--;
                role90Left--;
                role20Left--;
                role10Left--;

                if (role10Left > 0 && role90Left > 0)//无保底情况
                {
                    records[i] = getSupplyRecord(getRandomSupplyInList(AllList), role180Left, role20Left);
                }
                if (role10Left == 0 && role20Left >= 10)//十连小保底,4星up概率为50%
                {
                    records[i] = getSupplyRecord(getRandomSupplyInList(Floor10List), role180Left, role20Left);
                }
                if (role10Left == 0 && role20Left < 10)//十连大保底,4星up概率为50%
                {
                    records[i] = getSupplyRecord(getRandomSupplyInList(Floor10List), role180Left, role20Left);
                }
                if (role90Left == 0 && role180Left >= 90)//90小保底,5星up概率为50%
                {
                    records[i] = getSupplyRecord(getRandomSupplyInList(Floor90List), role180Left, role20Left);
                }
                if (role90Left == 0 && role180Left < 90)//90大保底,5星up概率为50%
                {
                    records[i] = getSupplyRecord(getRandomSupplyInList(Floor90List), role180Left, role20Left);
                }

                bool isupItem = isUpItem(records[i].SupplyItem);

                if (records[i].SupplyItem.RareType == YSRareType.四星 && isupItem == false)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 10;//十连大保底重置
                }
                if (records[i].SupplyItem.RareType == YSRareType.四星 && isupItem == true)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                }
                if (records[i].SupplyItem.RareType == YSRareType.五星 && isupItem == false)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                    role90Left = 90;//九十小保底重置
                    role180Left = 90;//九十大保底重置
                }
                if (records[i].SupplyItem.RareType == YSRareType.Star5 && isupItem == true)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                    role90Left = 90;//九十小保底重置
                    role180Left = 180;//九十大保底重置
                }
            }
            return records;
        }






    }

}
