using SqlSugar;
using System;

namespace EAM.Listen.Model
{
    /// <summary>
    /// 厂区配置
    /// </summary>
    [SugarTable("BASE_Factory_Config")]
    public class FactoryConfig
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [SugarColumn(ColumnName = "config_key")]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置键值
        /// </summary>
        [SugarColumn(ColumnName = "config_value")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置值类型
        /// </summary>
        [SugarColumn(ColumnName = "config_type")]
        public string ConfigType { get; set; }

        /// <summary>
        /// 配置分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 配置描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [SugarColumn(ColumnName = "enable_flag")]
        public string EnableFlag { get; set; }

    }
}