/*
 Navicat Premium Data Transfer

 Source Server         : 172.18.20.82【二期设备】
 Source Server Type    : SQL Server
 Source Server Version : 11002100
 Source Host           : 172.18.20.82:1433
 Source Catalog        : HJDB
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 11002100
 File Encoding         : 65001

 Date: 14/07/2025 13:46:28
*/


-- ----------------------------
-- Table structure for A_AssetFile_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_AssetFile_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_AssetFile_T]
GO

CREATE TABLE [dbo].[A_AssetFile_T] (
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileId] int  NOT NULL,
  [FileClass] varchar(2) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[A_AssetFile_T] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for A_AssetInfo_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_AssetInfo_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_AssetInfo_T]
GO

CREATE TABLE [dbo].[A_AssetInfo_T] (
  [AssetNo] varchar(22) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FactoryCode] varchar(4) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AssetMainNo] varchar(12) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AssetSubNo] varchar(4) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AssetName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [AssetClass] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Model] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [EntryDate] date  NULL,
  [CostCenter] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [DurableYear] int  NULL,
  [DurableMonth] int  NULL,
  [MadeFactory] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ControlNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NOT NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_AssetInfo_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产主编号(工厂代码+资产主编号+资产子编号)',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'公司代码',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'FactoryCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产主编号',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'AssetMainNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产子编号',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'AssetSubNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产名称(资产描述)',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'AssetName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产分类名称',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'AssetClass'
GO

EXEC sp_addextendedproperty
'MS_Description', N'型号规格',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'Model'
GO

EXEC sp_addextendedproperty
'MS_Description', N'首次购置日期(进厂日期)',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'EntryDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'成本中心',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'CostCenter'
GO

EXEC sp_addextendedproperty
'MS_Description', N'耐用年限',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'DurableYear'
GO

EXEC sp_addextendedproperty
'MS_Description', N'耐用月数',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'DurableMonth'
GO

EXEC sp_addextendedproperty
'MS_Description', N'制造厂商（供应商名称）',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'MadeFactory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'校验管制编号',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'ControlNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新人工号',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'固定资产基本信息',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetInfo_T'
GO


-- ----------------------------
-- Table structure for A_AssetReceive_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_AssetReceive_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_AssetReceive_T]
GO

CREATE TABLE [dbo].[A_AssetReceive_T] (
  [AssetNo] varchar(22) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Line] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [StartTime] datetime  NOT NULL,
  [EndTime] datetime  NULL,
  [ReceiveMode] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReturnMode] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReceiveUser] varchar(15) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReturnUser] varchar(15) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_AssetReceive_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'领用开始时间',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'领用结束时间',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'领用的方式（A:主动，P:被动）',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'ReceiveMode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'归还的方式（A:主动，P:被动）',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'ReturnMode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'领取人',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'ReceiveUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'归还人',
'SCHEMA', N'dbo',
'TABLE', N'A_AssetReceive_T',
'COLUMN', N'ReturnUser'
GO


-- ----------------------------
-- Table structure for A_MaintenanceItem_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaintenanceItem_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_MaintenanceItem_T]
GO

CREATE TABLE [dbo].[A_MaintenanceItem_T] (
  [AssetNo] varchar(22) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [TimeMark] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ItemName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [SortNo] int  NULL
)
GO

ALTER TABLE [dbo].[A_MaintenanceItem_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceItem_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'日期标记【D日,W周,M:月,Q:季，Y年】',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceItem_T',
'COLUMN', N'TimeMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'项目名称',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceItem_T',
'COLUMN', N'ItemName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceItem_T',
'COLUMN', N'SortNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备维护项目',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceItem_T'
GO


-- ----------------------------
-- Table structure for A_MaintenanceReport_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaintenanceReport_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_MaintenanceReport_T]
GO

CREATE TABLE [dbo].[A_MaintenanceReport_T] (
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Year] int  NOT NULL,
  [Month] int  NULL,
  [Status] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_MaintenanceReport_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReport_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年标记',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReport_T',
'COLUMN', N'Year'
GO

EXEC sp_addextendedproperty
'MS_Description', N'月标记',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReport_T',
'COLUMN', N'Month'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报表状态(Y:正常，N：异常)',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReport_T',
'COLUMN', N'Status'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产保养报表',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReport_T'
GO


-- ----------------------------
-- Table structure for A_MaintenanceReportD_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaintenanceReportD_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_MaintenanceReportD_T]
GO

CREATE TABLE [dbo].[A_MaintenanceReportD_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Year] int  NOT NULL,
  [TimeMark] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [TimeMarkStamp] int  NOT NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NOT NULL,
  [IsVisible] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_MaintenanceReportD_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'年标记',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'Year'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间标记',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'TimeMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间标记戳',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'TimeMarkStamp'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新人员(工号)',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备保养报告详情',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportD_T'
GO


-- ----------------------------
-- Table structure for A_MaintenanceReportDV_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaintenanceReportDV_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_MaintenanceReportDV_T]
GO

CREATE TABLE [dbo].[A_MaintenanceReportDV_T] (
  [MRDId] int  NOT NULL,
  [ItemName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ItemValue] varchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_MaintenanceReportDV_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'保养记录ID',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportDV_T',
'COLUMN', N'MRDId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'保养项目',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportDV_T',
'COLUMN', N'ItemName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'项目值',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportDV_T',
'COLUMN', N'ItemValue'
GO


-- ----------------------------
-- Table structure for A_MaintenanceReportItem_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_MaintenanceReportItem_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_MaintenanceReportItem_T]
GO

CREATE TABLE [dbo].[A_MaintenanceReportItem_T] (
  [Year] int  NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [TimeMark] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ItemName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [SortNo] int  NOT NULL
)
GO

ALTER TABLE [dbo].[A_MaintenanceReportItem_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'年标记',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportItem_T',
'COLUMN', N'Year'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportItem_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'日期标记【D,W,M:月份(月制),YM:月份(年制),Q:季度，Y年】',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportItem_T',
'COLUMN', N'TimeMark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'项目名称',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportItem_T',
'COLUMN', N'ItemName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'A_MaintenanceReportItem_T',
'COLUMN', N'SortNo'
GO


-- ----------------------------
-- Table structure for A_ResumeMaintenanceD_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_ResumeMaintenanceD_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_ResumeMaintenanceD_T]
GO

CREATE TABLE [dbo].[A_ResumeMaintenanceD_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [ReportId] int  NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExecuteCategory] nvarchar(2) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ExecuteDate] datetime  NULL,
  [TakenDept] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ExecuteState] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [ExecuteUser] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [NextExecuteDate] datetime  NULL,
  [Remark] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_ResumeMaintenanceD_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'保养、校验实施记录ID',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'履历报表ID',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'ReportId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'实施类别（保养、校验）',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'ExecuteCategory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'实施日期',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'ExecuteDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'保管部门',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'TakenDept'
GO

EXEC sp_addextendedproperty
'MS_Description', N'实施状况',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'ExecuteState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'实施人员',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'ExecuteUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'下次实施日期',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'NextExecuteDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注（校验报告编号）',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新用户',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'保养、校验实施记录',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeMaintenanceD_T'
GO


-- ----------------------------
-- Table structure for A_ResumeRepairD_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_ResumeRepairD_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_ResumeRepairD_T]
GO

CREATE TABLE [dbo].[A_ResumeRepairD_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [ReportId] int  NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RepairDate] datetime  NULL,
  [AbnormalDesc] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RepairReason] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [RepairUser] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [CheckResult] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[A_ResumeRepairD_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'维修记录ID',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'履历报表ID',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'ReportId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'维修日期',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'RepairDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'异常描述',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'AbnormalDesc'
GO

EXEC sp_addextendedproperty
'MS_Description', N'维修原因',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'RepairReason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'维修人员',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'RepairUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'验收结果',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'CheckResult'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注／维修单号',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新用户',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'维修记录',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeRepairD_T'
GO


-- ----------------------------
-- Table structure for A_ResumeReport_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[A_ResumeReport_T]') AND type IN ('U'))
	DROP TABLE [dbo].[A_ResumeReport_T]
GO

CREATE TABLE [dbo].[A_ResumeReport_T] (
  [Id] int  IDENTITY(1000,1) NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [PageNo] int  NOT NULL
)
GO

ALTER TABLE [dbo].[A_ResumeReport_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'履历报表ID',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeReport_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产编号',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeReport_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页码',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeReport_T',
'COLUMN', N'PageNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'履历报表',
'SCHEMA', N'dbo',
'TABLE', N'A_ResumeReport_T'
GO


-- ----------------------------
-- Table structure for M_ErrorHandleScan_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ErrorHandleScan_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_ErrorHandleScan_T]
GO

CREATE TABLE [dbo].[M_ErrorHandleScan_T] (
  [ErrorId] int  NOT NULL,
  [HandlerNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [HandlerName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerLevel] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [ErrorStatus] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NOT NULL
)
GO

ALTER TABLE [dbo].[M_ErrorHandleScan_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'关联故障Id',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'ErrorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人编号',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'HandlerNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人姓名',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'HandlerName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人等级',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'HandlerLevel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'刷卡后的故障状态',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'ErrorStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T',
'COLUMN', N'CreateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障处理刷卡记录表',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorHandleScan_T'
GO


-- ----------------------------
-- Table structure for M_ErrorRecord_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ErrorRecord_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_ErrorRecord_T]
GO

CREATE TABLE [dbo].[M_ErrorRecord_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Area] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Dept] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Machine] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [CallReason] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MaxHandleTimes] int  NULL,
  [MaxHelpTimes] int  NULL,
  [StartTime] datetime  NOT NULL,
  [ComeTime] datetime  NULL,
  [CallHelpTime] datetime  NULL,
  [CallHelpMode] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [EndTime] datetime  NULL,
  [HandlerNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [HelperNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Status] char(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [ErrorReason] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [FaultType] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [FaultContent] nvarchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [SolutionContent] nvarchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [ProdCount] int  NULL,
  [SolverNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [SolverName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [PCIP] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [PCMac] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [PCUser] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NOT NULL,
  [TargetHandler] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [PassCount] int  NULL,
  [NGCount] int  NULL,
  [QCName] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsVisible] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_ErrorRecord_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'ID',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'厂区',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'线别',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Dept'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台(机台类型*机台编号)',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Machine'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫原因',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'CallReason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大处理时间（分钟）',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'MaxHandleTimes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大支援时间（分钟）',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'MaxHelpTimes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开始呼叫时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开始处理时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'ComeTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫支援时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'CallHelpTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫支援方式（0超时触发，1主动呼叫）',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'CallHelpMode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'结束处理时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人编号',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'HandlerNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'支援人编号',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'HelperNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'状态(A待处理 B处理中 C呼叫支援 D支援处理中 E确认待完成 N解除呼叫 Y已完成)',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'Status'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障原因',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'ErrorReason'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障类型',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'FaultType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障内容',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'FaultContent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'解决方案内容',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'SolutionContent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'制品跟踪数',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'ProdCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'解决人编号',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'SolverNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'解决人名称',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'SolverName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫电脑IP',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'PCIP'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫电脑MAC',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'PCMac'
GO

EXEC sp_addextendedproperty
'MS_Description', N'呼叫电脑用户',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'PCUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新人编号',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'指定人员',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'TargetHandler'
GO

EXEC sp_addextendedproperty
'MS_Description', N'良品',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'PassCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'不良品',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'NGCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品质确认人',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T',
'COLUMN', N'QCName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障记录表',
'SCHEMA', N'dbo',
'TABLE', N'M_ErrorRecord_T'
GO


-- ----------------------------
-- Table structure for M_FaultSolution_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_FaultSolution_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_FaultSolution_T]
GO

CREATE TABLE [dbo].[M_FaultSolution_T] (
  [MachineType] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FaultType] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FaultItems] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [SolutionItems] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [MaxHandleTimes] int  NULL,
  [MaxHelpTimes] int  NULL,
  [AutoHelpFlag] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [ImitateSound] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_FaultSolution_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台类型',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'MachineType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障类型',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'FaultType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障内容',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'FaultItems'
GO

EXEC sp_addextendedproperty
'MS_Description', N'解决方案',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'SolutionItems'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大处理时间',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'MaxHandleTimes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大支援时间',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'MaxHelpTimes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'自动支援标记',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'AutoHelpFlag'
GO

EXEC sp_addextendedproperty
'MS_Description', N'模拟声音',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T',
'COLUMN', N'ImitateSound'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障解决方案表',
'SCHEMA', N'dbo',
'TABLE', N'M_FaultSolution_T'
GO


-- ----------------------------
-- Table structure for M_LineMachines_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_LineMachines_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_LineMachines_T]
GO

