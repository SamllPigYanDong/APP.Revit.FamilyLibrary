namespace Revit.Shared.Entity.Commons
{

    public class ApiResponse<T>
    {
        public T Content { get; set; }

        public bool Status
        {
            get
            {
                if (Code == ResponseCode.Success)
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


    public class ApiResponse
    {
        public ApiResponse()
        {
            Code = ResponseCode.Success;
            Message = "操作成功";
        }

        public ApiResponse(object content)
        {
            Code = ResponseCode.Success;
            Message = "操作成功";
            Content = content;
        }

        /// <summary>
        /// 响应代码
        /// </summary>
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 响应消息内容
        /// </summary>
        public string Message { get; set; }
        public object Content { get; set; }

        /// <summary>
        /// 设置响应状态为成功
        /// </summary>
        /// <param name="message"></param>
        public ApiResponse SetSuccess(string message = "成功")
        {
            Code = ResponseCode.Success;
            Message = message;

            return this;
        }

        /// <summary>
        /// 设置响应状态为错误
        /// </summary>
        /// <param name="message"></param>
        public ApiResponse SetError(string message = "错误")
        {
            Code = ResponseCode.Error;
            Message = message;

            return this;
        }

        /// <summary>
        /// 设置响应状态为未知资源
        /// </summary>
        /// <param name="message"></param>
        public ApiResponse SetNotFound(string message = "未知资源")
        {
            Message = message;
            Code = ResponseCode.NotFound;
            return this;
        }

        /// <summary>
        /// 设置响应状态为无权限
        /// </summary>
        /// <param name="message"></param>
        public ApiResponse SetNoPermission(string message = "无权限")
        {
            Message = message;
            Code = ResponseCode.NoPermission;
            return this;
        }

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
}
