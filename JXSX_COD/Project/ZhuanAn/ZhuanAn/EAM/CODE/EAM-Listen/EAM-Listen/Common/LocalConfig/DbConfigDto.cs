namespace EAM.Listen.Common.Config
{
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    public class DbConfigDto
    {
        public string ConfigId { get; set; }

        public string Conn { get; set; }

        public int DbType { get; set; }

        public bool IsAutoCloseConnection { get; set; }
    }
}