CREATE TABLE [dbo].[M_LineMachines_T] (
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Machine] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[M_LineMachines_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'产线',
'SCHEMA', N'dbo',
'TABLE', N'M_LineMachines_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台（机台类型&机台编号）',
'SCHEMA', N'dbo',
'TABLE', N'M_LineMachines_T',
'COLUMN', N'Machine'
GO

EXEC sp_addextendedproperty
'MS_Description', N'产线机台关联表',
'SCHEMA', N'dbo',
'TABLE', N'M_LineMachines_T'
GO


-- ----------------------------
-- Table structure for M_LineQC_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_LineQC_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_LineQC_T]
GO

CREATE TABLE [dbo].[M_LineQC_T] (
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [QCName] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [QCWorkCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_LineQC_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'产线名称',
'SCHEMA', N'dbo',
'TABLE', N'M_LineQC_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'绑定品管',
'SCHEMA', N'dbo',
'TABLE', N'M_LineQC_T',
'COLUMN', N'QCName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'品管工号',
'SCHEMA', N'dbo',
'TABLE', N'M_LineQC_T',
'COLUMN', N'QCWorkCode'
GO


-- ----------------------------
-- Table structure for M_Machine_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Machine_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_Machine_T]
GO

CREATE TABLE [dbo].[M_Machine_T] (
  [MachineCode] int  IDENTITY(1,1) NOT NULL,
  [AssetNo] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MachineName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MachineNo] int  NULL,
  [TheoryCT] decimal(10,3)  NULL,
  [Power] decimal(10,3)  NULL,
  [MachineCategory] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [IsLink] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [MCode] int  NULL
)
GO

ALTER TABLE [dbo].[M_Machine_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码(设备类型代码)',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'资产编码',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'AssetNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备名称',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'理论CT',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'TheoryCT'
GO

EXEC sp_addextendedproperty
'MS_Description', N'功率(KW)',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'Power'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备类别',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineCategory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否连接',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'IsLink'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关联线体',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台设备清单',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T'
GO


-- ----------------------------
-- Table structure for M_MachineReport_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MachineReport_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MachineReport_T]
GO

CREATE TABLE [dbo].[M_MachineReport_T] (
  [MachineCode] int  NULL,
  [OperateCode] int  NULL,
  [RunState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [LineCode] int  NULL,
  [ProductCount] int  NULL,
  [FailedCount] int  NULL,
  [WarnState] int  NULL,
  [WarnCode] int  NULL,
  [CreateTime] datetime  NOT NULL,
  [RemoteEndPoint] varchar(30) COLLATE Chinese_PRC_CI_AS  NULL,
  [HexCode] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [ReturnHexCode] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_MachineReport_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'MachineCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'操作命令',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'OperateCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运行状态',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'RunState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'线别编号',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'LineCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'产能(包含不良)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'ProductCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'不良',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'FailedCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警状态',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'WarnState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警代码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'WarnCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间(上报时间)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'CreateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'远端节点（数据来源）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'RemoteEndPoint'
GO

EXEC sp_addextendedproperty
'MS_Description', N'16进制原码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'HexCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'返回的16进制字符',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'ReturnHexCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机器上报的数据',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T'
GO


-- ----------------------------
-- Table structure for M_MachineReportRefine_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MachineReportRefine_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MachineReportRefine_T]
GO

CREATE TABLE [dbo].[M_MachineReportRefine_T] (
  [Id] int  NOT NULL,
  [MCode] int  NOT NULL,
  [MachineNo] int  NULL,
  [RunState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [CT] real  NULL,
  [ProductCount] int  NULL,
  [FailedCount] int  NULL,
  [WarnState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [WarnCode] varchar(2) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NOT NULL,
  [RemoteEndPoint] varchar(30) COLLATE Chinese_PRC_CI_AS  NULL,
  [HexCode] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_MachineReportRefine_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'MCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号（数字编号）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'MachineNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运行状态',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'RunState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'CT(秒)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'CT'
GO

EXEC sp_addextendedproperty
'MS_Description', N'产能(包含不良)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'ProductCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'不良',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'FailedCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警状态',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'WarnState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警代码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'WarnCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'创建时间(上报时间)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'CreateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'远端节点（数据来源）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'RemoteEndPoint'
GO

EXEC sp_addextendedproperty
'MS_Description', N'16进制原码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T',
'COLUMN', N'HexCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机器上报的数据(过滤后)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReportRefine_T'
GO


-- ----------------------------
-- Table structure for M_MachineRunStat_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MachineRunStat_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MachineRunStat_T]
GO

CREATE TABLE [dbo].[M_MachineRunStat_T] (
  [FixDate] date  NOT NULL,
  [MCode] int  NOT NULL,
  [MachineNo] int  NOT NULL,
  [ErrorCount] int  NULL,
  [ProductCount] int  NULL,
  [FailedCount] int  NULL,
  [RunSeconds] int  NULL,
  [ErrorSeconds] int  NULL,
  [PlanRestSeconds] int  NULL,
  [TimeUtilizeRate] decimal(10,2)  NULL,
  [EfficacyUtilizeRate] decimal(10,2)  NULL,
  [PassRate] decimal(10,2)  NULL,
  [OEE] decimal(10,2)  NULL
)
GO

ALTER TABLE [dbo].[M_MachineRunStat_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'固定日期',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'FixDate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'MCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'MachineNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障次数',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'ErrorCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'总产量（含不良品）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'ProductCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'不良品数量',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'FailedCount'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运行时间（秒）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'RunSeconds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'故障时间(秒)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'ErrorSeconds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'计划停机时间(秒)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'PlanRestSeconds'
GO

EXEC sp_addextendedproperty
'MS_Description', N'时间稼动率',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'TimeUtilizeRate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'性能稼动率',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'EfficacyUtilizeRate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'良品率',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'PassRate'
GO

EXEC sp_addextendedproperty
'MS_Description', N'综合稼动率',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineRunStat_T',
'COLUMN', N'OEE'
GO


-- ----------------------------
-- Table structure for M_MachineType_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MachineType_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MachineType_T]
GO

CREATE TABLE [dbo].[M_MachineType_T] (
  [MachineType] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[M_MachineType_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台类型名称',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineType_T',
'COLUMN', N'MachineType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'机台类型表',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineType_T'
GO


-- ----------------------------
-- Table structure for M_MachineWarnCode_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MachineWarnCode_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MachineWarnCode_T]
GO

CREATE TABLE [dbo].[M_MachineWarnCode_T] (
  [MachineCode] int  NOT NULL,
  [WarnCode] varchar(2) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [WarnDesc] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[M_MachineWarnCode_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineWarnCode_T',
'COLUMN', N'MachineCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警代码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineWarnCode_T',
'COLUMN', N'WarnCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警详情',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineWarnCode_T',
'COLUMN', N'WarnDesc'
GO

EXEC sp_addextendedproperty
'MS_Description', N'报警代码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineWarnCode_T'
GO


-- ----------------------------
-- Table structure for M_MsgReceiver_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_MsgReceiver_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_MsgReceiver_T]
GO

CREATE TABLE [dbo].[M_MsgReceiver_T] (
  [Area] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [StageType] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [WorkCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Dept] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_MsgReceiver_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'区域',
'SCHEMA', N'dbo',
'TABLE', N'M_MsgReceiver_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'阶段（1：呼叫支援,2：支援超时）',
'SCHEMA', N'dbo',
'TABLE', N'M_MsgReceiver_T',
'COLUMN', N'StageType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'接收消息人工号',
'SCHEMA', N'dbo',
'TABLE', N'M_MsgReceiver_T',
'COLUMN', N'WorkCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'部门',
'SCHEMA', N'dbo',
'TABLE', N'M_MsgReceiver_T',
'COLUMN', N'Dept'
GO

EXEC sp_addextendedproperty
'MS_Description', N'消息接收配置表',
'SCHEMA', N'dbo',
'TABLE', N'M_MsgReceiver_T'
GO


-- ----------------------------
-- Table structure for M_PlanTime_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_PlanTime_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_PlanTime_T]
GO

CREATE TABLE [dbo].[M_PlanTime_T] (
  [PlanName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [StartTime] time(1)  NULL,
  [EndTime] time(1)  NULL,
  [MaxSeconds] int  NULL
)
GO

ALTER TABLE [dbo].[M_PlanTime_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'计划休息时间名称',
'SCHEMA', N'dbo',
'TABLE', N'M_PlanTime_T',
'COLUMN', N'PlanName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'开始时间',
'SCHEMA', N'dbo',
'TABLE', N'M_PlanTime_T',
'COLUMN', N'StartTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'结束时间',
'SCHEMA', N'dbo',
'TABLE', N'M_PlanTime_T',
'COLUMN', N'EndTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最大生效的时间(秒)',
'SCHEMA', N'dbo',
'TABLE', N'M_PlanTime_T',
'COLUMN', N'MaxSeconds'
GO


-- ----------------------------
-- Table structure for O_MachineDistribute_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[O_MachineDistribute_T]') AND type IN ('U'))
	DROP TABLE [dbo].[O_MachineDistribute_T]
GO

CREATE TABLE [dbo].[O_MachineDistribute_T] (
  [PointName] nvarchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Count] int  NULL
)
GO

ALTER TABLE [dbo].[O_MachineDistribute_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'点位',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineDistribute_T',
'COLUMN', N'PointName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备数量',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineDistribute_T',
'COLUMN', N'Count'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备分布',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineDistribute_T'
GO


-- ----------------------------
-- Table structure for O_MachineState_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[O_MachineState_T]') AND type IN ('U'))
	DROP TABLE [dbo].[O_MachineState_T]
GO

CREATE TABLE [dbo].[O_MachineState_T] (
  [StateName] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Count] int  NULL
)
GO

ALTER TABLE [dbo].[O_MachineState_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'状态名称',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineState_T',
'COLUMN', N'StateName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'状态数量',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineState_T',
'COLUMN', N'Count'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备维护状态',
'SCHEMA', N'dbo',
'TABLE', N'O_MachineState_T'
GO


-- ----------------------------
-- Table structure for S_ContactPerson_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_ContactPerson_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_ContactPerson_T]
GO

CREATE TABLE [dbo].[S_ContactPerson_T] (
  [WorkCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RealName] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[S_ContactPerson_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'工号',
'SCHEMA', N'dbo',
'TABLE', N'S_ContactPerson_T',
'COLUMN', N'WorkCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'S_ContactPerson_T',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系人表',
'SCHEMA', N'dbo',
'TABLE', N'S_ContactPerson_T'
GO


-- ----------------------------
-- Table structure for S_FileInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_FileInfo]') AND type IN ('U'))
	DROP TABLE [dbo].[S_FileInfo]
GO

CREATE TABLE [dbo].[S_FileInfo] (
  [FileID] int  IDENTITY(10000,1) NOT NULL,
  [FileName] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileExtension] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [FileAliasName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [FileSize] bigint  NULL,
  [FileClass] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Enabled] varchar(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NULL,
  [UpdateUser] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[S_FileInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件名',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'FileName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件后缀类型',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'FileExtension'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件别名',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'FileAliasName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件大小(字节)',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'FileSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件分类',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'FileClass'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可用',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'Enabled'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新用户',
'SCHEMA', N'dbo',
'TABLE', N'S_FileInfo',
'COLUMN', N'UpdateUser'
GO


-- ----------------------------
-- Table structure for S_LineInfo_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_LineInfo_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_LineInfo_T]
GO

CREATE TABLE [dbo].[S_LineInfo_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Factory] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Area] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[S_LineInfo_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'Id',
'SCHEMA', N'dbo',
'TABLE', N'S_LineInfo_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'工厂',
'SCHEMA', N'dbo',
'TABLE', N'S_LineInfo_T',
'COLUMN', N'Factory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区域',
'SCHEMA', N'dbo',
'TABLE', N'S_LineInfo_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'线别',
'SCHEMA', N'dbo',
'TABLE', N'S_LineInfo_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'产线信息',
'SCHEMA', N'dbo',
'TABLE', N'S_LineInfo_T'
GO


-- ----------------------------
-- Table structure for S_PreviewFileInfo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_PreviewFileInfo]') AND type IN ('U'))
	DROP TABLE [dbo].[S_PreviewFileInfo]
GO

CREATE TABLE [dbo].[S_PreviewFileInfo] (
  [FileId] int  IDENTITY(10000,1) NOT NULL,
  [SourceFileId] int  NOT NULL,
  [PageNo] int  NOT NULL,
  [FileName] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileExtension] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileSize] bigint  NOT NULL,
  [PreviewType] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileAliasName] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[S_PreviewFileInfo] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'预览文件ID',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'FileId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'源文件ID',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'SourceFileId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'页码',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'PageNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预览文件名',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'FileName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预览文件后缀',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'FileExtension'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预览文件大小',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'FileSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'预览类型(1:高清，2:模糊)',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'PreviewType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件别名',
'SCHEMA', N'dbo',
'TABLE', N'S_PreviewFileInfo',
'COLUMN', N'FileAliasName'
GO


-- ----------------------------
-- Table structure for S_SysDic
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_SysDic]') AND type IN ('U'))
	DROP TABLE [dbo].[S_SysDic]
GO

CREATE TABLE [dbo].[S_SysDic] (
  [Id] int  NOT NULL,
  [Catalog] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Desc] nvarchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [SortNo] int  NULL,
  [AppName] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[S_SysDic] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'目录id',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目录名称',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic',
'COLUMN', N'Catalog'
GO

EXEC sp_addextendedproperty
'MS_Description', N'描述',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic',
'COLUMN', N'Desc'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic',
'COLUMN', N'SortNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'应用',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic',
'COLUMN', N'AppName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据字典目录',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDic'
GO


-- ----------------------------
-- Table structure for S_SysDicDetial
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_SysDicDetial]') AND type IN ('U'))
	DROP TABLE [dbo].[S_SysDicDetial]
GO

CREATE TABLE [dbo].[S_SysDicDetial] (
  [Id] int  IDENTITY(1000,1) NOT NULL,
  [CatalogId] int  NOT NULL,
  [DataKey] varchar(255) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [DataValue] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
  [SortNo] int  NULL
)
GO

ALTER TABLE [dbo].[S_SysDicDetial] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'目录Id',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial',
'COLUMN', N'CatalogId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据值',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial',
'COLUMN', N'DataKey'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据说明',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial',
'COLUMN', N'DataValue'
GO

EXEC sp_addextendedproperty
'MS_Description', N'排序',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial',
'COLUMN', N'SortNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'数据字典详情',
'SCHEMA', N'dbo',
'TABLE', N'S_SysDicDetial'
GO


-- ----------------------------
-- Table structure for S_SystemFile_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_SystemFile_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_SystemFile_T]
GO

CREATE TABLE [dbo].[S_SystemFile_T] (
  [FileId] int  IDENTITY(10000,1) NOT NULL,
  [FileName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileSize] int  NOT NULL,
  [FileVersion] varchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileType] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileContent] varbinary(max)  NOT NULL,
  [FileTime] datetime  NOT NULL,
  [ProgramVersion] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateStatus] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateMode] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Remark] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [UseFlag] char(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL,
  [UpdateTime] datetime  NOT NULL,
  [IsUpdateApp] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[S_SystemFile_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件编号',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件名称（包含后缀）',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件大小(字节)',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileSize'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件版本(无版本的自增1)',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileVersion'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件类型',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'二进制文件',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileContent'
GO

EXEC sp_addextendedproperty
'MS_Description', N'文件上传时间',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'FileTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'应用版本',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'ProgramVersion'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新状态（Y:可更新，N:不可更新）',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'UpdateStatus'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新方式（0强制，1可选）',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'UpdateMode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注（更新说明）',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可用(Y:是，N:否)',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'UseFlag'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新人',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'更新时间',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否更新程序',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T',
'COLUMN', N'IsUpdateApp'
GO

EXEC sp_addextendedproperty
'MS_Description', N'系统文件表',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T'
GO


-- ----------------------------
-- Table structure for S_User_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_User_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_User_T]
GO

CREATE TABLE [dbo].[S_User_T] (
  [UserNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [Dept] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Pwd] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserRight] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [UserLevel] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Area] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UseFlag] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NULL,
  [ImageUpdateTime] datetime  NULL,
  [UserImage] image  NULL
)
GO

