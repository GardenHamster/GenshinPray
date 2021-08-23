using GenshinPray.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenshinPray.Models
{
    public class ApiResult<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResult() { }

        public ApiResult(int code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ApiResult(int code,string message,T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>(ResultCode.Success, "ok", data);
        }

        public static ApiResult<T> Error(int code, string message)
        {
            return new ApiResult<T>(code, message);
        }

        public static ApiResult<T> Error(string message)
        {
            return new ApiResult<T>(ResultCode.Error, message);
        }

        /// <summary>
        /// 未授权
        /// </summary>
        public static ApiResult<T> Unauthorized
        {
            get
            {
                return new ApiResult<T>(ResultCode.Unauthorized, "授权码不存在或者已经过期");
            }
        }

        /// <summary>
        /// 未授权
        /// </summary>
        public static ApiResult<T> ServerError
        {
            get
            {
                return new ApiResult<T>(ResultCode.ServerError, "接口异常");
            }
        }


    }
}
