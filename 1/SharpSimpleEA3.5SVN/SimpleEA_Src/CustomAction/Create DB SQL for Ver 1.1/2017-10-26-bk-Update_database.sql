/*****************************************************************************/
/* System           : SIMPLE EA SYSTEM DB CREATE                             */
/* Name             : DB CREATE SCRIPT                                       */
/* Description      :                                                        */
/* Author & Date    : SES  2010/07/08                                        */
/* Update           : SES JiJianxiong 2010/11/12                             */
/* Notes            :                                                        */
/*****************************************************************************/

/****************************************/
/*           Update DTATBASE            */
/****************************************/

/****************************************/
/*            CREATE TABLE FOR VER.1.1  */
/*             AND Add Defulat Record   */
/****************************************/
USE [SimpleEA]
GO
/****** Object:  Table [dbo].[LogInformation]******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 2011.01.07 Update By SES.jijianxiong ST
---- 2010.12.7 Update By SES.JiJianxiong ST
----IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogInformation]') AND type in (N'U'))
----BEGIN
----CREATE TABLE [dbo].[LogInformation](
----	[ID] [int] NOT NULL,
----	[UserID] [int] NOT NULL,
----	[SerialNumber] [nvarchar](10) NOT NULL,
----	[Jobuid] [nvarchar](25) NOT NULL,
----	[JobID] [int] NOT NULL,
----	[FunctionID] [int] NULL,
----	[PageID] [int] NULL,
----	[Number] [int] NULL,
----	[Time] [datetime] NOT NULL,
----	[MFPName] [nvarchar](50) NULL,
----	[PCName] [nvarchar](64) NULL,
----	[FileName] [nvarchar](256) NULL,
----	[Status] [int] NOT NULL,
----	[ErrorCode] [nvarchar](50) NULL,
----	[MFPPrintTaskID] [int] NULL,
----) ON [PRIMARY]
----END

--IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogInformation]') AND type in (N'U'))
--BEGIN
--CREATE TABLE [dbo].[LogInformation](
--	[ID] [int] NOT NULL,
--	[Time] [datetime] NOT NULL,
--	[UserID] [int] NOT NULL,
--	[Jobuid] [nvarchar](25) NOT NULL,
--	[SerialNumber] [nvarchar](10) NOT NULL,
--	[MFPName] [nvarchar](50) NULL,
--	[MFPModel] [nvarchar](max) NOT NULL,
--	[MFPIPAddress] [nvarchar](15) NOT NULL,
--	[Duplex] [int] NULL,
--	[JobID] [int] NOT NULL,
--	[FunctionID] [int] NULL,
--	[FileName] [nvarchar](256) NULL,
--	[PageID] [int] NULL,
--	[Number] [int] NULL,
--	[Status] [int] NOT NULL,
--	[ErrorCode] [nvarchar](50) NULL,
-- CONSTRAINT [PK_LogInformation] PRIMARY KEY CLUSTERED 
--(
--	[ID] ASC
--)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
--) ON [PRIMARY]
--END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LogInformation](
	[ID] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](10) NOT NULL,
	[LoginName] [nvarchar](10) NOT NULL,
	[GroupID] [int] NOT NULL,
	[GroupName] [nvarchar](30) NOT NULL,
	[Jobuid] [nvarchar](25) NOT NULL,
	[SerialNumber] [nvarchar](10) NOT NULL,
	[MFPName] [nvarchar](50) NULL,
	[MFPModel] [nvarchar](max) NOT NULL,
	[MFPIPAddress] [nvarchar](15) NOT NULL,
	[Duplex] [int] NULL,
	[JobID] [int] NOT NULL,
	[FunctionID] [int] NULL,
	[FileName] [nvarchar](256) NULL,
	[PageID] [int] NULL,
	[Number] [int] NULL,
	[Status] [int] NOT NULL,
	[ErrorCode] [nvarchar](50) NULL,
        [MFPPrintTaskID] [int] NULL,
 CONSTRAINT [PK_LogInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO

---- 2010.12.7 Update By SES.JiJianxiong ED
-- 2011.01.07 Update By SES.jijianxiong ED

/****** Object:  Table [dbo].[SettingDisp]******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- 2010.12.10 Update By SES.JiJianxiong ST
-- By Specification_SimpleEA(V1.28)_20101210

---- 2010.12.7 Update By SES.JiJianxiong ST
----IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingDisp]') AND type in (N'U'))
----BEGIN
----CREATE TABLE [dbo].[SettingDisp](
----	[Dis_U_UserName] [int] NOT NULL,
----	[Dis_U_LoginName] [int] NOT NULL,
----	[Dis_U_GroupName] [int] NOT NULL,
----	[Dis_U_CardID] [int] NOT NULL,
----	[Dis_U_Restrict] [int] NOT NULL,
----	[Dis_G_GroupName] [int] NOT NULL,
----	[Dis_G_Number] [int] NOT NULL,
----	[Dis_G_Restrict] [int] NOT NULL,
----	[Dis_R_Restrict] [int] NOT NULL,
----	[Dis_R_BWCopy] [int] NOT NULL,
----	[Dis_R_FCCopy] [int] NOT NULL,
----	[Dis_R_BWPrint] [int] NOT NULL,
----	[Dis_R_FCPrint] [int] NOT NULL,
----	[Dis_R_BWScan] [int] NOT NULL,
----	[Dis_R_FCScan] [int] NOT NULL,
----	[Dis_R_Fax] [int] NOT NULL,
----	[Dis_Job_Total] [int] NOT NULL,
----	[Dis_Job_BWTotal] [int] NOT NULL,
----	[Dis_Job_FCTotal] [int] NOT NULL,
----	[Dis_Result_Copy] [int] NOT NULL,
----	[Dis_Result_Print] [int] NOT NULL,
----	[Dis_Result_Scan] [int] NOT NULL,
----	[Dis_Result_Fax] [int] NOT NULL,
----	[Dis_Result_Other] [int] NOT NULL
----) ON [PRIMARY]
----END

--IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingDisp]') AND type in (N'U'))
--BEGIN
--CREATE TABLE [dbo].[SettingDisp](
--	[Dis_U_UserName] [int] NOT NULL,
--	[Dis_U_LoginName] [int] NOT NULL,
--	[Dis_U_GroupName] [int] NOT NULL,
--	[Dis_U_CardID] [int] NOT NULL,
--	[Dis_U_Restrict] [int] NOT NULL,
--	[Dis_G_GroupName] [int] NOT NULL,
--	[Dis_G_Number] [int] NOT NULL,
--	[Dis_G_Restrict] [int] NOT NULL,
--	[Dis_R_Restrict] [int] NOT NULL,
--	[Dis_R_Copy] [int] NOT NULL,
--	[Dis_R_Print] [int] NOT NULL,
--	[Dis_R_Scan] [int] NOT NULL,
--	[Dis_R_Fax] [int] NOT NULL,
--	[Dis_Job_Total] [int] NOT NULL,
--	[Dis_Job_CopyTotal] [int] NOT NULL,
--	[Dis_Job_PrintTotal] [int] NOT NULL,
--	[Dis_Job_ScanTotal] [int] NOT NULL,
--	[Dis_Job_FaxTotal] [int] NOT NULL,
--	[Dis_Result_Copy] [int] NOT NULL,
--	[Dis_Result_Print] [int] NOT NULL,
--	[Dis_Result_Scan] [int] NOT NULL,
--	[Dis_Result_Fax] [int] NOT NULL,
--	[Dis_Result_Other] [int] NOT NULL
--) ON [PRIMARY]
--END


--GO
--INSERT [dbo].[SettingDisp] ([Dis_U_UserName], [Dis_U_LoginName], [Dis_U_GroupName], [Dis_U_CardID], [Dis_U_Restrict], [Dis_G_GroupName], [Dis_G_Number], [Dis_G_Restrict], [Dis_R_Restrict], [Dis_R_Copy], [Dis_R_Print], [Dis_R_Scan], [Dis_R_Fax], [Dis_Job_Total], [Dis_Job_CopyTotal], [Dis_Job_PrintTotal], [Dis_Job_ScanTotal], [Dis_Job_FaxTotal], [Dis_Result_Copy], [Dis_Result_Print], [Dis_Result_Scan], [Dis_Result_Fax], [Dis_Result_Other]) VALUES (1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 1, 0 , 0 , 1, 1, 0, 0, 0)
---- 2010.12.7 Update By SES.JiJianxiong ED
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingDisp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettingDisp](
	[Dis_U_UserName] [int] NOT NULL,
	[Dis_U_LoginName] [int] NOT NULL,
	[Dis_U_GroupName] [int] NOT NULL,
	[Dis_U_CardID] [int] NOT NULL,
	[Dis_U_Restrict] [int] NOT NULL,
	[Dis_G_GroupName] [int] NOT NULL,
	[Dis_G_Number] [int] NOT NULL,
	[Dis_G_Restrict] [int] NOT NULL,
	[Dis_R_Restrict] [int] NOT NULL,
	[Dis_R_Copy] [int] NOT NULL,
	[Dis_R_Print] [int] NOT NULL,
	[Dis_R_Scan] [int] NOT NULL,
	[Dis_R_Fax] [int] NOT NULL,
	[Dis_Job_Total] [int] NOT NULL,
	[Dis_Job_CopyTotal] [int] NOT NULL,
	[Dis_Job_PrintTotal] [int] NOT NULL,
	[Dis_Job_ScanTotal] [int] NOT NULL,
	[Dis_Job_FaxTotal] [int] NOT NULL,
	[Dis_Result_Copy] [int] NOT NULL,
	[Dis_Result_Print] [int] NOT NULL,
	[Dis_Result_Scan] [int] NOT NULL,
	[Dis_Result_Fax] [int] NOT NULL,
	[Dis_Result_Other] [int] NOT NULL,
	[Dis_Log_MaxCount] [int] NOT NULL,
	[Dis_Avai_Borrow] [int] NOT NULL
) ON [PRIMARY]

INSERT [dbo].[SettingDisp] ([Dis_U_UserName], [Dis_U_LoginName], [Dis_U_GroupName], [Dis_U_CardID], [Dis_U_Restrict], [Dis_G_GroupName], [Dis_G_Number], [Dis_G_Restrict], [Dis_R_Restrict], [Dis_R_Copy], [Dis_R_Print], [Dis_R_Scan], [Dis_R_Fax], [Dis_Job_Total], [Dis_Job_CopyTotal], [Dis_Job_PrintTotal], [Dis_Job_ScanTotal], [Dis_Job_FaxTotal], [Dis_Result_Copy], [Dis_Result_Print], [Dis_Result_Scan], [Dis_Result_Fax], [Dis_Result_Other] , [Dis_Log_MaxCount] , [Dis_Avai_Borrow]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 , 1 , 1, 1, 1, 1, 1, 0, 0)

END


GO


-- 2010.12.10 Update By SES.JiJianxiong ED


/****** Object:  Table [dbo].[DeviceSession]******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceSession]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DeviceSession](
	[SerialNumber] [nvarchar](10) NOT NULL,
	[DeviceSession] [xml] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_DeviceSession] PRIMARY KEY CLUSTERED 
(
	[SerialNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END

GO

/****** Object:  Table [dbo].[DeviceSession]******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OSAErrorInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OSAErrorInformation](
	[ErrorCode] [nvarchar](50) NOT NULL,
	[ErrorInfo] [nvarchar](256) NULL,
 CONSTRAINT [PK_OSAErrorInformation] PRIMARY KEY CLUSTERED 
(
	[ErrorCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_CONTROL_SYSTEM_TROUBLE', N'故障： 控制系统故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_DEVELOPER_TROUBLE', N'故障： 开发人员故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_FATAL_ERROR_OTHER', N'故障： 其他故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_FAX_TROUBLE', N'故障： 传真故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_INFO_OTHER', N'消息： 其他消息。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_LIMITS_REACHED', N'故障： 操作达到限制条件的限制。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_NETWORK_TROUBLE', N'故障： 网路故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_OPERATIONAL_ERROR_OTHER', N'业务:  其他业务。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_OPTIONS_TROUBLE', N'故障： 选项故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_ENG_FEEDSYSTEM_TROUBLE', N'故障： 送纸系统故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_ENG_TRANSPORTSYSTEM_TROUBLE', N'故障： 纸张传送系统故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_ENG_TROUBLE', N'故障： 打印引擎故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_FEEDTRAY', N'业务:  输入纸盒故障（未关闭或缺纸）。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_OUTTRAY_FULL', N'业务:  输出纸盒已满。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_OUTTRAY_NOTEMPTY', N'业务:  输出纸盒不为空。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_PAPER_JAM', N'业务:  打印卡纸。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_PAPER_LOW', N'警告： 纸盒纸张不足。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_STAPLE', N'业务:  装订异常（Out of staples）。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PRINT_TONER_LOW', N'警告： 碳粉不足。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_PROCESS_CONTROL_TROUBLE', N'故障： 过程控制故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_RIC_TROUBLE', N'故障： 电子邮件故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SCAN_ORG_JAM', N'业务： 扫描卡纸。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SCAN_ORG_NOTDETECTED', N'业务:  原件无法检出。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SCAN_TROUBLE', N'故障： 扫描故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SERVICE_REQUIRED_TROUBLE', N'警告： 预警性服务（Preventive service required）。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SYS_DOOR_OPEN', N'业务： 门未关闭。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SYS_READY', N'消息： 系统已准备就绪。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_SYSTEM_TROUBLE', N'故障： 一般性系统故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_TONER_TROUBLE', N'故障： 碳粉故障。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'OSA_WARNING_OTHER', N'警告： 其他警告。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_OTHER', N'警告： 禁止连续操作。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_LOGINERROR', N'警告： 登录错误（错误的用户名或密码）。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_REGISTER_LIMIT', N'警告： 此MFP没有使用授权，请联系经销商。')

END

GO

/****** Object:  Table [dbo].[PaperSizeInformation] ******/