ALTER TABLE [dbo].[S_User_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户工号',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户名字',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户部门',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'Dept'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户状态（W等待中 H对应中 其它）',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户密码',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'Pwd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限（A：超级管理员 B：一般管理员）',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserRight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人级别（1：工程师 2:高级工程师）',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserLevel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'责任区域',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可用（ Y:账号可用，N不可用）',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UseFlag'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新人工号',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新头像时间',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'ImageUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'用户头像图片',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T',
'COLUMN', N'UserImage'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人信息表',
'SCHEMA', N'dbo',
'TABLE', N'S_User_T'
GO


-- ----------------------------
-- Table structure for S_UserInfo_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_UserInfo_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_UserInfo_T]
GO

CREATE TABLE [dbo].[S_UserInfo_T] (
  [WorkCode] varchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UserName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Token] varchar(1000) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [CreateTime] datetime  NULL,
  [ExpiresTime] datetime  NULL,
  [UserRight] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[S_UserInfo_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'工号',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'WorkCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'UserName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'访问Token(保养)',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'Token'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Token创建时间',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'CreateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'Token有效时间',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'ExpiresTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备系统的权限标志',
'SCHEMA', N'dbo',
'TABLE', N'S_UserInfo_T',
'COLUMN', N'UserRight'
GO


-- ----------------------------
-- Table structure for S_WxMessage_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_WxMessage_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_WxMessage_T]
GO

