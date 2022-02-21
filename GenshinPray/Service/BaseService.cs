using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.VO;
using GenshinPray.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Service
{
    public abstract class BaseService
    {
        /// <summary>
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="prayRecords"></param>
        /// <returns></returns>
        public List<GoodsVO> ChangeToGoodsVO(YSPrayRecord[] prayRecords)
        {
            return prayRecords.Select(m => new GoodsVO()
            {
                GoodsName = m.GoodsItem.GoodsName,
                GoodsType = Enum.GetName(typeof(YSGoodsType), m.GoodsItem.GoodsType),
                GoodsSubType = Enum.GetName(typeof(YSGoodsSubType), m.GoodsItem.GoodsSubType),
                RareType = Enum.GetName(typeof(YSRareType), m.GoodsItem.RareType),
            }).ToList();
        }

        /// <summary>
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="goodsItems"></param>
        /// <returns></returns>
        public List<GoodsVO> ChangeToGoodsVO(List<YSGoodsItem> goodsItems)
        {
            return goodsItems.Select(m => new GoodsVO()
            {
                GoodsName = m.GoodsName,
                GoodsType = Enum.GetName(typeof(YSGoodsType), m.GoodsType),
                GoodsSubType = Enum.GetName(typeof(YSGoodsSubType), m.GoodsSubType),
                RareType = Enum.GetName(typeof(YSRareType), m.RareType)
            }).ToList();
        }

        /// <summary>
        /// 将YSPrayRecord转换为GoodsVO
        /// </summary>
        /// <param name="prayRecordList"></param>
        /// <returns></returns>
        public List<PrayRecordVO> ChangeToPrayRecordVO(List<PrayRecordDTO> prayRecordList)
        {
            return prayRecordList.Select(m => new PrayRecordVO()
            {
                GoodsName = m.GoodsName,
                GoodsType = Enum.GetName(typeof(YSGoodsType), m.GoodsType),
                GoodsSubType = Enum.GetName(typeof(YSGoodsSubType), m.GoodsSubType),
                RareType = Enum.GetName(typeof(YSRareType), m.RareType),
                Cost = m.Cost,
                CreateDate = m.CreateDate
            }).ToList();
        }

    }
}
