using System.Text.Json;
using System.Text.Json.Serialization;

namespace HK.WebApi.Utilities
{
    /// <summary>
    /// 自定义 DateTime 转换器，将日期时间格式化为 yyyy-MM-dd HH:mm:ss
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 将字符串转换为 DateTime
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTime();
        }

        /// <summary>
        /// 将 DateTime 转换为字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // 将 UTC 时间转换为本地时间
            if (value.Kind == DateTimeKind.Utc)
            {
                value = value.ToLocalTime();
            }

            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    /// <summary>
    /// 自定义 DateTimeOffset 转换器
    /// </summary>
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        /// <summary>
        /// 将字符串转换为 DateTimeOffset
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTimeOffset();
        }

        /// <summary>
        /// 将 DateTimeOffset 转换为字符串
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            // 将 UTC 时间转换为本地时间
            writer.WriteStringValue(value.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}