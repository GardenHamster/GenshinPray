﻿using GenshinPray.Attribute;
using GenshinPray.Common;
using GenshinPray.Exceptions;
using GenshinPray.Models;
using GenshinPray.Models.DTO;
using GenshinPray.Models.PO;
using GenshinPray.Service.PrayService;
using GenshinPray.Type;
using GenshinPray.Util;
using Microsoft.AspNetCore.Mvc;
using SqlSugar.IOC;
using System;
using System.Collections.Generic;

namespace GenshinPray.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RolePrayController : BasePrayController<RolePrayService>
    {
        /// <summary>
        /// 单抽角色祈愿池
        /// </summary>
        /// <param name="authorize"></param>
        /// <param name="memberCode">成员编号(可以传入QQ号)</param>
        /// <param name="memberName"></param>
        /// <param name="pondIndex">卡池编号</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(prayTimesLimit: true)]
        public ApiResult PrayOne([FromForm] AuthorizeDTO authorize, string memberCode, string memberName = "", int pondIndex = 0, bool toBase64 = false, int imgWidth = 0)
        {
            try
            {
                int prayCount = 1;
                checkNullParam(memberCode);
                CheckImgWidth(imgWidth);

                YSPrayResult ySPrayResult = null;
                AuthorizePO authorizePO = authorize.AuthorizePO;
                Dictionary<int, YSUpItem> upItemDic = goodsService.LoadRoleItem(authorizePO.Id);
                YSUpItem ysUpItem = upItemDic.ContainsKey(pondIndex) ? upItemDic[pondIndex] : null;
                if (ysUpItem == null) ysUpItem = DataCache.DefaultRoleItem.ContainsKey(pondIndex) ? DataCache.DefaultRoleItem[pondIndex] : null;
                if (ysUpItem == null) return ApiResult.PondNotConfigured;

                lock (PrayLock)
                {
                    DbScoped.SugarScope.BeginTran();
                    MemberPO memberInfo = memberService.GetOrInsert(authorizePO.Id, memberCode, memberName);
                    List<MemberGoodsDTO> memberGoods = goodsService.GetMemberGoods(authorizePO.Id, memberCode);
                    ySPrayResult = basePrayService.GetPrayResult(authorizePO, memberInfo, ysUpItem, memberGoods, prayCount);
                    memberService.UpdateMember(memberInfo);//更新保底信息
                    prayRecordService.AddPrayRecord(authorizePO.Id, memberCode, prayCount);//添加调用记录
                    memberGoodsService.AddMemberGoods(ySPrayResult, memberGoods, YSPondType.角色, authorizePO.Id, memberCode);//添加成员出货记录
                    DbScoped.SugarScope.CommitTran();
                }

                ApiPrayResult prayResult = basePrayService.CreatePrayResult(ysUpItem, ySPrayResult, authorizePO, authorize.PrayTimesToday, toBase64, imgWidth);
                return ApiResult.Success(prayResult);
            }
            catch (BaseException ex)
            {
                DbScoped.SugarScope.RollbackTran();
                LogHelper.Info(ex);
                return ApiResult.Error(ex);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, $"authorzation：{GetAuthCode()}，memberCode：{memberCode}，memberName：{memberName}，pondIndex：{pondIndex}，toBase64：{toBase64}，imgWidth：{imgWidth}");
                DbScoped.SugarScope.RollbackTran();
                return ApiResult.ServerError;
            }
        }

        /// <summary>
        /// 十连角色祈愿池
        /// </summary>
        /// <param name="authorize"></param>
        /// <param name="memberCode">成员编号(可以传入QQ号)</param>
        /// <param name="memberName"></param>
        /// <param name="pondIndex">卡池编号</param>
        /// <param name="toBase64"></param>
        /// <param name="imgWidth"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(prayTimesLimit: true)]
        public ApiResult PrayTen([FromForm] AuthorizeDTO authorize, string memberCode, string memberName = "", int pondIndex = 0, bool toBase64 = false, int imgWidth = 0)
        {
            try
            {
                int prayCount = 10;
                checkNullParam(memberCode);
                CheckImgWidth(imgWidth);

                YSPrayResult ySPrayResult = null;
                AuthorizePO authorizePO = authorize.AuthorizePO;
                Dictionary<int, YSUpItem> upItemDic = goodsService.LoadRoleItem(authorizePO.Id);
                YSUpItem ysUpItem = upItemDic.ContainsKey(pondIndex) ? upItemDic[pondIndex] : null;
                if (ysUpItem == null) ysUpItem = DataCache.DefaultRoleItem.ContainsKey(pondIndex) ? DataCache.DefaultRoleItem[pondIndex] : null;
                if (ysUpItem == null) return ApiResult.PondNotConfigured;

                lock (PrayLock)
                {
                    DbScoped.SugarScope.BeginTran();
                    MemberPO memberInfo = memberService.GetOrInsert(authorizePO.Id, memberCode, memberName);
                    List<MemberGoodsDTO> memberGoods = goodsService.GetMemberGoods(authorizePO.Id, memberCode);
                    ySPrayResult = basePrayService.GetPrayResult(authorizePO, memberInfo, ysUpItem, memberGoods, prayCount);
                    memberService.UpdateMember(memberInfo);//更新保底信息
                    prayRecordService.AddPrayRecord(authorizePO.Id, memberCode, prayCount);//添加调用记录
                    memberGoodsService.AddMemberGoods(ySPrayResult, memberGoods, YSPondType.角色, authorizePO.Id, memberCode);//添加成员出货记录
                    DbScoped.SugarScope.CommitTran();
                }

                ApiPrayResult prayResult = basePrayService.CreatePrayResult(ysUpItem, ySPrayResult, authorizePO, authorize.PrayTimesToday, toBase64, imgWidth);
                return ApiResult.Success(prayResult);
            }
            catch (BaseException ex)
            {
                DbScoped.SugarScope.RollbackTran();
                LogHelper.Info(ex);
                return ApiResult.Error(ex);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex, $"authorzation：{GetAuthCode()}，memberCode：{memberCode}，memberName：{memberName}，pondIndex：{pondIndex}，toBase64：{toBase64}，imgWidth：{imgWidth}");
                DbScoped.SugarScope.RollbackTran();
                return ApiResult.ServerError;
            }
        }





    }

}