CREATE TABLE [dbo].[S_WxMessage_T] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Title] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Content] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [WorkCodes] varchar(max) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MsgType] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [LinkUrl] varchar(500) COLLATE Chinese_PRC_CI_AS  NULL,
  [Articles] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [SendTime] datetime  NOT NULL,
  [ResultMsg] nvarchar(max) COLLATE Chinese_PRC_CI_AS  NULL,
  [ErrorId] int  NULL,
  [Remark] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[S_WxMessage_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键ID',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'消息标题',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'Title'
GO

EXEC sp_addextendedproperty
'MS_Description', N'消息内容(富文本)',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'Content'
GO

EXEC sp_addextendedproperty
'MS_Description', N'工号(多个逗号隔开)',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'WorkCodes'
GO

EXEC sp_addextendedproperty
'MS_Description', N'消息类型(text:文本 ,textcard:模板,news:图文)',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'MsgType'
GO

EXEC sp_addextendedproperty
'MS_Description', N'链接（textcard类型消息）',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'LinkUrl'
GO

EXEC sp_addextendedproperty
'MS_Description', N'图文集合，最多6组（news类型消息）',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'Articles'
GO

EXEC sp_addextendedproperty
'MS_Description', N'发送时间',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'SendTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'发送结果',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'ResultMsg'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关联的故障ID',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'ErrorId'
GO

EXEC sp_addextendedproperty
'MS_Description', N'备注',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T',
'COLUMN', N'Remark'
GO

EXEC sp_addextendedproperty
'MS_Description', N'企业微信发送记录表',
'SCHEMA', N'dbo',
'TABLE', N'S_WxMessage_T'
GO


-- ----------------------------
-- function structure for SplitString
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[SplitString]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP FUNCTION[dbo].[SplitString]
GO

CREATE FUNCTION [dbo].[SplitString]
(
    @String VARCHAR(MAX),
    @Delimiter CHAR(1)
)
RETURNS @Result TABLE (Value VARCHAR(MAX))
AS
BEGIN
    DECLARE @StartPosition INT, @EndPosition INT

    SET @StartPosition = 1
    SET @EndPosition = CHARINDEX(@Delimiter, @String)

    WHILE @EndPosition > 0
    BEGIN
        INSERT INTO @Result (Value)
        SELECT SUBSTRING(@String, @StartPosition, @EndPosition - @StartPosition)

        SET @StartPosition = @EndPosition + 1
        SET @EndPosition = CHARINDEX(@Delimiter, @String, @StartPosition)
    END

    INSERT INTO @Result (Value)
    SELECT SUBSTRING(@String, @StartPosition, LEN(@String) - @StartPosition + 1)

    RETURN
END
GO


-- ----------------------------
-- procedure structure for AddMaintenanceRecord
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[AddMaintenanceRecord]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[AddMaintenanceRecord]
GO

CREATE PROCEDURE [dbo].[AddMaintenanceRecord](@AssetNo VARCHAR(50),@RYear INT,@RMonth INT,@TimeMark VARCHAR(1),@TimeMarkValue VARCHAR(2),@SignUserName VARCHAR(20),@UpdateUserId VARCHAR(20),@IsForce VARCHAR(1), @RestCode VARCHAR(1) OUTPUT, @RestMsg VARCHAR(100) OUTPUT)
AS
BEGIN

	/**添加指定维护记录
	**20230628 刘文波
	--@AssetNo 				资产编号
	--@RYear  				报表年份
	--@RMonth					报表月份
	--@TimeMark				日期标志
	--@TimeMarkValue	日期标志值
	--@SignUserName		保养人签名
	--@UpdateUserId		更新人
	--@IsForce				是否强制更新（Y：是，N:否）
	--@RestCode				返回结果代码：0成功，其他失败
	--@RestMsg				返回结果说明
	**/

	---------------------------------参数检查---------------------------------
	IF(ISNULL(@AssetNo,'')='')
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='资产编号不能为空'
		RETURN
	END
	
	IF(ISNULL(@RYear,'')='')
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='年份不能为空'
		RETURN
	END
	
	IF(ISNULL(@TimeMark,'')='')
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='日期标志不能为空'
		RETURN
	END
	
	IF(ISNULL(@TimeMarkValue,'')='')
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='日期标志值不能为空'
		RETURN
	END
	
	------------------------------转换标记值----------------------------------------
	DECLARE @TimeMarkStamp INT;
	--记算得到日期所年中的标记值
	--日
	IF @TimeMark='D'
	BEGIN
		SET @TimeMarkStamp=DATEPART(dayofyear, CONCAT(@RYear,'-',@RMonth,'-',@TimeMarkValue));
	END
	--周
	IF @TimeMark='W'
	BEGIN
	  SET DATEFIRST 1;--设置以'星期一'开始计算周数
		SET @TimeMarkStamp=DATEPART(Week, CONCAT(@RYear,'-',@RMonth,'-','1'));--本月的第一天所属性周的标记
		SET @TimeMarkStamp=@TimeMarkStamp + (@TimeMarkValue-1);
	END
	--月
	IF @TimeMark='M' AND ISNULL(@RMonth,'')<>''
	BEGIN
		SET @TimeMarkStamp=@RMonth;
	END
	--年表的月、季、年
	IF (@TimeMark='M' AND ISNULL(@RMonth,'')=''  )  OR @TimeMark='Q' OR @TimeMark='Y' 
	BEGIN
		SET @TimeMarkStamp=@TimeMarkValue;
	END
	
	-----------------------------------条件判断[检查报表是否存在]--------------------------------
	--查找相关报表
	SELECT 1 FROM A_MaintenanceReport_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND ISNULL([Month],'')=@RMonth;

	--判断报表是否存在
	IF NOT EXISTS(SELECT 1 FROM A_MaintenanceReport_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND ISNULL([Month],'')=@RMonth)
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='未找到相关报表'
		RETURN
	END

	------------------------------执行添加更新--------------------------------------------
	--------------添加记录相关数据-------------------------
	DECLARE @MRDId INT;
	--获取记录信息
	SELECT @MRDId=Id  FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark=@TimeMark AND TimeMarkStamp=@TimeMarkStamp;
	--如果不存在就添加记录
	IF ISNULL(@MRDId,'')=''
	BEGIN
		INSERT INTO  A_MaintenanceReportD_T(AssetNo,[Year],TimeMark,TimeMarkStamp,UpdateUser,UpdateTime) 	VALUES(@AssetNo,@RYear,@TimeMark,@TimeMarkStamp,@UpdateUserId,GETDATE()) 
	  SELECT @MRDId= SCOPE_IDENTITY()
	END

--查找保养项目
	SELECT ItemName,SortNo INTO #MRI FROM A_MaintenanceReportItem_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark=@TimeMark AND ItemName<>'保养人签名' ;

	--------------添加记录值相关数据-------------------------
	IF(@IsForce<>'Y')
	BEGIN
		 --不覆盖 
		 --删除没有值的项目
		 DELETE A_MaintenanceReportDV_T  WHERE MRDId=@MRDId AND ISNULL(ItemValue, '')='';
		 --插入数据
		 INSERT INTO A_MaintenanceReportDV_T(MRDId,ItemName,ItemValue) SELECT @MRDId,ItemName,'V' FROM #MRI  WHERE ItemName NOT IN(SELECT ItemName FROM A_MaintenanceReportDV_T WHERE MRDId=@MRDId ) ORDER BY SortNo;
		 --判断是否有【保养人签名】项目，没有追加
		 IF NOT EXISTS (SELECT 1 FROM A_MaintenanceReportDV_T WHERE MRDId=@MRDId AND ItemName='保养人签名')
		 BEGIN
				INSERT INTO A_MaintenanceReportDV_T(MRDId,ItemName,ItemValue)VALUES (@MRDId,'保养人签名',@SignUserName);
		 END
	END
	ELSE
	BEGIN
		--强制覆盖
		--删除原有的记录数据
		DELETE A_MaintenanceReportDV_T WHERE MRDId=@MRDId;
		--插入新数据
		INSERT INTO A_MaintenanceReportDV_T(MRDId,ItemName,ItemValue) SELECT @MRDId,ItemName,'V' FROM #MRI ORDER BY SortNo;
		--追加【保养人签名】项目
		INSERT INTO A_MaintenanceReportDV_T(MRDId,ItemName,ItemValue)VALUES (@MRDId,'保养人签名',@SignUserName);
	END

	-----返回执行结果-----
	SET @RestCode='0'
	SET @RestMsg='添加保养记录成功'

END
GO


-- ----------------------------
-- procedure structure for GetNotHoliDay
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[GetNotHoliDay]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[GetNotHoliDay]
GO

CREATE PROCEDURE [dbo].[GetNotHoliDay](@RYear INT,@RMonth  INT,@DayCount INT OUTPUT,@Days VARCHAR(300) OUTPUT,@RestCode VARCHAR(1) OUTPUT, @RestMsg VARCHAR(100) OUTPUT)
AS
BEGIN
  --获取指定月份的非节假日的日期值
	
	SET @DayCount=0;
	SET @Days='';
	--------------------------参数检查-------------------------------
	IF(ISNULL(@RYear,'')='')
	BEGIN
		SET @RestCode='1';
		SET @RestMsg='未指定年份';
		RETURN 		
	END
	
	IF(ISNULL(@RMonth,'')='')
	BEGIN
			SET @RestCode='1';
			SET @RestMsg='未指定月份';
			RETURN 		
	END
	
	------------------------- 获取有效天数，及日期--------------------------------------
	DECLARE @CurrYear INT= YEAR(GETDATE());--当年
	DECLARE @CurrMonth INT =MONTH(GETDATE());--当月
	DECLARE @MaxDayCount INT = 0;--指定月份最大有效天数
	DECLARE @DayIndex INT = 1;--天数索引
	DECLARE @DataDicId INT ;--数据字典目录ID
	DECLARE @IsHoliday VARCHAR(1);--是否休息日，Y:是，N:否
	DECLARE @RDay VARCHAR(20);--循环的日期

	--大于当年当月
	IF @RYear>@CurrYear OR(@RYear=@CurrYear AND @RMonth>@CurrMonth)
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='年月超出当前时间'
		RETURN 		
	END
	
	--设置当月最大天数
	IF(@RYear=@CurrYear AND @RMonth=@CurrMonth)
	BEGIN
		--当年当月
		SET @MaxDayCount=DAY(GETDATE());
	END
	ELSE
	BEGIN
		--小于当年当月
		SET @MaxDayCount= DAY(dateadd(month,1,CONCAT(@RYear,'-',@RMonth,'-','01'))-1) 
	END
	
	--获取系统数据字典设置的节日
	SELECT @DataDicId =Id FROM S_SysDic WHERE [Catalog]='HOLIDAY_DATE_DAY';
	SELECT * INTO #SDD FROM S_SysDicDetial WHERE CatalogId=ISNULL(@DataDicId,'') AND Year(DataKey)=@RYear AND Month(DataKey)=@RMonth ;


	--遍历有效天数的每一天
	WHILE @DayIndex<=@MaxDayCount BEGIN
		 SET @RDay =CONCAT(@RYear,'-',@RMonth,'-',@DayIndex);
		 SELECT @IsHoliday = DataValue FROM #SDD WHERE Day(DataKey)=@DayIndex

		 --是否节日
		 IF(ISNULL(@IsHoliday,'')<>'')
		 BEGIN
			--优化根据系统设置
			--不是节日
			IF(@IsHoliday<>'Y')
			BEGIN
				SET @DayCount=@DayCount+1;
				SET @Days=CONCAT(@Days,DATEPART(dayofyear, @RDay),',')
			END
		 END
		 ELSE
		 BEGIN
			--系统未指定当日是否节日
			--判断是否周日
				IF( DATENAME(weekday, @RDay) NOT IN('星期日'))
				BEGIN
					SET @DayCount=@DayCount+1;
					SET @Days=CONCAT(@Days,DATEPART(dayofyear, @RDay),',')
				END
		 END
		 
		 SET @DayIndex=@DayIndex+1;
		 SET @IsHoliday='';
	END
	
	--去掉最后的逗号
	IF(LEN(@Days)>1)
	BEGIN
		SET @Days=LEFT(@Days, LEN(@Days)-1);
	END

	PRINT @DayCount
	PRINT @Days
	SET @RestCode='0';
	SET @RestMsg='成功';
	RETURN 		

END
GO


-- ----------------------------
-- procedure structure for GetWorkWeek
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[GetWorkWeek]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[GetWorkWeek]
GO

CREATE PROCEDURE [dbo].[GetWorkWeek](@RYear INT,@RMonth  INT,@WeekCount INT OUTPUT,@Weeks VARCHAR(300) OUTPUT,@RestCode VARCHAR(1) OUTPUT, @RestMsg VARCHAR(100) OUTPUT)
AS
BEGIN
  --获取指定月份的周数及期周值
	
	SET @WeekCount=0;
	SET @Weeks='';
	--------------------------参数检查-------------------------------
	IF(ISNULL(@RYear,'')='')
	BEGIN
		SET @RestCode='1';
		SET @RestMsg='未指定年份';
		RETURN 		
	END
	
	IF(ISNULL(@RMonth,'')='')
	BEGIN
			SET @RestCode='1';
			SET @RestMsg='未指定月份';
			RETURN 		
	END
	
	------------------------- 获取有效天数，及日期--------------------------------------
	DECLARE @CurrYear INT= YEAR(GETDATE());--当年
	DECLARE @CurrMonth INT =MONTH(GETDATE());--当月
	DECLARE @MaxDay INT;   --有效的最大天数
	DECLARE @FirstDay VARCHAR(20);   --月的第一天日期
	DECLARE @LastDay VARCHAR(20);   --月的有效最后一天日期
	DECLARE @WeekIndex INT ;--周数索引(月的第一天的周相对年的周数)
	DECLARE @MaxWeekIndex INT ; --周数索引(月的最大有效星期相对年的周数)

	SET DATEFIRST 1 --设置以星期一开始计算周的数值
	--大于当年当月
	IF @RYear>@CurrYear OR(@RYear=@CurrYear AND @RMonth>@CurrMonth)
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='日期超出当前年月'
		RETURN
	END
	
	--设置指定月的 第一天所在周数与有效的最后一天所在周数
	IF(@RYear=@CurrYear AND @RMonth=@CurrMonth)
	BEGIN
		--当年当月
		SET @MaxDay=DAY(GETDATE());
	END
	ELSE
	BEGIN
		--小于当年当月
	  SET @MaxDay =DAY(dateadd(month,1,CONCAT(@RYear,'-',@RMonth,'-','1'))-1) 
	END
	--设置第一天与最后一天的日期
	SET @FirstDay= CONCAT(@RYear,'-',@RMonth,'-','1');
	SET @LastDay =CONCAT(@RYear,'-',@RMonth,'-',@MaxDay);
	
	------------------获取有效的周数标识--------------------------------
		--第一天周数
		IF DATENAME(weekday, @FirstDay) IN ('星期六','星期天')  
		BEGIN
		  SET @WeekIndex =	DATEPART(WEEK, @FirstDay)+1;
		END
		ELSE	
		BEGIN
			SET @WeekIndex =	DATEPART(WEEK, @FirstDay);
		END
		--最后一天周数
		IF DATENAME(weekday, @LastDay) IN ('星期六','星期天')
		BEGIN
		  SET @MaxWeekIndex =	DATEPART(WEEK, @LastDay);
		END
		ELSE	
		BEGIN
			SET @MaxWeekIndex =	DATEPART(WEEK, @LastDay)-1;
		END
		
		IF @MaxWeekIndex<@WeekIndex
		BEGIN
			SET @RestCode='1'
			SET @RestMsg='不足一周'
			RETURN
		END
	
	--遍历有效天数的每一天
	WHILE @WeekIndex<=@MaxWeekIndex BEGIN
	  SET @WeekCount=@WeekCount+1
		SET @Weeks=@Weeks+CONCAT(@WeekIndex,','); 
		SET @WeekIndex=@WeekIndex+1
	END
	
	--去掉最后的逗号
	IF(LEN(@Weeks)>1)
	BEGIN
		SET @Weeks=LEFT(@Weeks, LEN(@Weeks)-1);
	END

	--PRINT @WeekCount
	--PRINT @Weeks
	SET @RestCode='0';
	SET @RestMsg='成功';
	RETURN 		

END
GO


-- ----------------------------
-- procedure structure for MaintenanceItemSync
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[MaintenanceItemSync]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[MaintenanceItemSync]
GO

CREATE PROCEDURE [dbo].[MaintenanceItemSync](@RYear INT,@TimeMark VARCHAR(1),@IsSyncDetial VARCHAR(1),@UpdateUser VARCHAR(50),@RestCode VARCHAR(1) OUTPUT, @RestMsg VARCHAR(100) OUTPUT)
AS
BEGIN
	/** 
	--将 预设的项目 同步到 指定报表的保养项目
	--	@AssetNo				资产编号
	--  @RYear					年份
	--	@TimeMark				日期标志
	--	@IsSyncDetial		是否同步报表的记录详情,追加默认的'V'值
	**/
	----------------------参数检查---------------------
	
	IF(ISNULL(@RYear,'')='')
	BEGIN
		SET @RestCode='1';
		SET @RestMsg='未指定年份';
		RETURN 		
	END
	
	IF(ISNULL(@TimeMark,'')='')
	BEGIN
		SET @RestCode='1';
		SET @RestMsg='未指定日期标记';
		RETURN 		
	END


	----------------------报表的保养项目-----------------
	--删除原有的项目
	DELETE A_MaintenanceReportItem_T WHERE [Year]=@RYear AND TimeMark=@TimeMark;
	--插入新的项目
	INSERT INTO  A_MaintenanceReportItem_T (AssetNo,[Year],TimeMark,ItemName,SortNo) 
	SELECT AssetNo,@RYear,@TimeMark,ItemName,SortNo FROM A_MaintenanceItem_T WHERE TimeMark=@TimeMark;
	--追加'保养人签名'项目
	INSERT A_MaintenanceReportItem_T (AssetNo,[Year],TimeMark,ItemName,SortNo) 
	SELECT AssetNo,@RYear,@TimeMark,'保养人签名',Max(SortNo)+1 FROM A_MaintenanceReportItem_T WHERE TimeMark=@TimeMark GROUP BY AssetNo
	

	-----------------------报表的保养记录------------------
	--是否要同步到报表的记录
	IF(@IsSyncDetial='Y')
	BEGIN
	
		DECLARE @MRDId INT;
		DECLARE @AssetNo VARCHAR(50);
		
		-- 查找维护的记录
		SELECT  Id,AssetNo INTO #MRD FROM A_MaintenanceReportD_T WHERE [Year]=@RYear AND TimeMark=@TimeMark;
		
		------------------- 遍历保养记录
		DECLARE C_ReportD  CURSOR  FAST_FORWARD FOR SELECT * FROM #MRD;--声明游标		
		OPEN C_ReportD;	-- 开启游标
		FETCH NEXT FROM C_ReportD INTO @MRDId,@AssetNo;-- 取第一条记录
		WHILE @@FETCH_STATUS=0 
		BEGIN
			--插入缺少的项目
			INSERT INTO A_MaintenanceReportDV_T (MRDId,ItemName,ItemValue)
			SELECT @MRDId,ItemName,'V' FROM A_MaintenanceReportItem_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark=@TimeMark AND ItemName<>'保养人签名' AND ItemName NOT IN(SELECT ItemName FROM A_MaintenanceReportDV_T WHERE MRDId=@MRDId);
				--删除多余的项目
			DELETE A_MaintenanceReportDV_T WHERE MRDId=@MRDId AND ItemName NOT IN(SELECT ItemName FROM A_MaintenanceReportItem_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark=@TimeMark)
			--UPDATE A_MaintenanceReportD_T SET UpdateUser=@UpdateUser,UpdateTime=GETDATE()	WHERE Id=@MRDId;
			-- 取下一条记录
			FETCH NEXT FROM C_ReportD INTO @MRDId,@AssetNo;-- 取下条
		END
		
		-- 关闭游标
		CLOSE C_ReportD;
		-- 释放游标
		DEALLOCATE C_ReportD;
		--删除临时表
		DELETE #MRD;

		
		SET @RestCode='0';
		SET @RestMsg='更新成功';
		RETURN 		
	END

END
GO


-- ----------------------------
-- function structure for clearSpace
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[clearSpace]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP FUNCTION[dbo].[clearSpace]
GO

CREATE FUNCTION [dbo].[clearSpace]
( @val AS nvarchar 
)
RETURNS nvarchar
WITH RETURNS NULL ON NULL INPUT 
AS
BEGIN
	-- routine body goes here, e.g.
	-- SELECT 'Navicat for SQL Server'
	RETURN NULL
END
GO


-- ----------------------------
-- procedure structure for StatMaintenanceReportStatus
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[StatMaintenanceReportStatus]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[StatMaintenanceReportStatus]
GO

CREATE PROCEDURE [dbo].[StatMaintenanceReportStatus](@RYear INT)
AS
BEGIN
  --定时任务
	-- 统计并更新所有保养报表的状态
	
	DECLARE @CurrYear INT;--当年
	DECLARE @CurrMonth INT;--当月
	
	--报表字段
	DECLARE @AssetNo VARCHAR(50);
	DECLARE @RMonth INT;
	

	--报表对应日期标记的最小记录数量
	DECLARE @DayCount INT;--日
	DECLARE @WeekCount INT;--周
	DECLARE @MonthCount INT;--月
	DECLARE @YearMonthCount INT;--年月
	DECLARE @QuarterCount INT;--季
	DECLARE @YearCount INT;--年
	DECLARE @Status VARCHAR;
	
	DECLARE @RealDayCount INT;--日
	DECLARE @RealWeekCount INT;--周
	DECLARE @RealMonthCount INT;--月
	DECLARE @RealYearMonthCount INT;--年月
	DECLARE @RealQuarterCount INT;--季
	DECLARE @RealYearCount INT;--年

	
	------------------参数检查---------------------------
	SET @CurrYear=YEAR(GETDATE())
	SET @CurrMonth=MONTH(GETDATE())
	IF(ISNULL(@RYear,'')='')
	BEGIN
   SET @RYear =@CurrYear
	END
	--大于当年,不执行
	IF @RYear>@CurrYear
	BEGIN
		RETURN
	END
	
	------------------初始化------------------------
	-- 获取要更新状态的报表
	SELECT AssetNo,[Year],[Month] INTO #MR FROM A_MaintenanceReport_T WHERE [Year]= @RYear
	IF(@RYear=@CurrYear)
	BEGIN
		--当年,删除大于当月的数据
		DELETE #MR WHERE ISNULL([Month],0)>@CurrMonth
	END

	-- 声明游标		 
  DECLARE C_Report  CURSOR  FAST_FORWARD FOR SELECT * FROM #MR;
	-- 开启游标
	OPEN C_Report;
	-- 取第一条记录
	FETCH NEXT FROM C_Report INTO @AssetNo,@RYear,@RMonth;
	
	-- 遍历游标数据
	WHILE @@FETCH_STATUS=0 
	BEGIN
	
		SET @Status='Y'
		--执行
		IF ISNULL(@RMonth,'')<>''
		BEGIN
			-------------------------------月报表-------------------------------
			-------------------------------设置最小数量-------------------------
			IF @RYear=@CurrYear AND @RMonth=@CurrMonth
			BEGIN
			 --当年当月
				-- SET @DayCount= DAY(GETDATE())-1
				-- SET @WeekCount = @DayCount/7 
				 SET @MonthCount = 0 
			END
			ELSE
			BEGIN
			--小于当年当月
				-- SET @DayCount= DAY(dateadd(month,1,CONCAT(@RYear,'-',@RMonth,'-','01'))-1) 
				-- SET @WeekCount = @DayCount/7 
				 SET @MonthCount = 1
			END
	
			--------------记录数是否满足最小值判断---------------
			DECLARE @Days VARCHAR(300),@Weeks VARCHAR(300)
			--日
			--获取资产的被领取使用的有效天数，及天数标志值
			EXEC [GetAssetUsedDayStamp] @AssetNo,@RYear,@RMonth,@DayCount OUTPUT,@Days OUTPUT,'','';
			IF(@DayCount>0)
			BEGIN
				SELECT @RealDayCount=Count(1) FROM A_MaintenanceReportD_T WHERE  AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='D' AND TimeMarkStamp IN(SELECT * FROM dbo.SplitString(@Days,','))
				IF @RealDayCount<@DayCount
				BEGIN
					SET @Status='N'
				END
			END	
			--周
			--获取有效的周数，及周数标志值
			EXEC [GetWorkWeek] @RYear,@RMonth,@WeekCount OUTPUT,@Weeks OUTPUT,'','';
			IF (@WeekCount>0)
			BEGIN
				SELECT @RealWeekCount=Count(1) FROM A_MaintenanceReportD_T WHERE  AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='W' AND TimeMarkStamp IN(SELECT * FROM dbo.SplitString(@Weeks,','))
				IF @RealWeekCount<@WeekCount
				BEGIN
					SET @Status='N'
				END
			END
			--月
			IF (@MonthCount>0)
			BEGIN
				 SELECT @RealMonthCount=Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='M' AND TimeMarkStamp=@RMonth
				 IF @RealMonthCount<@MonthCount
				 BEGIN
						SET @Status='N'
				 END
			END
		END
		ELSE
		BEGIN
		
			----------------------年报表----------------------------------
			-----------------设置最小值-----------------------------------
			IF @RYear=@CurrYear 
				BEGIN
				 --当年
					 SET @YearMonthCount= @CurrMonth-1
					 SET @QuarterCount = @YearMonthCount/3
					 SET @YearCount = 0
				END
				ELSE
				BEGIN
					 --小于当年
					 SET @YearMonthCount= 12
					 SET @QuarterCount = 4 
					 SET @YearCount = 1
				END
			--------------记录数是否满足最小值判断---------------
			--月
			IF (@YearMonthCount>0)
			BEGIN
				 SELECT @RealYearMonthCount=Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='M' AND TimeMarkStamp<=@YearMonthCount
				 IF @RealYearMonthCount<@YearMonthCount
				 BEGIN
						SET @Status='N'
				 END
			END
			--print concat('月份：',@RMonth,',月数据：',@YearMonthCount,',记录数：', @RealYearMonthCount,',状态：',@Status);
			--季
			IF (@QuarterCount>0)
			BEGIN
				 SELECT @RealQuarterCount=Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='Q' AND TimeMarkStamp<=@QuarterCount
				 IF @RealQuarterCount<@QuarterCount
				 BEGIN
						SET @Status='N'
				 END
			END
			--年
			IF (@YearCount>0)
			BEGIN
				 SELECT @RealYearCount=Count(1) FROM A_MaintenanceReportD_T WHERE AssetNo=@AssetNo AND [Year]=@RYear AND TimeMark='Y' AND TimeMarkStamp<=@YearCount
				 IF @RealQuarterCount<@YearCount
				 BEGIN
						SET @Status='N'
				 END
			END
		END
		
		print concat('资产：',@AssetNo,'年份',@RYear,'月份：',@RMonth,',状态：',@Status);
			--更新
		IF ISNULL(@RMonth,'')=''
		BEGIN
			--年表
				UPDATE A_MaintenanceReport_T SET [Status]=@Status WHERE AssetNo=@AssetNo AND [Year]=@RYear AND [Month] IS NULL;
		END
		ELSE
		BEGIN
			--月表
				UPDATE A_MaintenanceReport_T SET [Status]=@Status WHERE AssetNo=@AssetNo AND [Year]=@RYear AND [Month] = @RMonth;
		END
		
		-- 取下一条记录
		FETCH NEXT FROM C_Report INTO @AssetNo,@RYear,@RMonth;
	END
	
	-- 关闭游标
	CLOSE C_Report;
	-- 释放游标
	DEALLOCATE C_Report;
END
GO


-- ----------------------------
-- procedure structure for GetAssetUsedDayStamp
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAssetUsedDayStamp]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[GetAssetUsedDayStamp]
GO