/****** Object:  Table [dbo].[PaperSizeInformation]    Script Date: 07/14/2014 17:14:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaperSizeInformation]') AND type in (N'U'))
DROP TABLE [dbo].[PaperSizeInformation]
GO
/****** Object:  Table [dbo].[PaperSizeInformation]    Script Date: 07/14/2014 17:14:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaperSizeInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PaperSizeInformation](
	[ID] [int] NOT NULL,
	[PaperSize] [nvarchar](40) NOT NULL,
	[PaperName] [nvarchar](100) NULL,
	[Comment] [nvarchar](50) NULL,
	[PaperTypeID] [int] NULL,
	[PaperTypeNM] [nvarchar](50) NULL,
 CONSTRAINT [PK_PaperSizeInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (1, N'A3W (12*18)', N'A3W (12*18)', N'', 1, N'A3')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (2, N'A3', N'A3', N'', 1, N'A3')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (3, N'A4', N'A4', N'', 2, N'A4')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (4, N'A5', N'A5', N'', 3, N'A5')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (5, N'B4', N'B4', N'', 4, N'B4')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (6, N'B5', N'B5', N'', 5, N'B5')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (7, N'Ledger/Tabloid (11*17)', N'Ledger/Tabloid (11*17)', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (8, N'Legal (8.5*14)', N'Legal (8.5*14)', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (9, N'Foolscap (8.5*13)', N'Foolscap (8.5*13)', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (10, N'Letter (8.5*11)', N'Letter (8.5*11)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (11, N'Executive (7.25*10.5)', N'Executive (7.25*10.5)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (12, N'Invoice (5.5*8.5)', N'Invoice (5.5*8.5)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (13, N'8K', N'8K', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (14, N'16K', N'16K', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (15, N'Japanese Hagaki Postcard /A6', N'Japanese Hagaki Postcard /A6', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (16, N'Monarch(envelope)', N'Monarch(envelope)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (17, N'DL(envelope)', N'DL(envelope)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (18, N'COM10(envelope)', N'COM10(envelope)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (19, N'C5(envelope)', N'C5(envelope)', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (20, N'Japanese Envelope Chou #3', N'Japanese Envelope Chou #3', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (21, N'Japanese Envelope You #2', N'Japanese Envelope You #2', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (22, N'Japanese Envelope You #4', N'Japanese Envelope You #4', N'', 7, N'Others(<A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (23, N'Custom', N'Custom', N'', 8, N'Others')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (24, N'Asian Legal (8.5*13.5)', N'Asian Legal (8.5*13.5)', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (25, N'Japanese Envelope Kaku #2', N'Japanese Envelope Kaku #2', N'', 6, N'Others(>A4)')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (26, N'Unused', N'Unused', N'Total number of jobs that doesn''t use papers. ', 8, N'Others')
INSERT [dbo].[PaperSizeInformation] ([ID], [PaperSize], [PaperName], [Comment], [PaperTypeID], [PaperTypeNM]) VALUES (27, N'Others', N'Others', N'Total number of other paper size.', 8, N'Others')