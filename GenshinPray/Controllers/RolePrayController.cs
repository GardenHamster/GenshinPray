using GenshinPray.Business;
using GenshinPray.Common;
using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Service;
using GenshinPray.Type;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        protected readonly List<YSGoodsItem> AllList = new List<YSGoodsItem>()
        {
            new YSGoodsItem(0.6m,YSGoodsType.其他,YSRareType.其他,"5星物品"),
            new YSGoodsItem(5.1m,YSGoodsType.其他,YSRareType.其他,"4星物品"),
            new YSGoodsItem(94.3m,YSGoodsType.其他,YSRareType.其他,"3星物品")
        };

        protected readonly List<YSGoodsItem> Floor10List = new List<YSGoodsItem>()
        {
            new YSGoodsItem(0.6m,YSGoodsType.其他,YSRareType.其他,"5星物品"),
            new YSGoodsItem(99.4m,YSGoodsType.其他,YSRareType.其他,"4星物品")
        };

        protected readonly List<YSGoodsItem> Floor90List = new List<YSGoodsItem>()
        {
            new YSGoodsItem(100,YSGoodsType.其他,YSRareType.其他,"5星物品"),
        };

        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
        /// <param name="memberId">玩家ID(可以传入QQ号)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResult<PrayResult>> RolePrayOne(string authCode, long memberId)
        {
            try
            {
                AuthorizePO authorizePO = authorizeService.GetAuthorize(authCode);
                if (authorizePO == null) return ApiResult<PrayResult>.Unauthorized;
                MemberPO memberInfo = memberService.getOrInsert(authorizePO.Id, memberId);
                int role180Surplus = memberInfo.Role180Surplus;
                int role90Surplus = memberInfo.Role90Surplus;
                int role20Surplus = memberInfo.Role20Surplus;
                int role10Surplus = memberInfo.Role10Surplus;

                YSPrayRecord[] prayRecords = getPrayRecord(1, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
                YSPrayRecord prayRecord = prayRecords.First();

                int Star5Cost = getStar5Cost(prayRecords, memberInfo.Role90Surplus);
                memberInfo.Role180Surplus = role180Surplus;
                memberInfo.Role90Surplus = role90Surplus;
                memberInfo.Role20Surplus = role20Surplus;
                memberInfo.Role10Surplus = role10Surplus;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                FileInfo paryFileInfo = drawOnePrayImg(prayRecord);
                PrayResult prayResult = new PrayResult();
                return ApiResult<PrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<PrayResult>.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="authCode">授权码</param>
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

                YSPrayRecord[] prayRecords = getPrayRecord(10, ref role180Surplus, ref role90Surplus, ref role20Surplus, ref role10Surplus);
                YSPrayRecord[] sortPrayRecords = sortGoods(prayRecords);

                int Star5Cost = getStar5Cost(prayRecords, memberInfo.Role90Surplus);
                memberInfo.Role180Surplus = role180Surplus;
                memberInfo.Role90Surplus = role90Surplus;
                memberInfo.Role20Surplus = role20Surplus;
                memberInfo.Role10Surplus = role10Surplus;
                memberService.updateMemberInfo(memberInfo);//更新保底信息
                FileInfo paryFileInfo = drawTenPrayImg(sortPrayRecords);
                PrayResult prayResult = new PrayResult();
                return ApiResult<PrayResult>.Success(prayResult);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex);
                return ApiResult<PrayResult>.ServerError;
            }
        }


        /// <summary>
        /// 随机一个十连记录
        /// </summary>
        /// <param name="prayCount"></param>
        /// <param name="role180Left"></param>
        /// <param name="role90Left"></param>
        /// <param name="role20Left"></param>
        /// <param name="role10Left"></param>
        /// <returns></returns>
        public YSPrayRecord[] getPrayRecord(int prayCount, ref int role180Left, ref int role90Left, ref int role20Left, ref int role10Left)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                role180Left--;
                role90Left--;
                role20Left--;
                role10Left--;

                if (role10Left > 0 && role90Left > 0)//无保底情况
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(AllList), role180Left, role20Left);
                }
                if (role10Left == 0 && role20Left >= 10)//十连小保底,4星up概率为50%
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor10List), role180Left, role20Left);
                }
                if (role10Left == 0 && role20Left < 10)//十连大保底,必出4星up物品
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor10List), role180Left, role20Left);
                }
                if (role90Left == 0 && role180Left >= 90)//90小保底,5星up概率为50%
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor90List), role180Left, role20Left);
                }
                if (role90Left == 0 && role180Left < 90)//90大保底,必出5星up物品
                {
                    records[i] = getPrayRecord(getRandomGoodsInList(Floor90List), role180Left, role20Left);
                }

                bool isupItem = isUpItem(records[i].GoodsItem);

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isupItem == false)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 10;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isupItem == true)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isupItem == false)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                    role90Left = 90;//九十小保底重置
                    role180Left = 90;//九十大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isupItem == true)
                {
                    role10Left = 10;//十连小保底重置
                    role20Left = 20;//十连大保底重置
                    role90Left = 90;//九十小保底重置
                    role180Left = 180;//九十大保底重置
                }
            }
            return records;
        }

        /// <summary>
        /// 根据名称随机实际补给项目
        /// </summary>
        /// <param name="prayRecord"></param>
        /// <param name="role180Left"></param>
        /// <param name="role20Left"></param>
        /// <returns></returns>
        private YSPrayRecord getPrayRecord(YSPrayRecord prayRecord, int role180Left, int role20Left)
        {
            if (prayRecord.GoodsItem.GoodsName == "5星物品")
            {
                bool isGetUp = role180Left < 90 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? getRandomGoodsInList(Setting.RoleStar5UpList) : getRandomGoodsInList(Setting.RoleStar5NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "4星物品")
            {
                bool isGetUp = role20Left < 10 ? true : RandomHelper.getRandomBetween(1, 100) > 50;
                return isGetUp ? getRandomGoodsInList(Setting.RoleStar4UpList) : getRandomGoodsInList(Setting.RoleStar4NonUpList);
            }
            if (prayRecord.GoodsItem.GoodsName == "3星物品")
            {
                return getRandomGoodsInList(Setting.ArmStar3PermList);
            }
            return prayRecord;
        }

        /// <summary>
        /// 判断一个项目是否up项目
        /// </summary>
        /// <param name="goodsItem"></param>
        /// <returns></returns>
        private bool isUpItem(YSGoodsItem goodsItem)
        {
            if (Setting.RoleStar5UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) return true;
            if (Setting.RoleStar4UpList.Where(m => m.GoodsName == goodsItem.GoodsName).Count() > 0) return true;
            return false;
        }

        /// <summary>
        /// 获取5星列表字符串
        /// </summary>
        /// <returns></returns>
        private string getStar5UpStr()
        {
            string returnStr = "";
            foreach (var item in Setting.RoleStar5UpList)
            {
                if (returnStr.Length > 0) returnStr += "，";
                returnStr += item.GoodsName;
            }
            return returnStr;
        }

        


        /// <summary>
        /// 获取90发内,玩家获得5星角色的累计祈愿次数,0代表还未获得S
        /// </summary>
        /// <param name="YSPrayRecords"></param>
        /// <param name="floorLeft"></param>
        /// <returns></returns>
        public int getStar5Cost(YSPrayRecord[] YSPrayRecords, int floorLeft)
        {
            int star5Index = -1;
            for (int i = 0; i < YSPrayRecords.Length; i++)
            {
                YSGoodsItem YSGoodsItem = YSPrayRecords[i].GoodsItem;
                if (YSGoodsItem.RareType != YSRareType.五星) continue;
                star5Index = i;
                break;
            }
            if (star5Index == -1) return 0;
            return 90 - floorLeft + star5Index + 1;
        }


    }

}