CREATE PROCEDURE [dbo].[GetAssetUsedDayStamp](@AssetNo VARCHAR(50),@RYear INT,@RMonth  INT,@DayCount INT OUTPUT,@Days VARCHAR(300) OUTPUT,@RestCode VARCHAR(1) OUTPUT, @RestMsg VARCHAR(100) OUTPUT)
AS
BEGIN
  --获取指定月份的非节假日的日期值
	
	SET @DayCount=0;
	SET @Days='';
	--------------------------参数检查-------------------------------
	IF(ISNULL(@RYear,'')='')
	BEGIN
		SET @RestCode='1';
		SET @RestMsg='未指定年份';
		RETURN 		
	END
	
	IF(ISNULL(@RMonth,'')='')
	BEGIN
			SET @RestCode='1';
			SET @RestMsg='未指定月份';
			RETURN 		
	END
	
	------------------------- 获取有效天数，及日期--------------------------------------
	DECLARE @CurrYear INT= YEAR(GETDATE());--当年
	DECLARE @CurrMonth INT =MONTH(GETDATE());--当月
	DECLARE @MaxDayCount INT = 0;--指定月份最大有效天数
	DECLARE @DayIndex INT = 1;--天数索引
	DECLARE @DataDicId INT ;--数据字典目录ID
	DECLARE @IsHoliday VARCHAR(1);--是否休息日，Y:是，N:否
	DECLARE @RDay VARCHAR(20);--循环的日期

	--大于当年当月
	IF @RYear>@CurrYear OR(@RYear=@CurrYear AND @RMonth>@CurrMonth)
	BEGIN
		SET @RestCode='1'
		SET @RestMsg='年月超出当前时间'
		RETURN 		
	END
	
	--设置当月最大天数
	IF(@RYear=@CurrYear AND @RMonth=@CurrMonth)
	BEGIN
		--当年当月
		SET @MaxDayCount=DAY(GETDATE());
	END
	ELSE
	BEGIN
		--小于当年当月
		SET @MaxDayCount= DAY(dateadd(month,1,CONCAT(@RYear,'-',@RMonth,'-','01'))-1) 
	END
	
	--获取系统数据字典设置的节日
	SELECT @DataDicId =Id FROM S_SysDic WHERE [Catalog]='HOLIDAY_DATE_DAY';
	SELECT * INTO #SDD FROM S_SysDicDetial WHERE CatalogId=ISNULL(@DataDicId,'') AND Year(DataKey)=@RYear AND Month(DataKey)=@RMonth ;
	
	--获取资产领用的记录
	DECLARE @StartTime VARCHAR(10) = CONCAT(@RYear,'-',@RMonth,'-1');
	DECLARE @EndTime VARCHAR(10) = CONCAT(@RYear,'-',@RMonth,'-',@MaxDayCount);
	DECLARE @NowDay VARCHAR(10) = CAST(GETDATE() AS DATE);
	SELECT CAST(StartTime AS DATE) StartTime,CAST(ISNULL(EndTime,GETDATE()) AS DATE) EndTime INTO #RE FROM A_AssetReceive_T WHERE AssetNo=@AssetNo AND StartTime<=@EndTime AND ISNULL(EndTime,GETDATE())>=@StartTime



	--遍历有效天数的每一天
	WHILE @DayIndex<=@MaxDayCount BEGIN
		 SET @RDay =CONCAT(@RYear,'-',@RMonth,'-',@DayIndex);
		 
		 --判断指定日期该资产是否有被领用使用
		 IF EXISTS(SELECT 1 FROM #RE WHERE StartTime<=@RDay AND EndTime>=@RDay)
		 BEGIN
			 SELECT @IsHoliday = DataValue FROM #SDD WHERE Day(DataKey)=@DayIndex
					 --是否节日
			 IF(ISNULL(@IsHoliday,'')<>'')
			 BEGIN
				--优化根据系统设置
				--不是节日
				IF(@IsHoliday<>'Y')
				BEGIN
					SET @DayCount=@DayCount+1;
					SET @Days=CONCAT(@Days,DATEPART(dayofyear, @RDay),',')
				END
			 END
			 ELSE
			 BEGIN
				--系统未指定当日是否节日
				--判断是否周日
					IF( DATENAME(weekday, @RDay) NOT IN('星期日'))
					BEGIN
						SET @DayCount=@DayCount+1;
						SET @Days=CONCAT(@Days,DATEPART(dayofyear, @RDay),',')
					END
			 END
		 END
		
		 
		 SET @DayIndex=@DayIndex+1;
		 SET @IsHoliday='';
	END
	
	--去掉最后的逗号
	IF(LEN(@Days)>1)
	BEGIN
		SET @Days=LEFT(@Days, LEN(@Days)-1);
	END

	PRINT @DayCount
	PRINT @Days
	SET @RestCode='0';
	SET @RestMsg='成功';
	RETURN 		

END
GO


-- ----------------------------
-- function structure for GetPlanRestSeconds
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPlanRestSeconds]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP FUNCTION[dbo].[GetPlanRestSeconds]
GO

