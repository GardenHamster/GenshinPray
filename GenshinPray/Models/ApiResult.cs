using GenshinPray.Common;
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

        public static ApiResult Success(object data)
        {
            return new ApiResult(ResultCode.Success, "ok", data);
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
                return new ApiResult((int)HttpStatusCode.InternalServerError, "接口异常");
            }
        }

        /// <summary>
        /// 超过每日api调用上限
        /// </summary>
        public static ApiResult ApiMaximum
        {
            get
            {
                return new ApiResult(ResultCode.ApiMaximum, "今日api调用上限");
            }
        }



    }
}
