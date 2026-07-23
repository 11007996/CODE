using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace webapi.sqlClass
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("P_REWO_BASE")]
    public partial class P_REWO_BASE
    {
           public P_REWO_BASE(){


           }
           /// <summary>
           /// Desc:工单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string WORK_ORDER {get;set;} = null!;

           /// <summary>
           /// Desc:工单类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_TYPE {get;set;}

           /// <summary>
           /// Desc:机种
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MODEL {get;set;}

           /// <summary>
           /// Desc:工厂料号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? IPN {get;set;}

           /// <summary>
           /// Desc:版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? VERSION {get;set;}

           /// <summary>
           /// Desc:SAP 品名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SPEC1 {get;set;}

           /// <summary>
           /// Desc:生产数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TARGET_QTY {get;set;}

           /// <summary>
           /// Desc:投入数量
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? INPUT_QTY {get;set;}

           /// <summary>
           /// Desc:产出数量
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? OUTPUT_QTY {get;set;}

           /// <summary>
           /// Desc:不良数量
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? NG_QTY {get;set;}

           /// <summary>
           /// Desc:报废数量
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? SCRAP_QTY {get;set;}

           /// <summary>
           /// Desc:工单创建日期
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_CREATE_DATE {get;set;}

           /// <summary>
           /// Desc:工单计划开始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_SCHEDULE_START_DATE {get;set;}

           /// <summary>
           /// Desc:工单计划结束日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_SCHEDULE_CLOSE_DATE {get;set;}

           /// <summary>
           /// Desc:工单开始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_START_DATE {get;set;}

           /// <summary>
           /// Desc:工单结束日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_CLOSE_DATE {get;set;}

           /// <summary>
           /// Desc:生产线
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? LINE {get;set;}

           /// <summary>
           /// Desc:生产线类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? LINE_TYPE {get;set;}

           /// <summary>
           /// Desc:途程名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ROUTE_NAME {get;set;}

           /// <summary>
           /// Desc:开始工序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? START_STATION_TYPE {get;set;}

           /// <summary>
           /// Desc:结束工序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? END_STATION_TYPE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SN_RULE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CARTON_RULE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? BOX_RULE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PALLET_RULE {get;set;}

           /// <summary>
           /// Desc:包装规则
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PKSPEC_NAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WORK_FLAG {get;set;}

           /// <summary>
           /// Desc:工单状态
           /// Default:1
           /// Nullable:True
           /// </summary>           
           public string? WO_STATUS {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_BUILD {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_CONFIG {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_PHSAE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_VERSION {get;set;}

           /// <summary>
           /// Desc:厂区
           /// Default:DEF
           /// Nullable:True
           /// </summary>           
           public string? SITE {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? REMARK {get;set;}

           /// <summary>
           /// Desc:仓库
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WAREHOUSE_NO {get;set;}

           /// <summary>
           /// Desc:储位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WAREHOUSE_LOCATION {get;set;}

           /// <summary>
           /// Desc:部门名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? DEPT_NAME {get;set;}

           /// <summary>
           /// Desc:工厂代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PLANT_CODE {get;set;}

           /// <summary>
           /// Desc:工作中心
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CHANGEEDFLAG {get;set;}

           /// <summary>
           /// Desc:工作中心代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WORK_CENTER {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WORK_CENTER_PLANT {get;set;}

           /// <summary>
           /// Desc:发布日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? RELEASEDATE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SEQID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? STORAGELOC {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PRIORITY {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MFGPACKTYPE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CUSTOMERDN {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ASSIGNNO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? UPDATE_EMPNO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? UPDATE_TIME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:DateTime.Now
           /// Nullable:True
           /// </summary>           
           public DateTime? CREATE_TIME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION2 {get;set;}

           /// <summary>
           /// Desc:线束研发版本号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION3 {get;set;}

           /// <summary>
           /// Desc:线束工艺变更号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION4 {get;set;}

           /// <summary>
           /// Desc:线束工单LOT号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION5 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? OPTION6 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WO_PURPOSE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? RULE_SET_NAME {get;set;}

           /// <summary>
           /// Desc:上级工单，父工单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? M_WO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? BURNIN_TIME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? COMPANY_NAME {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? EQUIPMENT_CODE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? RMA_NO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? RMA_CUSTOMER {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? RMA_ACCOUNT_VALUE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:N/A' 
           /// Nullable:False
           /// </summary>           
           public string PLANT {get;set;} = null!;

           /// <summary>
           /// Desc:基本计量单位
           /// Default:PCS
           /// Nullable:True
           /// </summary>           
           public string? BASE_UNIT_MEASURE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? MACHINE_GROUP {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? STATION_TYPE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ASSIGNSTATUS {get;set;}

           /// <summary>
           /// Desc:设计卡
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? DRAWNUM {get;set;}

           /// <summary>
           /// Desc:单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? UNIT {get;set;}

           /// <summary>
           /// Desc:定长
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FIXED_LENGTH {get;set;}

           /// <summary>
           /// Desc:盘数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DISK_NUMBER {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? NOTES {get;set;}

           /// <summary>
           /// Desc:包装材料
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? PACKAGING_MATERIALS {get;set;}

           /// <summary>
           /// Desc:设计卡版本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? DRAWNUM_VERSION {get;set;}

           /// <summary>
           /// Desc:车间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WORK_SHOP {get;set;}

           /// <summary>
           /// Desc:客户料号+客户名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CUSTOMERPART_CUSTOMERNAME {get;set;}

           /// <summary>
           /// Desc:项目行号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SALES_ORDER_ROW {get;set;}

           /// <summary>
           /// Desc:定长单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? FIXED_UNIT {get;set;}

           /// <summary>
           /// Desc:BOM版次
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? BOM_VER {get;set;}

           /// <summary>
           /// Desc:SO长文本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SO_TEXT {get;set;}

           /// <summary>
           /// Desc:送货地
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SEND_ADDRESS {get;set;}

           /// <summary>
           /// Desc:顶层工单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? TOP_WORK_ORDER {get;set;}

           /// <summary>
           /// Desc:扩展工单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? WORK_ORDER_EXT {get;set;}

           /// <summary>
           /// Desc:客户料号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CUSPART {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? UEBTO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? LONGTEXT {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_ASSING_START_DATE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? WO_ASSING_END_DATE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? SAPSTATUS {get;set;}

           /// <summary>
           /// Desc:订单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? ORDER_NO {get;set;}

           /// <summary>
           /// Desc:标识编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? IDENTIFICATION_NUMBER {get;set;}

           /// <summary>
           /// Desc:客户编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CUSTOMER_CODE {get;set;}

           /// <summary>
           /// Desc:客户订单号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CUSTOMER_PO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:True
           /// </summary>           
           public decimal? ISARRANGEMENT {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? POSITION {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? CIRCUITS {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CUTDATE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ASSEMBLYDATE {get;set;}

           /// <summary>
           /// Desc:客户料号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? APN {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? FREMARK {get;set;}

           /// <summary>
           /// Desc:重工工单
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string REWORK_ORDER {get;set;} = null!;

    }
}
