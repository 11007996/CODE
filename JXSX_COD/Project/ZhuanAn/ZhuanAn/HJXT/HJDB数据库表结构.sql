/*
 Navicat Premium Data Transfer

 Source Server         : 172.18.17.13【永新呼叫】
 Source Server Type    : SQL Server
 Source Server Version : 11007001
 Source Host           : 172.18.17.13:1433
 Source Catalog        : HJDB
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 11007001
 File Encoding         : 65001

 Date: 16/01/2024 17:02:47
*/


-- ----------------------------
-- Table structure for M_ContactPerson_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_ContactPerson_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_ContactPerson_T]
GO

CREATE TABLE [dbo].[M_ContactPerson_T] (
  [WorkCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [RealName] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_ContactPerson_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'工号',
'SCHEMA', N'dbo',
'TABLE', N'M_ContactPerson_T',
'COLUMN', N'WorkCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'姓名',
'SCHEMA', N'dbo',
'TABLE', N'M_ContactPerson_T',
'COLUMN', N'RealName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'联系人表',
'SCHEMA', N'dbo',
'TABLE', N'M_ContactPerson_T'
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
  [TargetHandler] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL
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
-- Table structure for M_HandlerInfo_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_HandlerInfo_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_HandlerInfo_T]
GO

CREATE TABLE [dbo].[M_HandlerInfo_T] (
  [HandlerNo] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [HandlerName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerDept] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerPwd] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerRight] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [HandlerLevel] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [Area] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UseFlag] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateTime] datetime  NULL,
  [ImageUpdateTime] datetime  NULL,
  [HandlerImage] image  NULL
)
GO

ALTER TABLE [dbo].[M_HandlerInfo_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人工号',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人名字',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人部门',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerDept'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人状态（W等待中 H对应中 其它）',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人密码',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerPwd'
GO

EXEC sp_addextendedproperty
'MS_Description', N'权限（A：超级管理员 B：一般管理员）',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerRight'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人级别（1：工程师 2:高级工程师）',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerLevel'
GO

EXEC sp_addextendedproperty
'MS_Description', N'责任区域',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'是否可用（ Y:账号可用，N不可用）',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'UseFlag'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新人工号',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'UpdateUser'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新时间',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'UpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'最后更新头像时间',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'ImageUpdateTime'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人头像图片',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T',
'COLUMN', N'HandlerImage'
GO

EXEC sp_addextendedproperty
'MS_Description', N'处理人信息表',
'SCHEMA', N'dbo',
'TABLE', N'M_HandlerInfo_T'
GO


-- ----------------------------
-- Table structure for M_LineInfo_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_LineInfo_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_LineInfo_T]
GO

