namespace EAM.ServiceCore.Model
{
    /// <summary>
    /// 集团员工信息视图
    /// </summary>
    [Tenant("EDH")]
    [SugarTable("EDH.V_YGJBXX_CPBG", "员工信息")]
    public class LuxEmp
    {
        [SugarColumn(ColumnName = "CLIENTID", ColumnDescription = "客户Id,888")]
        public string ClientId { get; set; }

        /// <summary>
        /// SAP编号
        /// </summary>

        [SugarColumn(ColumnName = "PERNR", ColumnDescription = "SAP编号")]
        public string SapCode { get; set; }

        /// <summary>
        /// 立讯编号(工号)
        /// </summary>

        [SugarColumn(ColumnName = "CPF01", ColumnDescription = "立讯编号")]
        public string EmpCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnName = "CPF02", ColumnDescription = "姓名")]
        public string EmpName { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>

        [SugarColumn(ColumnName = "CPF35", ColumnDescription = "离职日期")]
        public string LeaveDate { get; set; }

        /// <summary>
        /// 人事范围编号
        /// </summary>

        [SugarColumn(ColumnName = "WERKS", ColumnDescription = "人事范围")]
        public string PersonnelScope { get; set; }

        /// <summary>
        /// 人事范围名称
        /// </summary>
        [SugarColumn(ColumnName = "NAME1", ColumnDescription = "人事范围")]
        public string PersonnelScopeName { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        [SugarColumn(ColumnName = "CPF29", ColumnDescription = "部门")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 组织编号
        /// </summary>
        [SugarColumn(ColumnName = "ORGEH", ColumnDescription = "部门")]
        public string OrganCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "O_STEXT", ColumnDescription = "部门")]
        public string DeptName { get; set; }

        /// <summary>
        /// 直属主管编号
        /// </summary>

        [SugarColumn(ColumnName = "TA_CPF41", ColumnDescription = "直属主管")]
        public string DirectSupervisorCode { get; set; }
    }
}