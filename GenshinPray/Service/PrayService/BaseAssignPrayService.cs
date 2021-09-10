using GenshinPray.Models;
using GenshinPray.Models.PO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service.PrayService
{
    public abstract class BaseAssignPrayService : BasePrayService
    {
        /// <summary>
        /// 无保底情况下单抽物品概率
        /// </summary>
        protected readonly List<YSProbability> AllList = new List<YSProbability>()
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
            new YSProbability(100, YSProbabilityType.五星物品),
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
        /// 根据名称随机实际补给项目
        /// </summary>
        /// <param name="ysProbability"></param>
        /// <param name="ysUpItem"></param>
        /// <param name="assignGoodsItem"></param>
        /// <param name="assignValue"></param>
        /// <param name="floor20Surplus"></param>
        /// <returns></returns>
        protected abstract YSPrayRecord GetActualItem(YSProbability ysProbability, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int assignValue, int floor20Surplus);

        /// <summary>
        /// 获取祈愿结果
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <param name="ysUpItem"></param>
        /// <param name="assignGoodsItem"></param>
        /// <param name="prayCount"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        public abstract YSPrayResult GetPrayResult(MemberPO memberInfo, YSUpItem ysUpItem, YSGoodsItem assignGoodsItem, int prayCount, int imgWidth);


        /// <summary>
        /// 模拟抽卡,获取祈愿记录
        /// </summary>
        /// <param name="ySUpItem"></param>
        /// <param name="assignGoodsItem"></param>
        /// <param name="prayCount">抽卡次数</param>
        /// <param name="assignValue">命定值</param>
        /// <param name="floor80Surplus">距离80小保底剩余多少抽</param>
        /// <param name="floor20Surplus">距离4星大保底剩余多少抽</param>
        /// <param name="floor10Surplus">距离4星小保底剩余多少抽</param>
        /// <returns></returns>
        public virtual YSPrayRecord[] GetPrayRecord(YSUpItem ySUpItem, YSGoodsItem assignGoodsItem, int prayCount, ref int assignValue, ref int floor80Surplus, ref int floor20Surplus, ref int floor10Surplus)
        {
            YSPrayRecord[] records = new YSPrayRecord[prayCount];
            for (int i = 0; i < records.Length; i++)
            {
                floor80Surplus--;
                floor20Surplus--;
                floor10Surplus--;

                if (floor10Surplus > 0 && floor80Surplus > 0)//无保底情况
                {
                    records[i] = GetActualItem(GetRandomInList(AllList), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus >= 10)//当祈愿获取到4星物品时，有75.000%的概率为本期4星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor10Surplus == 0 && floor20Surplus < 10)//如果本次祈愿获取的4星物品非本期4星UP武器，下次祈愿获取的4星物品必定为本期4星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor10List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor80Surplus == 0 && assignValue < 2)//当祈愿获取到5星武器时，有75.000%的概率为本期5星UP武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                if (floor80Surplus == 0 && assignValue >= 2)//命定值达到满值后，在本祈愿中获得的下一把5星武器必定为当前定轨武器
                {
                    records[i] = GetActualItem(GetRandomInList(Floor80List), ySUpItem, assignGoodsItem, assignValue, floor20Surplus);
                }
                
                bool isUpItem = IsUpItem(ySUpItem, records[i].GoodsItem);//判断是否为本期up的物品
                bool isAssignItem = assignGoodsItem != null && records[i].GoodsItem.GoodsID == assignGoodsItem.GoodsID;//判断是否为本期定轨物品

                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 10;//十连大保底重置为10
                }
                if (records[i].GoodsItem.RareType == YSRareType.四星 && isUpItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == false)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor80Surplus = 80;//八十小保底重置
                    if (assignGoodsItem != null) assignValue++;//如果已经定轨，命定值+1
                }
                if (records[i].GoodsItem.RareType == YSRareType.五星 && isAssignItem == true)
                {
                    floor10Surplus = 10;//十连小保底重置
                    floor20Surplus = 20;//十连大保底重置
                    floor80Surplus = 80;//八十小保底重置
                    if (assignGoodsItem != null) assignValue = 0;//命定值重置
                }
            }
            return records;
        }



    }
}
