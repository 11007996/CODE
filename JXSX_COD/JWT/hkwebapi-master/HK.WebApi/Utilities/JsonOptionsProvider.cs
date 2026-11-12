using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HK.WebApi.Utilities
{
    /// <summary>
    /// 手动序列化配置
    /// </summary>
    public static class JsonOptionsProvider
    {
        /// <summary>
        /// 全局统一JSON配置
        /// </summary>
        public static readonly JsonSerializerOptions GlobalJsonOptions = new()
        {
            // 驼峰命名
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            // 不转义非 ASCII 字符（如中文）
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            // 忽略空值 
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            // 处理循环引用
            //ReferenceHandler = ReferenceHandler.Preserve,
            // 支持字符串数字
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new JsonStringEnumConverter(),
                new DateTimeConverter(),
                new DateTimeOffsetConverter()
            }
        };
    }
}