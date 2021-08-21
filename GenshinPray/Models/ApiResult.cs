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

        public ApiResult(int code,string message,T data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

        public static ApiResult<T> Success(T data)
        {
            return new ApiResult<T>(1, "ok", data);
        }


    }
}
