namespace HK.WebApi.Models
{
    /// <summary>
    /// API 统一返回结果
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 状态码：0 表示成功，其他值表示失败
        /// </summary>
        public int Code { get; set; }
        
        /// <summary>
        /// 数据总数（用于分页）
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 成功返回结果
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="message">返回消息</param>
        /// <returns>ApiResult 对象</returns>
        public static ApiResult Success(object data = null, string message = "操作成功")
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Message = message
            };
        }

        /// <summary>
        /// 成功返回结果（带分页）
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="count">数据总数</param>
        /// <param name="message">返回消息</param>
        /// <returns>ApiResult 对象</returns>
        public static ApiResult Success(object data, int count, string message = "操作成功")
        {
            return new ApiResult
            {
                Code = 200,
                Data = data,
                Count = count,
                Message = message
            };
        }

        /// <summary>
        /// 失败返回结果
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="message">错误消息</param>
        /// <returns>ApiResult 对象</returns>
        public static ApiResult Fail(int code = 500, string message = "操作失败")
        {
            return new ApiResult
            {
                Code = code,
                Message = message
            };
        }
    }
}    