CREATE TABLE [dbo].[M_LineInfo_T] (
  [Factory] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Area] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[M_LineInfo_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'工厂',
'SCHEMA', N'dbo',
'TABLE', N'M_LineInfo_T',
'COLUMN', N'Factory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'区域',
'SCHEMA', N'dbo',
'TABLE', N'M_LineInfo_T',
'COLUMN', N'Area'
GO

EXEC sp_addextendedproperty
'MS_Description', N'线别',
'SCHEMA', N'dbo',
'TABLE', N'M_LineInfo_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'产线信息',
'SCHEMA', N'dbo',
'TABLE', N'M_LineInfo_T'
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
-- Table structure for M_Machine_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[M_Machine_T]') AND type IN ('U'))
	DROP TABLE [dbo].[M_Machine_T]
GO

CREATE TABLE [dbo].[M_Machine_T] (
  [MCode] int  NOT NULL,
  [MachineCode] varchar(30) COLLATE Chinese_PRC_CI_AS  NULL,
  [MachineNo] int  NULL,
  [Version] varchar(5) COLLATE Chinese_PRC_CI_AS  NULL,
  [TheoryCT] decimal(10,3)  NULL,
  [MachineName] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [MachineCategory] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL,
  [Line] nvarchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [Machine] nvarchar(20) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_Machine_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码(设备类型代码)',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'版本',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'Version'
GO

EXEC sp_addextendedproperty
'MS_Description', N'理论CT',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'TheoryCT'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备名称',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineName'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备类别',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'MachineCategory'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关联线体',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'Line'
GO

EXEC sp_addextendedproperty
'MS_Description', N'关联机台',
'SCHEMA', N'dbo',
'TABLE', N'M_Machine_T',
'COLUMN', N'Machine'
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
  [Id] int  IDENTITY(1,1) NOT NULL,
  [MCode] int  NOT NULL,
  [MachineNo] int  NULL,
  [RunState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [CT] decimal(10,2)  NULL,
  [ProductCount] int  NULL,
  [FailedCount] int  NULL,
  [WarnState] char(1) COLLATE Chinese_PRC_CI_AS  NULL,
  [WarnCode] varchar(2) COLLATE Chinese_PRC_CI_AS  NULL,
  [CreateTime] datetime  NOT NULL,
  [RemoteEndPoint] varchar(30) COLLATE Chinese_PRC_CI_AS  NULL,
  [HexCode] varchar(100) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_MachineReport_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'主键',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'Id'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'MCode'
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备编号（数字编号）',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'MachineNo'
GO

EXEC sp_addextendedproperty
'MS_Description', N'运行状态',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'RunState'
GO

EXEC sp_addextendedproperty
'MS_Description', N'CT(秒)',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineReport_T',
'COLUMN', N'CT'
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
  [MCode] int  NULL,
  [WarnCode] varchar(2) COLLATE Chinese_PRC_CI_AS  NULL,
  [WarnDesc] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[M_MachineWarnCode_T] SET (LOCK_ESCALATION = TABLE)
GO

EXEC sp_addextendedproperty
'MS_Description', N'设备代码编码',
'SCHEMA', N'dbo',
'TABLE', N'M_MachineWarnCode_T',
'COLUMN', N'MCode'
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
  [Area] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [StageType] varchar(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [WorkCode] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL
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
-- Table structure for S_SystemFile_T
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[S_SystemFile_T]') AND type IN ('U'))
	DROP TABLE [dbo].[S_SystemFile_T]
GO

CREATE TABLE [dbo].[S_SystemFile_T] (
  [FileId] int  IDENTITY(10000,1) NOT NULL,
  [FileName] nvarchar(50) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileSize] int  NOT NULL,
  [FileVersion] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileType] varchar(10) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [FileContent] varbinary(max)  NOT NULL,
  [FileTime] datetime  NOT NULL,
  [ProgramVersion] varchar(10) COLLATE Chinese_PRC_CI_AS  NULL,
  [UpdateStatus] char(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UpdateMode] char(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [Remark] nvarchar(100) COLLATE Chinese_PRC_CI_AS  NULL,
  [UseFlag] char(1) COLLATE Chinese_PRC_CI_AS  NOT NULL,
  [UpdateUser] varchar(10) COLLATE Chinese_PRC_CI_AS DEFAULT '' NOT NULL,
  [UpdateTime] datetime  NOT NULL
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
'MS_Description', N'系统文件表',
'SCHEMA', N'dbo',
'TABLE', N'S_SystemFile_T'
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
-- Primary Key structure for table M_ContactPerson_T
-- ----------------------------
ALTER TABLE [dbo].[M_ContactPerson_T] ADD CONSTRAINT [PK__M_Contac__9F8431E6D9FA19C5] PRIMARY KEY CLUSTERED ([WorkCode])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for M_ErrorRecord_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[M_ErrorRecord_T]', RESEED, 49290)
GO


-- ----------------------------
-- Primary Key structure for table M_ErrorRecord_T
-- ----------------------------
ALTER TABLE [dbo].[M_ErrorRecord_T] ADD CONSTRAINT [PK__M_ErrorR__3214EC0706BFAD7C] PRIMARY KEY CLUSTERED ([Id])
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
-- Primary Key structure for table M_HandlerInfo_T
-- ----------------------------
ALTER TABLE [dbo].[M_HandlerInfo_T] ADD CONSTRAINT [PK__M_Handle__9E0BCF753C099A6E] PRIMARY KEY CLUSTERED ([HandlerNo])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Primary Key structure for table M_LineInfo_T
-- ----------------------------
ALTER TABLE [dbo].[M_LineInfo_T] ADD CONSTRAINT [PK__M_LineIn__33DF7CA8C69E0121] PRIMARY KEY CLUSTERED ([Factory], [Line])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Uniques structure for table M_Machine_T
-- ----------------------------
ALTER TABLE [dbo].[M_Machine_T] ADD CONSTRAINT [qk_machine_code_no] UNIQUE NONCLUSTERED ([MCode] ASC, [MachineNo] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for M_MachineReport_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[M_MachineReport_T]', RESEED, 1128)
GO


-- ----------------------------
-- Indexes structure for table M_MachineReport_T
-- ----------------------------
CREATE NONCLUSTERED INDEX [index_report_machineId]
ON [dbo].[M_MachineReport_T] (
  [MCode] ASC
)
GO

CREATE NONCLUSTERED INDEX [index_report_createtime]
ON [dbo].[M_MachineReport_T] (
  [CreateTime] DESC
)
GO

CREATE NONCLUSTERED INDEX [index_report_id_time]
ON [dbo].[M_MachineReport_T] (
  [MCode] ASC,
  [CreateTime] DESC
)
GO


-- ----------------------------
-- Primary Key structure for table M_MachineReport_T
-- ----------------------------
ALTER TABLE [dbo].[M_MachineReport_T] ADD CONSTRAINT [PK__M_Machin__3214EC079C9CC1FB] PRIMARY KEY CLUSTERED ([Id])
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
ALTER TABLE [dbo].[M_MachineWarnCode_T] ADD CONSTRAINT [uk_warncode_mcode_warncode] UNIQUE NONCLUSTERED ([MCode] ASC, [WarnCode] ASC)
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
-- Auto increment value for S_SystemFile_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_SystemFile_T]', RESEED, 10042)
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
-- Auto increment value for S_WxMessage_T
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[S_WxMessage_T]', RESEED, 28527)
GO


-- ----------------------------
-- Primary Key structure for table S_WxMessage_T
-- ----------------------------
ALTER TABLE [dbo].[S_WxMessage_T] ADD CONSTRAINT [PK__S_WxMess__3214EC07FB6FC412] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

