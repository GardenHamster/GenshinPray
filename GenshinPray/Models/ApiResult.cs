using GenshinPray.Common;
using GenshinPray.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class ApiResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public ApiResult() { }

        public ApiResult(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ApiResult(int code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static ApiResult Success()
        {
            return new ApiResult(ResultCode.Success, "ok", null);
        }

        public static ApiResult Success(string message)
        {
            return new ApiResult(ResultCode.Success, message, null);
        }

        public static ApiResult Success(object data)
        {
            return new ApiResult(ResultCode.Success, "ok", data);
        }

        public static ApiResult Success(string message, object data)
        {
            return new ApiResult(ResultCode.Success, message, data);
        }

        public static ApiResult Error(BaseException ex)
        {
            return new ApiResult(ex.ErrorCode, ex.Message);
        }

        public static ApiResult Error(int code, string message)
        {
            return new ApiResult(code, message);
        }

        public static ApiResult Error(string message)
        {
            return new ApiResult(ResultCode.Error, message);
        }

        /// <summary>
        /// 未授权
        /// </summary>
        public static ApiResult Unauthorized
        {
            get
            {
                return new ApiResult((int)HttpStatusCode.Unauthorized, "授权码不存在或者已经过期");
            }
        }

        /// <summary>
        /// 内部异常
        /// </summary>
        public static ApiResult ServerError
        {
            get
            {
                return new ApiResult((int)HttpStatusCode.InternalServerError, "接口异常，如果问题一直重复出现请联系作者");
            }
        }

        /// <summary>
        /// 超过每日api调用上限
        /// </summary>
        public static ApiResult ApiMaximum
        {
            get
            {
                return new ApiResult(ResultCode.ApiMaximum, "今日api调用上限，请参考贴子获取独立授权码");
            }
        }

        /// <summary>
        /// 找不到相关物品
        /// </summary>
        public static ApiResult GoodsNotFound
        {
            get
            {
                return new ApiResult(ResultCode.GoodsNotFound, "找不到相关物品");
            }
        }

        /// <summary>
        /// 找不到相关物品
        /// </summary>
        public static ApiResult AssignNotFound
        {
            get
            {
                return new ApiResult(ResultCode.AssignNotFound, "定轨的物品不在当前up池中");
            }
        }

        /// <summary>
        /// 参数错误
        /// </summary>
        public static ApiResult InvalidParameter
        {
            get
            {
                return new ApiResult(ResultCode.InvalidParameter, "参数错误");
            }
        }

        /// <summary>
        /// 卡池未配置
        /// </summary>
        public static ApiResult PondNotConfigured
        {
            get
            {
                return new ApiResult(ResultCode.PondNotConfigured, "卡池未配置");
            }
        }

        /// <summary>
        /// 权限不足
        /// </summary>
        public static ApiResult PermissionDenied
        {
            get
            {
                return new ApiResult(ResultCode.PermissionDenied, "权限不足，请参考贴子获取独立授权码");
            }
        }



    }
}
