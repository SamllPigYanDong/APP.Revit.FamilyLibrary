using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revit.Entity.Entity
{
    public class ApiResponse<T>
    {
        public T Content { get; set; }

        public bool Status
        {
            get
            {
                if (Code== ResponseCode .Success)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 响应代码
        /// </summary>
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }

    }
    /// <summary>
    /// 响应状态码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,

        /// <summary>
        /// 未知资源
        /// </summary>
        NoPermission = 401,

        /// <summary>
        /// 无权限
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// 错误、异常
        /// </summary>
        Error = 500,
    }


    public class ApiResponse
    {
        public string Message { get; set; }

        public ResponseCode Code { get; set; }

        public object Result { get; set; }
    }

    
}
