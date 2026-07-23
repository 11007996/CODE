namespace EAM.Model.Fixture
{
    /// <summary>
    /// 治具文件关联
    /// </summary>
    [SugarTable("FIX_Fixture_File")]
    public class FixtureFile
    {
        /// <summary>
        /// 治具ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "fixture_ID")]
        public int FixtureId { get; set; }

        /// <summary>
        /// 文件ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "file_ID")]
        public long FileId { get; set; }
    }
}