CREATE FUNCTION [dbo].[GetPlanRestSeconds]
( 
@watchStart datetime,
@watchEnd datetime
)
RETURNS INT-- int
AS
BEGIN
	DECLARE  @planRestSeconds INT;
	DECLARE  @PlanName VARCHAR;
	DECLARE  @StartTime TIME;
	DECLARE  @EndTime TIME;
	--计算变量
	DECLARE @tempStartTime datetime;
	DECLARE @tempEndTime datetime;
	--初始计划时间
	SET @planRestSeconds= 0;
	
	-- 声明游标		 
	DECLARE C_PlanTime  CURSOR  FAST_FORWARD FOR SELECT * FROM M_PlanTime_T;
	
	-- 开启游标
	OPEN C_PlanTime;
	
	-- 取第一条记录
	FETCH NEXT FROM C_PlanTime INTO 	 @PlanName,@StartTime,@EndTime;
	
	-- 遍历游标数据
	WHILE @@FETCH_STATUS=0 
	BEGIN
	
		SET @tempStartTime= CONVERT(varchar(50), @watchStart,23) + ' ' + CONVERT(varchar(8), @StartTime);
		SET @tempEndTime= CONVERT(varchar(50), @watchStart,23)+ ' ' + CONVERT(varchar(8), @EndTime);
		--次日
		IF @tempEndTime < @tempStartTime
		BEGIN
				SET	@tempEndTime=DATEADD(DAY, 1, @tempEndTime);
		END
 		--计划停机开始时间 > 设备监控的结束时间 OR 计划停机结束时间 < 设备监控的开始时间  忽略
		IF (@tempStartTime > @watchEnd or @tempEndTime < @watchStart)
		BEGIN	
			FETCH NEXT FROM C_PlanTime INTO  @PlanName,@StartTime,@EndTime;
			continue;
		END
		IF @tempStartTime < @watchStart
		BEGIN
				SET	@tempStartTime = @watchStart;
		END
		IF @tempEndTime > @watchEnd
		BEGIN
			SET	@tempEndTime = @watchEnd;
		END
		SET	@planRestSeconds += datediff(second, @tempStartTime,@tempEndTime);

		-- 取下一条记录
		FETCH NEXT FROM C_PlanTime INTO  @PlanName,@StartTime,@EndTime;
	END
	
	-- 关闭游标
	CLOSE C_PlanTime;
	-- 释放游标
	DEALLOCATE C_PlanTime;
	
	-- 返回计划停机时间     
	RETURN @planRestSeconds;
	 
END
GO


-- ----------------------------
-- procedure structure for RefineReport
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[RefineReport]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[RefineReport]
GO

CREATE PROCEDURE [dbo].[RefineReport](@startDate DATE)
AS
BEGIN
	-- 功能:精简设备上报的数据，并将精简后的数据插入 M_MachineReportRefine_T 表中
	-- 参数说明：
	-- 		@startDate 指定日期，可为null
	
	DECLARE @startTime datetime;--开始时间
	DECLARE @endTime datetime;--结束时间
	-- 参数处理
	IF  @startDate is null 
	BEGIN
		-- 为空时，默认精简前一天的上报记录
		SET @startDate=DATEADD(DAY, -1,GETDATE());
	END
	SET @startTime=CONVERT(varchar(50), @startDate,23)+' 07:00:00';
	SET @endTime=DATEADD(DAY, 1, @startTime);

	PRINT( @startTime);
	PRINT( @endTime);
	-- 精简报告记录
	WITH cte AS (
		SELECT *,
					LAG ( RunState,1,0 ) OVER (PARTITION BY MCode,MachineNo ORDER BY  CreateTime) AS PreRunState,
					LAG ( WarnState,1,0 ) OVER (PARTITION BY MCode,MachineNo ORDER BY  CreateTime) AS PreWarnState,
					LEAD ( RunState,1,0 ) OVER (PARTITION BY MCode,MachineNo  ORDER BY  CreateTime) AS NextRunState,
					LEAD ( ProductCount,1,0 ) OVER ( ORDER BY  CreateTime  ) AS NextProductCount,
          LEAD ( FailedCount,1,0 ) OVER (PARTITION BY MCode,MachineNo  ORDER BY  CreateTime  ) AS NextFailedCount
		FROM
					dbo.M_MachineReport_T WHERE  CreateTime>=@startTime AND CreateTime<=@endTime)

	-- 将精简后的数据 插入 M_MachineReportRefine_T 精简表中
	INSERT INTO M_MachineReportRefine_T 
		SELECT * FROM M_MachineReport_T 
		WHERE id IN (SELECT	id FROM	cte WHERE RunState!=PreRunState OR WarnState!=PreWarnState OR NextRunState=0 OR NextProductCount < ProductCount OR NextFailedCount < FailedCount) 
		AND id NOT IN(SELECT id FROM M_MachineReportRefine_T WHERE CreateTime>=@startTime AND CreateTime<=@endTime);

