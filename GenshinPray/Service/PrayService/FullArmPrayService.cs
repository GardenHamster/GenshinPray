using GenshinPray.Dao;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Service.PrayService;
using GenshinPray.Type;
using GenshinPray.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service.PrayService
{
    public class FullArmPrayService : BasePrayService
    {

        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSProbability> SingleList = new List<YSProbability>()
        {
            new YSProbability(0.7m, YSProbabilityType.五星物品),
            new YSProbability(6.0m, YSProbabilityType.四星物品),
            new YSProbability(93.3m,YSProbabilityType.三星物品)
        };

        /// <summary>
        /// 小保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor80List = new List<YSProbability>()
        {
            new YSProbability(100, YSProbabilityType.五星物品)
        };

        /// <summary>
        /// 十连保底物品概率
        /// </summary>
        protected readonly List<YSProbability> Floor10List = new List<YSProbability>()
        {
            new YSProbability(0.7m, YSProbabilityType.五星物品),
            new YSProbability(99.3m,YSProbabilityType.四星物品)
        };


        /// <summary>
        /// 模拟抽卡,获取祈愿记录
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="ySUpItem"></param>
        /// <param name="memberGoods"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(MemberPO memberInfo, YSUpItem ySUpItem, List<MemberGoodsDTO> memberGoods, int prayCount)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                memberInfo.FullArm80Surplus--;
                memberInfo.FullArm10Surplus--;

                if (memberInfo.FullArm10Surplus > 0)//无保底
                {
                    records[i] = GetActualItem(GetRandomInList(SingleList), ySUpItem);
                }
                if (memberInfo.FullArm10Surplus <= 0)//十连保底
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem);
                }

                //武器池从第66抽开始,每抽出5星概率提高7%(基础概率),直到第80抽时概率上升到100%
                if (memberInfo.FullArm80Surplus < 14 && RandomHelper.getRandomBetween(1, 100) < (14 - memberInfo.FullArm80Surplus + 1) * 0.07 * 100)//低保
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem);
                }

                records[i].IsNew = CheckIsNew(memberGoods, records, records[i]);//判断是否为New
                records[i].OwnCountBefore = GetOwnCountBefore(memberGoods, records, records[i]);//统计已拥有数量

                if (records[i].GoodsItem.RareType == YSRareType.四星)
                {
                    records[i].Cost = 10 - memberInfo.FullArm10Surplus;
                    memberInfo.FullArm10Surplus = 10;//十连小保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星)
                {
                    records[i].Cost = 80 - memberInfo.FullArm80Surplus;
                    memberInfo.FullArm10Surplus = 10;//十连小保底重置
                    memberInfo.FullArm80Surplus = 80;//八十发保底重置
                }
            }
            return records;
        }

        protected YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem)
        {
            if (ysProbability.ProbabilityType == YSProbabilityType.五星物品) return GetRandomInList(ysUpItem.Star5AllList);
            if (ysProbability.ProbabilityType == YSProbabilityType.四星物品) return GetRandomInList(ysUpItem.Star4AllList);
            if (ysProbability.ProbabilityType == YSProbabilityType.三星物品) return GetRandomInList(ysUpItem.Star3AllList);
            throw new GoodsNotFoundException($"未能随机获取与{Enum.GetName(typeof(YSProbability), ysProbability.ProbabilityType)}类型对应的物品");
        }

        public YSPrayResult GetPrayResult(AuthorizePO authorize, MemberPO memberInfo, YSUpItem ysUpItem, List<MemberGoodsDTO> memberGoods, int prayCount, int imgWidth)
        {
            YSPrayResult ysPrayResult = new YSPrayResult();
            int arm80SurplusBefore = memberInfo.FullArm80Surplus;

            YSPrayRecord[] prayRecords = GetPrayRecord(memberInfo, ysUpItem, memberGoods, prayCount);
            YSPrayRecord[] sortPrayRecords = SortGoods(prayRecords);

            memberInfo.ArmPrayTimes += prayCount;
            memberInfo.TotalPrayTimes += prayCount;
            memberDao.Update(memberInfo);//更新保底信息

            ysPrayResult.MemberInfo = memberInfo;
            ysPrayResult.ParyFileInfo = DrawPrayImg(authorize, sortPrayRecords, memberInfo, imgWidth);
            ysPrayResult.PrayRecords = prayRecords;
            ysPrayResult.SortPrayRecords = sortPrayRecords;
            ysPrayResult.Star5Cost = GetStar5Cost(prayRecords, arm80SurplusBefore, 80);
            ysPrayResult.Surplus10 = memberInfo.FullArm10Surplus;
            return ysPrayResult;
        }




    }
}