END
GO


-- ----------------------------
-- procedure structure for StatReportOne
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[StatReportOne]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[StatReportOne]
GO

CREATE PROCEDURE [dbo].[StatReportOne]
(
@startDate DATE,--指定日期
@paramMCode int,--设备编码
@paramMachineNo int--设备编号
)
AS
BEGIN
	--功能 ：统计分析单个设备单日的数据。并将分析结果存入M_MachineRunStat_T表中
	
	DECLARE @paramStartTime datetime;--开始时间
	DECLARE @paramEndTime datetime;--结束时间
	-- 数据字段
	DECLARE @Id INT;
	DECLARE @MCode INT;
	DECLARE @MachineNo INT;
	DECLARE @RunState char;
	DECLARE @CT real;
	DECLARE @ProductCount INT;
	DECLARE @FailedCount INT;
	DECLARE @WarnState char;
	DECLARE @WarnCode varchar;
	DECLARE @CreateTime datetime;
	DECLARE @RemoteEndPoint varchar;
	DECLARE @HexCode varchar;
	DECLARE @PreRunState char;
	DECLARE @PreWarnState char;
	DECLARE @NextRunState char;
	DECLARE @EndTime datetime;
  --统计后的数据
	DECLARE @ErrorCount INT;	
	DECLARE @StatProductCount INT;	
	DECLARE @StatFailedCount INT;	
	DECLARE @RunSeconds INT;	
	DECLARE @ErrorSeconds INT;	
	DECLARE @PlanRestSeconds INT;	
	DECLARE @TimeUtilizeRate decimal(10,2);	
	DECLARE @EfficacyUtilizeRate decimal(10,2);	
	DECLARE @PassRate decimal(10,2);	
	DECLARE @OEE decimal(10,2);	
	--变量
	DECLARE @totalSeconds INT;	
	DECLARE @runStartTime datetime;
	DECLARE @runEndTime datetime;
	DECLARE @TheoryCT decimal(10,3)
	--初始参数
	SET @ErrorCount=0;
	SET @totalSeconds=0;--开机时间(秒)
	SET @RunSeconds=0;--运行时间（秒）
	SET @ErrorSeconds=0;--故障时间（秒）
	SET @PlanRestSeconds=0;--计划停机时间（秒）
	
	-- 参数处理
	IF  @startDate is null 
	BEGIN
		SET @startDate=DATEADD(DAY, -1,GETDATE());
	END
	SET @paramStartTime=CONVERT(varchar(50), @startDate,23)+' 07:00:00';
	SET @paramEndTime=DATEADD(DAY, 1, @paramStartTime);
	
	
	-- 从精简后的上报数据表中取数据进行计算
	WITH cte AS (
	    SELECT *,
						LAG ( RunState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS PreRunState,
						LAG ( WarnState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS PreWarnState,
						LEAD ( RunState,1,0 ) OVER ( ORDER BY  CreateTime  ) AS NextRunState
			FROM dbo.M_MachineReportRefine_T 
			WHERE Mcode = @paramMCode AND MachineNo = @paramMachineNo AND CreateTime>=@paramStartTime AND CreateTime <= @paramEndTime)
	-- 将数据给到虚拟表#TempReport
	SELECT
		*,
		LEAD ( CreateTime,1,@paramEndTime ) OVER ( ORDER BY  CreateTime  ) AS EndTime 
		INTO #TempReport
	FROM
		cte WHERE RunState!=PreRunState OR WarnState!=PreWarnState OR  NextRunState=0 ORDER BY CreateTime asc;
	
	-- 判断是否有数据，没有则结束存储过程。
	IF NOT EXISTS(SELECT * FROM #TempReport)
	BEGIN
		RETURN
	END

	-- 设备的理论CT
	SELECT @TheoryCT = TheoryCT FROM M_Machine_T WHERE MCode=@paramMCode AND MachineNo=@paramMachineNo;
	--=========================通过游标遍历上报的数据==============================
	-- 声明游标		 
  DECLARE C_Report  CURSOR  FAST_FORWARD FOR SELECT * FROM #TempReport;
	
	-- 开启游标
	OPEN C_Report;
	
	-- 取第一条记录
	FETCH NEXT FROM C_Report INTO 	 @Id,@MCode,@MachineNo,@RunState,@CT,@ProductCount,@FailedCount,@WarnState ,@WarnCode,@CreateTime,@RemoteEndPoint,@HexCode,@PreRunState,@PreWarnState,@NextRunState,@EndTime;
	SET @runStartTime = @CreateTime;
	SET @StatProductCount = @ProductCount;
	SET @StatFailedCount = @FailedCount;
	
	-- 遍历游标数据
	WHILE @@FETCH_STATUS=0 
	BEGIN
	
		IF @RunState=1 BEGIN
				SET @ErrorSeconds+=datediff(second, @CreateTime,@EndTime);
			  SET @ErrorCount+=1;
		END
		ELSE BEGIN
				SET @RunSeconds+=datediff(second, @CreateTime,@EndTime);
		END

		-- 取下一条记录
		FETCH NEXT FROM C_Report INTO 	 @Id,@MCode,@MachineNo,@RunState,@CT,@ProductCount,@FailedCount,@WarnState ,@WarnCode,@CreateTime,@RemoteEndPoint,@HexCode,@PreRunState,@PreWarnState,@NextRunState,@EndTime;
	END
	
	-- 关闭游标
	CLOSE C_Report;
	-- 释放游标
	DEALLOCATE C_Report;
	
	--======================数据分析============================
	SET @runEndTime=@EndTime;
	SET @StatProductCount = @ProductCount-@StatProductCount;
	SET @StatFailedCount = @FailedCount-@StatFailedCount;
	SET @totalSeconds=datediff(second, @runStartTime,@runEndTime);
	SET @planRestSeconds =  dbo.GetPlanRestSeconds(@runStartTime,@runEndTime);

		
 --时间稼动率= (（开机时间-异常时间-计划停机时间)/（开机时间-计划停机时间))*100%
	IF (@totalSeconds - @planRestSeconds) > 0
	BEGIN
		 SET @TimeUtilizeRate = ((@totalSeconds - (@ErrorSeconds - @PlanRestSeconds)) / (@totalSeconds - @PlanRestSeconds+0.0)) * 100;
	END
	
 --性能稼动率=  ((产能 * 理论CT) / (开机时间—异常时间-计划停机时间))*100%
 	IF (@totalSeconds - (@ErrorSeconds - @PlanRestSeconds)) > 0 BEGIN
 			SET @EfficacyUtilizeRate =((@StatProductCount * @TheoryCT) / (@totalSeconds - (@ErrorSeconds - @planRestSeconds)+0.0)) * 100;
 	END
	
	--良品率=(1-(不良品数/产量))*100
	IF (@StatProductCount > 0) BEGIN
		 SET	@PassRate = (1 - (@StatFailedCount /( @StatProductCount+0.0))) * 100;
	END
	
	-- OEE 综合稼动率
  SET	@OEE = (@TimeUtilizeRate * @EfficacyUtilizeRate * @PassRate) / 10000.0;

	
	-- ===============将统计分析后的数据存入 M_MachineRunStat_T 表中=====================
  IF EXISTS(SELECT 1 FROM M_MachineRunStat_T WHERE FixDate=@startDate AND Mcode=@paramMCode AND MachineNo=@paramMachineNo)
	BEGIN
		--更新
		UPDATE M_MachineRunStat_T SET ErrorCount=@ErrorCount,ProductCount=@StatProductCount,
		FailedCount= @StatFailedCount,RunSeconds=@RunSeconds,ErrorSeconds=@ErrorSeconds,
		PlanRestSeconds=@PlanRestSeconds,TimeUtilizeRate=@TimeUtilizeRate,EfficacyUtilizeRate=@EfficacyUtilizeRate,PassRate=@PassRate,OEE=@OEE
		WHERE FixDate=@startDate AND Mcode=@paramMCode AND  MachineNo=@paramMachineNo
	END
	ELSE BEGIN
		--插入
		INSERT INTO M_MachineRunStat_T(FixDate,Mcode,MachineNo,ErrorCount,ProductCount,FailedCount,RunSeconds,ErrorSeconds,	PlanRestSeconds,TimeUtilizeRate,EfficacyUtilizeRate,PassRate,OEE)
		VALUES(@startDate,@paramMCode,@paramMachineNo,@ErrorCount,@StatProductCount,@StatFailedCount,@RunSeconds,@ErrorSeconds,@PlanRestSeconds,@TimeUtilizeRate,@EfficacyUtilizeRate,@PassRate,@OEE)
	END
END
GO


-- ----------------------------
-- procedure structure for StatReport
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[StatReport]') AND type IN ('FN', 'FS', 'FT', 'IF', 'TF'))
	DROP PROCEDURE[dbo].[StatReport]
GO

CREATE PROCEDURE [dbo].[StatReport](@startDate DATE)
AS
BEGIN
	-- 功能：统计并解析所有设备的上报数据，并存入M_MachineRunStat_T表中
	DECLARE @startTime datetime;--开始时间
	DECLARE @endTime datetime;--结束时间
	-- 参数处理
	IF  @startDate is null 
	BEGIN
		SET @startDate=DATEADD(DAY, -1,GETDATE());
	END
	SET @startTime=CONVERT(varchar(50), @startDate,23)+' 07:00:00';
	SET @endTime=DATEADD(DAY, 1, @startTime);
	
	-- 执行 【精简数据】存储过程
	EXECUTE RefineReport @startDate;
	
	-- 从精简后的上报数据表中取数据进行计算
	DECLARE @MCode INT;
	DECLARE @MachineNo INT;
	
	SELECT MCode,MachineNo INTO #Machine FROM M_MachineReportRefine_T WHERE CreateTime>=@startTime AND CreateTime<=@endTime  GROUP BY MCode,MachineNo ;
	--如果不存在数据则停止存储过程
	IF NOT EXISTS(SELECT * FROM #Machine)
	BEGIN
		RETURN;
	END
	--=========================通过游标遍历设备==============================
	-- 声明游标		 
  DECLARE C_Machine  CURSOR  FAST_FORWARD FOR SELECT * FROM #Machine;
	
	-- 开启游标
	OPEN C_Machine;
	
	-- 取第一条记录
	FETCH NEXT FROM C_Machine INTO 	 @MCode,@MachineNo;
	
	-- 遍历游标数据
	WHILE @@FETCH_STATUS=0 
	BEGIN
		--执行单个设备数据的分析
		EXECUTE dbo.StatReportOne @startDate,@MCode,@MachineNo;
		-- 取下一条记录
		FETCH NEXT FROM C_Machine INTO 	 @MCode,@MachineNo;
	END
	
	-- 关闭游标
	CLOSE C_Machine;
	-- 释放游标
	DEALLOCATE C_Machine;
	
END
GO


-- ----------------------------
-- Uniques structure for table A_AssetFile_T
-- ----------------------------
ALTER TABLE [dbo].[A_AssetFile_T] ADD CONSTRAINT [uqk_assetNo_file_fileClass] UNIQUE NONCLUSTERED ([AssetNo] ASC, [FileId] ASC, [FileClass] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table A_AssetInfo_T
-- ----------------------------
ALTER TABLE [dbo].[A_AssetInfo_T] ADD CONSTRAINT [PK__A_AssetI__434A45B88CAF3F8C] PRIMARY KEY CLUSTERED ([AssetNo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table A_AssetReceive_T
-- ----------------------------
ALTER TABLE [dbo].[A_AssetReceive_T] ADD CONSTRAINT [uk_asset_endTime] UNIQUE NONCLUSTERED ([AssetNo] ASC, [EndTime] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table A_MaintenanceItem_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceItem_T] ADD CONSTRAINT [uk_asset_mark_item] UNIQUE NONCLUSTERED ([AssetNo] ASC, [TimeMark] ASC, [ItemName] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table A_MaintenanceReport_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceReport_T] ADD CONSTRAINT [uk_assetNo_year_month] UNIQUE NONCLUSTERED ([AssetNo] ASC, [Year] DESC, [Month] DESC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for A_MaintenanceReportD_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[A_MaintenanceReportD_T]', RESEED, 341738)
GO


-- ----------------------------
-- Uniques structure for table A_MaintenanceReportD_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceReportD_T] ADD CONSTRAINT [uk_asset_year_mark_stamp] UNIQUE NONCLUSTERED ([AssetNo] ASC, [Year] ASC, [TimeMark] ASC, [TimeMarkStamp] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table A_MaintenanceReportD_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceReportD_T] ADD CONSTRAINT [PK__A_Mainte__3214EC0766AA0AA1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table A_MaintenanceReportDV_T
-- ----------------------------
CREATE NONCLUSTERED INDEX [ind_MRDId]
ON [dbo].[A_MaintenanceReportDV_T] (
  [MRDId] ASC
)
GO


-- ----------------------------
-- Uniques structure for table A_MaintenanceReportDV_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceReportDV_T] ADD CONSTRAINT [uk_Id_Item] UNIQUE NONCLUSTERED ([MRDId] ASC, [ItemName] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table A_MaintenanceReportItem_T
-- ----------------------------
ALTER TABLE [dbo].[A_MaintenanceReportItem_T] ADD CONSTRAINT [uk_asset_year_mark_item] UNIQUE NONCLUSTERED ([AssetNo] ASC, [Year] ASC, [TimeMark] ASC, [ItemName] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for A_ResumeMaintenanceD_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[A_ResumeMaintenanceD_T]', RESEED, 3045)
GO


-- ----------------------------
-- Primary Key structure for table A_ResumeMaintenanceD_T
-- ----------------------------
ALTER TABLE [dbo].[A_ResumeMaintenanceD_T] ADD CONSTRAINT [PK__A_Resume__3214EC0774D8BB24] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for A_ResumeRepairD_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[A_ResumeRepairD_T]', RESEED, 42)
GO


-- ----------------------------
-- Auto increment value for A_ResumeReport_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[A_ResumeReport_T]', RESEED, 2936)
GO


-- ----------------------------
-- Uniques structure for table A_ResumeReport_T
-- ----------------------------
ALTER TABLE [dbo].[A_ResumeReport_T] ADD CONSTRAINT [UQ__M_Mainte__952A9F31599BD80A] UNIQUE NONCLUSTERED ([AssetNo] ASC, [PageNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table A_ResumeReport_T
-- ----------------------------
ALTER TABLE [dbo].[A_ResumeReport_T] ADD CONSTRAINT [PK__M_Mainte__3214EC074AA6C00C_copy1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for M_ErrorRecord_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[M_ErrorRecord_T]', RESEED, 128085)
GO


-- ----------------------------
-- Primary Key structure for table M_ErrorRecord_T
-- ----------------------------
ALTER TABLE [dbo].[M_ErrorRecord_T] ADD CONSTRAINT [PK__M_ErrorR__3214EC0720A6F26E] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table M_FaultSolution_T
-- ----------------------------
ALTER TABLE [dbo].[M_FaultSolution_T] ADD CONSTRAINT [PK__M_FaultS__23828C321FA4DDA1] PRIMARY KEY CLUSTERED ([MachineType])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table M_LineQC_T
-- ----------------------------
ALTER TABLE [dbo].[M_LineQC_T] ADD CONSTRAINT [PK__M_LineQC__B827DC66FB929610] PRIMARY KEY CLUSTERED ([Line])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for M_Machine_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[M_Machine_T]', RESEED, 74)
GO


-- ----------------------------
-- Primary Key structure for table M_Machine_T
-- ----------------------------
ALTER TABLE [dbo].[M_Machine_T] ADD CONSTRAINT [PK__M_Machin__DB84B5B9AA8B52BB] PRIMARY KEY CLUSTERED ([MachineCode])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Indexes structure for table M_MachineReportRefine_T
-- ----------------------------
CREATE NONCLUSTERED INDEX [index_report_machineId_copy1]
ON [dbo].[M_MachineReportRefine_T] (
  [MCode] ASC
)
GO

CREATE NONCLUSTERED INDEX [index_report_createtime_copy1]
ON [dbo].[M_MachineReportRefine_T] (
  [CreateTime] DESC
)
GO

CREATE NONCLUSTERED INDEX [index_report_id_time_copy1]
ON [dbo].[M_MachineReportRefine_T] (
  [MCode] ASC,
  [CreateTime] DESC
)
GO


-- ----------------------------
-- Primary Key structure for table M_MachineReportRefine_T
-- ----------------------------
ALTER TABLE [dbo].[M_MachineReportRefine_T] ADD CONSTRAINT [PK__M_Machin__3214EC079C9CC1FB_copy1] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table M_MachineRunStat_T
-- ----------------------------
ALTER TABLE [dbo].[M_MachineRunStat_T] ADD CONSTRAINT [uk_runstat_date_code_no] UNIQUE NONCLUSTERED ([FixDate] DESC, [MCode] ASC, [MachineNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table M_MachineType_T
-- ----------------------------
ALTER TABLE [dbo].[M_MachineType_T] ADD CONSTRAINT [PK__M_Module__EAC9AEC2CC577A8E] PRIMARY KEY CLUSTERED ([MachineType])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table M_MachineWarnCode_T
-- ----------------------------
ALTER TABLE [dbo].[M_MachineWarnCode_T] ADD CONSTRAINT [uk_machine_warncode] UNIQUE NONCLUSTERED ([MachineCode] ASC, [WarnCode] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table M_MsgReceiver_T
-- ----------------------------
ALTER TABLE [dbo].[M_MsgReceiver_T] ADD CONSTRAINT [uk_msgRecceiver_area_stage_code] UNIQUE NONCLUSTERED ([Area] ASC, [StageType] ASC, [WorkCode] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table O_MachineDistribute_T
-- ----------------------------
ALTER TABLE [dbo].[O_MachineDistribute_T] ADD CONSTRAINT [PK__O_Machin__1ACFA7EC9F2BE8EE] PRIMARY KEY CLUSTERED ([PointName])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table O_MachineState_T
-- ----------------------------
ALTER TABLE [dbo].[O_MachineState_T] ADD CONSTRAINT [PK__O_Machin__55476314EF4E7700] PRIMARY KEY CLUSTERED ([StateName])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_ContactPerson_T
-- ----------------------------
ALTER TABLE [dbo].[S_ContactPerson_T] ADD CONSTRAINT [PK__M_Contac__9F8431E6D9FA19C5_copy1] PRIMARY KEY CLUSTERED ([WorkCode])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_FileInfo
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_FileInfo]', RESEED, 10148)
GO


-- ----------------------------
-- Primary Key structure for table S_FileInfo
-- ----------------------------
ALTER TABLE [dbo].[S_FileInfo] ADD CONSTRAINT [PK__S_FileIn__6F0F989F67234005] PRIMARY KEY CLUSTERED ([FileID])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_LineInfo_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_LineInfo_T]', RESEED, 61)
GO


-- ----------------------------
-- Uniques structure for table S_LineInfo_T
-- ----------------------------
ALTER TABLE [dbo].[S_LineInfo_T] ADD CONSTRAINT [uk_lineinfo_name] UNIQUE NONCLUSTERED ([Line] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_LineInfo_T
-- ----------------------------
ALTER TABLE [dbo].[S_LineInfo_T] ADD CONSTRAINT [PK__S_LineIn__3214EC0715C445B7] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_PreviewFileInfo
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_PreviewFileInfo]', RESEED, 10145)
GO


-- ----------------------------
-- Uniques structure for table S_PreviewFileInfo
-- ----------------------------
ALTER TABLE [dbo].[S_PreviewFileInfo] ADD CONSTRAINT [uq_preview_sourcefile_pageno_type] UNIQUE NONCLUSTERED ([SourceFileId] ASC, [PageNo] ASC, [PreviewType] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_PreviewFileInfo
-- ----------------------------
ALTER TABLE [dbo].[S_PreviewFileInfo] ADD CONSTRAINT [PK__S_Previe__25572778662FD702] PRIMARY KEY CLUSTERED ([FileId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table S_SysDic
-- ----------------------------
ALTER TABLE [dbo].[S_SysDic] ADD CONSTRAINT [uk_catalog] UNIQUE NONCLUSTERED ([Catalog] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_SysDic
-- ----------------------------
ALTER TABLE [dbo].[S_SysDic] ADD CONSTRAINT [PK__S_SysDic__FC9CBC4B231CEB29] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_SysDicDetial
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_SysDicDetial]', RESEED, 1004)
GO


-- ----------------------------
-- Primary Key structure for table S_SysDicDetial
-- ----------------------------
ALTER TABLE [dbo].[S_SysDicDetial] ADD CONSTRAINT [PK__S_SysDic__3214EC074CD1845B] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_SystemFile_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_SystemFile_T]', RESEED, 10179)
GO


-- ----------------------------
-- Uniques structure for table S_SystemFile_T
-- ----------------------------
ALTER TABLE [dbo].[S_SystemFile_T] ADD CONSTRAINT [qk_sysfile_name_version] UNIQUE NONCLUSTERED ([FileName] ASC, [FileVersion] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_SystemFile_T
-- ----------------------------
ALTER TABLE [dbo].[S_SystemFile_T] ADD CONSTRAINT [PK__S_System__6F0F98BF153329B9] PRIMARY KEY CLUSTERED ([FileId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table S_User_T
-- ----------------------------
ALTER TABLE [dbo].[S_User_T] ADD CONSTRAINT [PK__M_Handle__9E0BCF753C099A6E_copy1] PRIMARY KEY CLUSTERED ([UserNo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for S_WxMessage_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_WxMessage_T]', RESEED, 26975)
GO


-- ----------------------------
-- Primary Key structure for table S_WxMessage_T
-- ----------------------------
ALTER TABLE [dbo].[S_WxMessage_T] ADD CONSTRAINT [PK__S_WxMess__3214EC07FB6FC412] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

