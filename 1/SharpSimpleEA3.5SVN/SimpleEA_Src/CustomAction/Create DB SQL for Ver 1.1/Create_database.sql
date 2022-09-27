USE [master]

/****** Object:  Database [SimpleEA] ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'SimpleEA')
DROP DATABASE [SimpleEA]
GO

GO
/****** Object:  Database [SimpleEA] ******/
CREATE DATABASE [SimpleEA]

GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleEA].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleEA] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleEA] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleEA] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleEA] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleEA] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleEA] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimpleEA] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleEA] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleEA] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleEA] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleEA] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleEA] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleEA] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleEA] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleEA] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleEA] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimpleEA] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleEA] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleEA] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleEA] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleEA] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleEA] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleEA] SET  READ_WRITE 
GO
ALTER DATABASE [SimpleEA] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SimpleEA] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleEA] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleEA] SET DB_CHAINING OFF 
GO

/****************************************/
/*            CREATE USER               */
/****************************************/
USE [master]
GO
/****** Object:  Login [AdminUser] ******/
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = N'admin')
CREATE LOGIN [admin] WITH PASSWORD=N'admin', DEFAULT_DATABASE=[SimpleEA], DEFAULT_LANGUAGE=[简体中文], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
EXEC sys.sp_addsrvrolemember @loginame = N'admin', @rolename = N'sysadmin'
GO
ALTER LOGIN [admin] ENABLE
GO

USE [SimpleEA]
GO
/****** Object:  Table [dbo].[CopyConfig]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyConfig]') AND type in (N'U'))
DROP TABLE [dbo].[CopyConfig]
GO
/****** Object:  Table [dbo].[DBThirdAuthConfig]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBThirdAuthConfig]') AND type in (N'U'))
DROP TABLE [dbo].[DBThirdAuthConfig]
GO
/****** Object:  Table [dbo].[DBThirdAuthSetting]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBThirdAuthSetting]') AND type in (N'U'))
DROP TABLE [dbo].[DBThirdAuthSetting]
GO
/****** Object:  Table [dbo].[DeviceSession]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeviceSession]') AND type in (N'U'))
DROP TABLE [dbo].[DeviceSession]
GO
/****** Object:  Table [dbo].[DropdownSetting]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropdownSetting]') AND type in (N'U'))
DROP TABLE [dbo].[DropdownSetting]
GO
/****** Object:  Table [dbo].[FunctionTypeInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionTypeInformation]') AND type in (N'U'))
DROP TABLE [dbo].[FunctionTypeInformation]
GO
/****** Object:  Table [dbo].[GroupInfo]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupInfo]') AND type in (N'U'))
DROP TABLE [dbo].[GroupInfo]
GO
/****** Object:  Table [dbo].[JobInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobInformation]') AND type in (N'U'))
DROP TABLE [dbo].[JobInformation]
GO
/****** Object:  Table [dbo].[JobTypeInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobTypeInformation]') AND type in (N'U'))
DROP TABLE [dbo].[JobTypeInformation]
GO
/****** Object:  Table [dbo].[LDAPSetting]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LDAPSetting]') AND type in (N'U'))
DROP TABLE [dbo].[LDAPSetting]
GO
/****** Object:  Table [dbo].[LogInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogInformation]') AND type in (N'U'))
DROP TABLE [dbo].[LogInformation]
GO
/****** Object:  Table [dbo].[MFPInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MFPInformation_Label]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MFPInformation] DROP CONSTRAINT [DF_MFPInformation_Label]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPInformation]') AND type in (N'U'))
DROP TABLE [dbo].[MFPInformation]
GO
/****** Object:  Table [dbo].[MFPPrintTask]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPPrintTask]') AND type in (N'U'))
DROP TABLE [dbo].[MFPPrintTask]
GO
/****** Object:  Table [dbo].[MFPUserRes]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPUserRes]') AND type in (N'U'))
DROP TABLE [dbo].[MFPUserRes]
GO
/****** Object:  Table [dbo].[OSAErrorInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OSAErrorInformation]') AND type in (N'U'))
DROP TABLE [dbo].[OSAErrorInformation]
GO
/****** Object:  Table [dbo].[PaperSizeInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PaperSizeInformation]') AND type in (N'U'))
DROP TABLE [dbo].[PaperSizeInformation]
GO
/****** Object:  Table [dbo].[PriceDetail]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PriceDetail_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PriceDetail] DROP CONSTRAINT [DF_PriceDetail_CreateTime]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceDetail]') AND type in (N'U'))
DROP TABLE [dbo].[PriceDetail]
GO
/****** Object:  Table [dbo].[PriceMaster]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PriceMaster_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PriceMaster] DROP CONSTRAINT [DF_PriceMaster_CreateTime]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PriceMaster]
GO
/****** Object:  Table [dbo].[PrintCopy]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrintCopy]') AND type in (N'U'))
DROP TABLE [dbo].[PrintCopy]
GO
/****** Object:  Table [dbo].[RestrictionInfo]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RestrictionInfo_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RestrictionInfo] DROP CONSTRAINT [DF_RestrictionInfo_CreateTime]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestrictionInfo]') AND type in (N'U'))
DROP TABLE [dbo].[RestrictionInfo]
GO
/****** Object:  Table [dbo].[RestrictionInformation]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestrictionInformation]') AND type in (N'U'))
DROP TABLE [dbo].[RestrictionInformation]
GO
/****** Object:  Table [dbo].[SettingDisp]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettingDisp_Login_Auth_method]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettingDisp] DROP CONSTRAINT [DF_SettingDisp_Login_Auth_method]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingDisp]') AND type in (N'U'))
DROP TABLE [dbo].[SettingDisp]
GO
/****** Object:  Table [dbo].[SettingManagement]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingManagement]') AND type in (N'U'))
DROP TABLE [dbo].[SettingManagement]
GO
/****** Object:  Table [dbo].[SettingMonitor]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingMonitor]') AND type in (N'U'))
DROP TABLE [dbo].[SettingMonitor]
GO
/****** Object:  Table [dbo].[SettinServerIP]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_TongxinType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] DROP CONSTRAINT [DF_SettinServerIP_TongxinType]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_TongxinAddr]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] DROP CONSTRAINT [DF_SettinServerIP_TongxinAddr]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_LoginType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] DROP CONSTRAINT [DF_SettinServerIP_LoginType]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_ColLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] DROP CONSTRAINT [DF_SettinServerIP_ColLimit]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_PaperLimit]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] DROP CONSTRAINT [DF_SettinServerIP_PaperLimit]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettinServerIP]') AND type in (N'U'))
DROP TABLE [dbo].[SettinServerIP]
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 10/31/2017 09:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInfo]') AND type in (N'U'))
DROP TABLE [dbo].[UserInfo]
GO
/****** Object:  Table [dbo].[UserPayDetail]    Script Date: 10/31/2017 09:25:28 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserPayDetail_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPayDetail] DROP CONSTRAINT [DF_UserPayDetail_CreateTime]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPayDetail]') AND type in (N'U'))
DROP TABLE [dbo].[UserPayDetail]
GO
/****** Object:  Table [dbo].[UserPayDetail]    Script Date: 10/31/2017 09:25:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserPayDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserPayDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Money] [numeric](7, 2) NOT NULL,
	[ColorMoney] [numeric](7, 2) NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_UserPayDetail_CreateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_UserPayDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserInfo](
	[ID] [int] NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[LoginName] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](30) NOT NULL,
	[ICCardID] [nvarchar](20) NULL,
	[PinCode] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[GroupID] [int] NOT NULL,
	[RestrictionID] [int] NULL,
	[ComeFrom] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[UserInfo] ([ID], [UserName], [LoginName], [Password], [ICCardID], [PinCode], [Email], [GroupID], [RestrictionID], [ComeFrom], [CreateTime], [UpdateTime]) VALUES (-1, N'secuadmin', N'secuadmin', N'secuadmin', null, null, null, 0, 0, 0, CAST(0x002D247F00000000 AS DateTime), CAST(0x002D247F00000000 AS DateTime))
INSERT [dbo].[UserInfo] ([ID], [UserName], [LoginName], [Password], [ICCardID], [PinCode], [Email], [GroupID], [RestrictionID], [ComeFrom], [CreateTime], [UpdateTime]) VALUES (0, N'admin', N'admin', N'admin', null, null, null, 0, 0, 0, CAST(0x002D247F00000000 AS DateTime), CAST(0x002D247F00000000 AS DateTime))
/****** Object:  Table [dbo].[SettinServerIP]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettinServerIP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettinServerIP](
	[IPAddress] [nvarchar](15) NULL,
	[TongxinType] [nvarchar](1) NULL CONSTRAINT [DF_SettinServerIP_TongxinType]  DEFAULT ((0)),
	[TongxinAddr] [nvarchar](100) NULL CONSTRAINT [DF_SettinServerIP_TongxinAddr]  DEFAULT (''),
	[LoginType] [nvarchar](1) NULL CONSTRAINT [DF_SettinServerIP_LoginType]  DEFAULT ((0)),
	[ColLimit] [int] NOT NULL CONSTRAINT [DF_SettinServerIP_ColLimit]  DEFAULT ((50)),
	[PaperLimit] [int] NOT NULL CONSTRAINT [DF_SettinServerIP_PaperLimit]  DEFAULT ((50)),
	[ServerIP] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](100) NULL,
	[EmailUserName] [nvarchar](50) NULL,
	[EmailPassword] [nvarchar](50) NULL,
	[CorpID] [nvarchar](50) NULL,
	[Secret] [nvarchar](200) NULL,
	[AgentID] [int] NULL,
	[CompanyInfo] [nvarchar](400) NULL
) ON [PRIMARY]
END
GO
INSERT INTO [SimpleEA].[dbo].[SettinServerIP]([IPAddress] ,[TongxinType],[TongxinAddr],[LoginType],[ColLimit],[PaperLimit],[ServerIP],[EmailAddress],[EmailUserName],[EmailPassword],[CorpID],[Secret],[AgentID],[CompanyInfo])VALUES('','0','','0',20 ,20 ,'' ,'' ,'' ,'' ,'' ,'' ,0 ,'')

/****** Object:  Table [dbo].[SettingMonitor]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingMonitor]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettingMonitor](
	[MonitorPdfFolder] [nvarchar](100) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[SettingManagement]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingManagement]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettingManagement](
	[SetPeriod] [int] NOT NULL,
	[SetPeriodTime] [int] NOT NULL,
	[SetTime] [int] NOT NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[SettingManagement] ([SetPeriod], [SetPeriodTime], [SetTime]) VALUES (4, 0, 24)
/****** Object:  Table [dbo].[SettingDisp]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[Dis_Avai_Borrow] [int] NOT NULL,
	[Dis_Count_mode] [int] NOT NULL,
	[Dis_A3_A4] [int] NULL,
	[Login_Auth_method] [int] NULL CONSTRAINT [DF_SettingDisp_Login_Auth_method]  DEFAULT ((0))
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'SettingDisp', N'COLUMN',N'Login_Auth_method'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录认证方式' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SettingDisp', @level2type=N'COLUMN',@level2name=N'Login_Auth_method'
GO
INSERT [dbo].[SettingDisp] ([Dis_U_UserName], [Dis_U_LoginName], [Dis_U_GroupName], [Dis_U_CardID], [Dis_U_Restrict], [Dis_G_GroupName], [Dis_G_Number], [Dis_G_Restrict], [Dis_R_Restrict], [Dis_R_Copy], [Dis_R_Print], [Dis_R_Scan], [Dis_R_Fax], [Dis_Job_Total], [Dis_Job_CopyTotal], [Dis_Job_PrintTotal], [Dis_Job_ScanTotal], [Dis_Job_FaxTotal], [Dis_Result_Copy], [Dis_Result_Print], [Dis_Result_Scan], [Dis_Result_Fax], [Dis_Result_Other], [Dis_Log_MaxCount], [Dis_Avai_Borrow], [Dis_Count_mode], [Dis_A3_A4], [Login_Auth_method]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0)
/****** Object:  Table [dbo].[RestrictionInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestrictionInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RestrictionInformation](
	[RestrictionID] [int] NOT NULL,
	[JobId] [int] NOT NULL,
	[FunctionId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[LimitNum] [int] NULL,
 CONSTRAINT [PK_RestrictionInformation] PRIMARY KEY CLUSTERED 
(
	[RestrictionID] ASC,
	[JobId] ASC,
	[FunctionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 1, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 1, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 2, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 2, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 3, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 3, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 4, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 4, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 5, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 5, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 6, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 6, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 7, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 7, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 8, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (-1, 8, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 1, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 1, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 2, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 2, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 3, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 3, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 4, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 4, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 5, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 5, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 6, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 6, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 7, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 7, 2, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 8, 1, 1, 0)
INSERT [dbo].[RestrictionInformation] ([RestrictionID], [JobId], [FunctionId], [Status], [LimitNum]) VALUES (0, 8, 2, 1, 0)
/****** Object:  Table [dbo].[RestrictionInfo]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RestrictionInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RestrictionInfo](
	[ID] [int] NOT NULL,
	[RestrictionName] [nvarchar](30) NOT NULL,
	[AllQuota] [numeric](7, 2) NULL,
	[ColorQuota] [numeric](7, 2) NULL,
	[OverLimit] [numeric](7, 2) NULL,
	[PrintBW] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_RestrictionInfo_CreateTime]  DEFAULT (getdate()),
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_RestrictionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[RestrictionInfo] ([ID], [RestrictionName], [AllQuota], [ColorQuota], [OverLimit], [PrintBW], [CreateTime], [UpdateTime]) VALUES (-1, N' 继承用户组', CAST(0.00 AS Numeric(7, 2)), CAST(0.00 AS Numeric(7, 2)), CAST(0.00 AS Numeric(7, 2)), 0, CAST(0x0000A32500ABD80B AS DateTime), CAST(0x0000A32500ABD80B AS DateTime))
INSERT [dbo].[RestrictionInfo] ([ID], [RestrictionName], [AllQuota], [ColorQuota], [OverLimit], [PrintBW], [CreateTime], [UpdateTime]) VALUES (0, N' 通用配额', CAST(100.00 AS Numeric(7, 2)), CAST(50.00 AS Numeric(7, 2)), CAST(10.00 AS Numeric(7, 2)), 0, CAST(0x002D247F018B8185 AS DateTime), CAST(0x0000A33000A921F0 AS DateTime))
/****** Object:  Table [dbo].[PrintCopy]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PrintCopy]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PrintCopy](
	[ID] [int] NOT NULL,
	[Time] [datetime] NULL,
	[UserID] [int] NULL,
	[IpAddress] [nvarchar](50) NULL,
	[CopyType] [int] NULL,
	[OrigFile] [nvarchar](50) NULL,
	[CopyFile] [nvarchar](50) NULL,
	[Finished] [int] NULL,
	[CopyTimes] [int] NULL,
 CONSTRAINT [PK_PrintCopy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PriceMaster]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PriceMaster](
	[PriceID] [int] NOT NULL,
	[PriceNM] [nvarchar](50) NOT NULL,
	[PriceCalMode] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_PriceMaster_CreateTime]  DEFAULT (getdate()),
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PriceMaster] PRIMARY KEY CLUSTERED 
(
	[PriceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[PriceMaster] ([PriceID], [PriceNM], [PriceCalMode], [CreateTime], [UpdateTime]) VALUES (0, N' 默认价格', 0, CAST(0x0000A31001371232 AS DateTime), CAST(0x0000A33B00B80883 AS DateTime))
/****** Object:  Table [dbo].[PriceDetail]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PriceDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PriceDetail](
	[PriceDetailID] [int] IDENTITY(1,1) NOT NULL,
	[PriceID] [int] NOT NULL,
	[PaperTypeID] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[PaperPrice] [numeric](7, 2) NULL,
	[GrayPrice] [numeric](7, 2) NULL,
	[ColorPrice] [numeric](7, 2) NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_PriceDetail_CreateTime]  DEFAULT (getdate()),
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PriceDetail] PRIMARY KEY CLUSTERED 
(
	[PriceDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[PriceDetail] ON
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (268, 0, 1, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80883 AS DateTime), CAST(0x0000A33B00B80883 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (269, 0, 2, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80884 AS DateTime), CAST(0x0000A33B00B80884 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (270, 0, 3, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80884 AS DateTime), CAST(0x0000A33B00B80884 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (271, 0, 4, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80884 AS DateTime), CAST(0x0000A33B00B80884 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (272, 0, 5, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80884 AS DateTime), CAST(0x0000A33B00B80884 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (273, 0, 6, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80886 AS DateTime), CAST(0x0000A33B00B80886 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (274, 0, 7, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (275, 0, 8, 1, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (276, 0, 1, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (277, 0, 2, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (278, 0, 3, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (279, 0, 4, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (280, 0, 5, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (281, 0, 6, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (282, 0, 7, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80887 AS DateTime), CAST(0x0000A33B00B80887 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (283, 0, 8, 2, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (284, 0, 1, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (285, 0, 2, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (286, 0, 3, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (287, 0, 4, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (288, 0, 5, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (289, 0, 6, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (290, 0, 7, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (291, 0, 8, 6, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80888 AS DateTime), CAST(0x0000A33B00B80888 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (292, 0, 1, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (293, 0, 2, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (294, 0, 3, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (295, 0, 4, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (296, 0, 5, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (297, 0, 6, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (298, 0, 7, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
INSERT [dbo].[PriceDetail] ([PriceDetailID], [PriceID], [PaperTypeID], [JobID], [PaperPrice], [GrayPrice], [ColorPrice], [CreateTime], [UpdateTime]) VALUES (299, 0, 8, 8, CAST(0.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(1.00 AS Numeric(7, 2)), CAST(0x0000A33B00B80889 AS DateTime), CAST(0x0000A33B00B80889 AS DateTime))
SET IDENTITY_INSERT [dbo].[PriceDetail] OFF
/****** Object:  Table [dbo].[PaperSizeInformation]    Script Date: 10/31/2017 09:25:27 ******/
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
/****** Object:  Table [dbo].[OSAErrorInformation]    Script Date: 10/31/2017 09:25:27 ******/
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
END
GO
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
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_LOGINERROR', N'警告： 登录错误（错误的用户名或密码）。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_OTHER', N'警告： 禁止连续操作。')
INSERT [dbo].[OSAErrorInformation] ([ErrorCode], [ErrorInfo]) VALUES (N'SIMPLEEA_REGISTER_LIMIT', N'警告： 此MFP没有使用授权，请联系经销商。')
/****** Object:  Table [dbo].[MFPUserRes]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPUserRes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFPUserRes](
	[UserID] [int] NOT NULL,
	[IPAddress] [nchar](15) NOT NULL,
 CONSTRAINT [PK_MFPUserRes] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[IPAddress] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MFPPrintTask]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPPrintTask]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFPPrintTask](
	[MFPPrintTaskID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](30) NOT NULL,
	[DiskFileName] [nvarchar](50) NOT NULL,
	[FileLocation] [nvarchar](300) NOT NULL,
	[PrintFileName] [nvarchar](80) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[State] [nchar](1) NOT NULL,
 CONSTRAINT [PK_MFPPrintTask] PRIMARY KEY CLUSTERED 
(
	[MFPPrintTaskID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[MFPInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFPInformation](
	[SerialNumber] [nvarchar](10) NOT NULL,
	[ModelName] [nvarchar](max) NOT NULL,
	[IPAddress] [nvarchar](15) NOT NULL,
	[Location] [nvarchar](50) NULL,
	[AdministratorID] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[PriceID] [int] NULL,
	[Label] [int] NULL CONSTRAINT [DF_MFPInformation_Label]  DEFAULT ((0)),
	[Monitor] [int] NULL,
	[Prompt] [nvarchar](max) NULL,
 CONSTRAINT [PK_MFPInformation] PRIMARY KEY CLUSTERED 
(
	[SerialNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LogInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LogInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LogInformation](
	[ID] [int] NOT NULL,
	[Time] [datetime] NOT NULL,
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](30) NOT NULL,
	[LoginName] [nvarchar](30) NOT NULL,
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
	[PapeCount] [int] NULL,
	[CopyCount] [int] NULL,
	[SpendMoney] [numeric](7, 2) NULL,
	[PriceDetailID] [int] NULL,
	[Status] [int] NOT NULL,
	[ErrorCode] [nvarchar](50) NULL,
	[MFPPrintTaskID] [int] NULL,
	[DspNumber] [int] NULL,
	[DspPapeCount] [int] NULL,
 CONSTRAINT [PK_LogInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LDAPSetting]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LDAPSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LDAPSetting](
	[Con_IP] [nvarchar](50) NULL,
	[Con_Port] [nvarchar](50) NULL,
	[Con_Verification] [nvarchar](50) NULL,
	[Con_Account] [nvarchar](50) NULL,
	[Con_Password] [nvarchar](50) NULL,
	[DB_Allowed] [int] NULL,
	[Ver_Verification] [nvarchar](50) NULL,
	[Ver_Login] [nvarchar](50) NULL,
	[Ver_Type] [nvarchar](50) NULL,
	[Ver_NTorDNS] [nvarchar](50) NULL,
	[Group_Verification] [nvarchar](50) NULL,
	[Group_UserAttribute_or_DN] [nvarchar](50) NULL,
	[Group_AttributeName] [nvarchar](50) NULL,
	[User_DNSetting] [nvarchar](100) NULL,
	[User_Search] [nvarchar](50) NULL,
	[User_Name] [nvarchar](50) NULL,
	[User_Email] [nvarchar](50) NULL,
	[User_ICNum] [nvarchar](50) NULL,
	[User_LDAPName] [nvarchar](50) NULL,
	[User_LDAPPassword] [nvarchar](50) NULL,
	[Syn_Month] [nvarchar](50) NULL,
	[Syn_Week] [nvarchar](50) NULL,
	[Syn_Time] [nvarchar](50) NULL,
	[Quota_Using] [nvarchar](50) NULL,
	[Syn_label] [nvarchar](300) NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[LDAPSetting] ([Con_IP], [Con_Port], [Con_Verification], [Con_Account], [Con_Password], [DB_Allowed], [Ver_Verification], [Ver_Login], [Ver_Type], [Ver_NTorDNS], [Group_Verification], [Group_UserAttribute_or_DN], [Group_AttributeName], [User_DNSetting], [User_Search], [User_Name], [User_Email], [User_ICNum], [User_LDAPName], [User_LDAPPassword], [Syn_Month], [Syn_Week], [Syn_Time], [Quota_Using], [Syn_label]) VALUES (N'127.0.0.1', N'389', N'none', N'', N'', 0, N'none', N'ggggg', N'NT Domain Name', N'', N'', N' User Attribute', N'', N'DC=example,DC=com', N'Base', N'displayName', N'mail', N'', N'', N'', N'1', N'1', N'1', N'1', N'1')
/****** Object:  Table [dbo].[JobTypeInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobTypeInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[JobTypeInformation](
	[ID] [int] NOT NULL,
	[JobName] [nvarchar](50) NOT NULL,
	[JobNameDisp] [nvarchar](50) NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_JobTypeInformation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (0, N'Other', N'其他', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (1, N'Copy', N'复印', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (2, N'Print', N'打印', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (3, N'Document Filing Print', N'文件归档打印', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (4, N'Scan Save', N'扫描并保存', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (5, N'List Print', N'清单打印', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (6, N'Scan', N'扫描', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (7, N'Internet Fax', N'网络传真', NULL)
INSERT [dbo].[JobTypeInformation] ([ID], [JobName], [JobNameDisp], [Comment]) VALUES (8, N'Fax', N'传真', NULL)
/****** Object:  Table [dbo].[JobInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JobInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[JobInformation](
	[UserID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[FunctionID] [int] NOT NULL,
	[PageID] [int] NOT NULL,
	[Number] [int] NOT NULL,
	[PapeCount] [int] NULL,
	[CopyCount] [int] NULL,
	[Duplex] [int] NULL,
	[SpendMoney] [numeric](7, 2) NULL,
	[PriceDetailID] [int] NULL,
	[SerialNumber] [nvarchar](10) NOT NULL,
	[Time] [datetime] NOT NULL,
	[DspNumber] [int] NULL,
	[DspPapeCount] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GroupInfo]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupInfo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupInfo](
	[ID] [int] NOT NULL,
	[GroupName] [nvarchar](30) NOT NULL,
	[RestrictionID] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupInfo] PRIMARY KEY CLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[GroupInfo] ([ID], [GroupName], [RestrictionID], [CreateTime], [UpdateTime]) VALUES (0, N'无所属', 0, CAST(0x002D247F018B8185 AS DateTime), CAST(0x002D247F018B8185 AS DateTime))
/****** Object:  Table [dbo].[FunctionTypeInformation]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FunctionTypeInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[FunctionTypeInformation](
	[JobID] [int] NOT NULL,
	[FunctionID] [int] NOT NULL,
	[FunctionName] [nvarchar](50) NOT NULL,
	[FunctionNameDisp] [nvarchar](50) NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_FunctionTypeInformation] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC,
	[FunctionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (0, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (0, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (1, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (1, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (2, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (2, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (3, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (3, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (4, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (4, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (5, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (5, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (6, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (6, 2, N'Full Color', N'彩色', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (7, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (8, 1, N'B/W', N'黑白', NULL)
INSERT [dbo].[FunctionTypeInformation] ([JobID], [FunctionID], [FunctionName], [FunctionNameDisp], [Comment]) VALUES (8, 2, N'(Channel2)', N'(Channel2)', NULL)
/****** Object:  Table [dbo].[DropdownSetting]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DropdownSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DropdownSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CD] [int] NULL,
	[code] [int] NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_DropdownSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[DropdownSetting] ON
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (1, 1, 1, N'AAAA')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (2, 1, 2, N'BBBB')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (3, 1, 3, N'CCCC')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (4, 1, 4, N'DDDDD')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (5, 1, 5, N'ggggg')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (6, 3, 1, N'DC=example,DC=com')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (7, 4, 1, N'displayName')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (8, 5, 1, N'mail')
INSERT [dbo].[DropdownSetting] ([ID], [CD], [code], [name]) VALUES (9, 6, 1, N'telephoneNumber')
SET IDENTITY_INSERT [dbo].[DropdownSetting] OFF
/****** Object:  Table [dbo].[DeviceSession]    Script Date: 10/31/2017 09:25:27 ******/
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
INSERT [dbo].[DeviceSession] ([SerialNumber], [DeviceSession], [CreateDate], [UpdateDate]) VALUES (N'3502991X00', N'<DeviceSession xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"><deviceinfo uuid="SN3502991X00MNSHARP MX-2648NC"><serial-number xmlns="urn:schemas-sc-jp:mfp:osa-1-1">3502991X00</serial-number><modelname xmlns="urn:schemas-sc-jp:mfp:osa-1-1">SHARP MX-2648NC</modelname><location xmlns="urn:schemas-sc-jp:mfp:osa-1-1" /><mac_address xmlns="urn:schemas-sc-jp:mfp:osa-1-1">34:F6:2D:9B:11:12</mac_address><network_address xmlns="urn:schemas-sc-jp:mfp:osa-1-1">202.120.87.215</network_address></deviceinfo><xmldocacl><user-info xmlns="urn:schemas-sc-jp:mfp:osa-1-1"><account-id xsi:nil="true" /></user-info><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="COMMON" allow="false"><setting-constraints><property sys-name="duplex-mode"><setting sys-name="SIMPLEX">false</setting><setting sys-name="DUPLEX">false</setting></property><property sys-name="finishing"><setting sys-name="STAPLE">false</setting><setting sys-name="PUNCH">false</setting><setting sys-name="FOLD">false</setting><setting sys-name="SADDLE-STITCH">false</setting></property></setting-constraints></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="PRINT" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="MONOCHROME">false</setting><setting sys-name="SINGLE-COLOR">false</setting><setting sys-name="DUAL-COLOR">false</setting><setting sys-name="TRIPLE-COLOR">false</setting><setting sys-name="FULL-COLOR">false</setting></property><property sys-name="prohibit-toner-save"><setting sys-name="NO-TONER-SAVE">false</setting><setting sys-name="TONER-SAVE-1">false</setting><setting sys-name="TONER-SAVE-2">false</setting><setting sys-name="TONER-SAVE-3">false</setting></property></setting-constraints><sub-feature sys-name="FTP-PULL-PRINT" allow="false" /><sub-feature sys-name="USB-DIRECT-PRINT" allow="false" /><sub-feature sys-name="SMB-PULL-PRINT" allow="false" /><sub-feature sys-name="SEND-WHILE-PRINT" allow="false" /><sub-feature sys-name="CREATE-PC-BROWSE-PDF" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="COPY" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="MONOCHROME">false</setting><setting sys-name="FULL-COLOR">false</setting><setting sys-name="SINGLE-COLOR">false</setting><setting sys-name="DUAL-COLOR">false</setting></property><property sys-name="prohibit-toner-save"><setting sys-name="NO-TONER-SAVE">false</setting><setting sys-name="TONER-SAVE-1">false</setting><setting sys-name="TONER-SAVE-2">false</setting><setting sys-name="TONER-SAVE-3">false</setting></property></setting-constraints><sub-feature sys-name="SPECIAL-MODES" allow="false" /><sub-feature sys-name="SEND-WHILE-COPY" allow="false" /><sub-feature sys-name="CREATE-PC-BROWSE-PDF" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="IMAGE-SEND" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="FULL-COLOR">false</setting></property></setting-constraints><sub-feature sys-name="DIRECT-ADDRESS-ENTRY" allow="false"><details><detail sys-name="EMAIL-SEND">false</detail><detail sys-name="NETWORK-FOLDER">false</detail></details></sub-feature><sub-feature sys-name="LOCAL-ADDRESS-BOOK" allow="false"><details><detail sys-name="EMAIL-SEND">false</detail><detail sys-name="NETWORK-FOLDER">false</detail></details></sub-feature><sub-feature sys-name="GLOBAL-ADDRESS-BOOK" allow="false"><details><detail sys-name="ADDRESS-BOOK-1">false</detail><detail sys-name="ADDRESS-BOOK-2">false</detail><detail sys-name="ADDRESS-BOOK-3">false</detail><detail sys-name="ADDRESS-BOOK-4">false</detail><detail sys-name="ADDRESS-BOOK-5">false</detail><detail sys-name="ADDRESS-BOOK-6">false</detail><detail sys-name="ADDRESS-BOOK-7">false</detail></details></sub-feature><sub-feature sys-name="EMAIL-SEND" allow="false" /><sub-feature sys-name="FTP-SEND" allow="false" /><sub-feature sys-name="NETWORK-FOLDER" allow="false" /><sub-feature sys-name="REMOTE-PC-SCAN" allow="false" /><sub-feature sys-name="DESKTOP" allow="false" /><sub-feature sys-name="SPECIAL-MODES" allow="false" /><sub-feature sys-name="PROGRAM-REGISTRATION" allow="false" /><sub-feature sys-name="CREATE-PC-BROWSE-PDF" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="SCAN-TO-HDD" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="MONOCHROME">false</setting><setting sys-name="FULL-COLOR">false</setting><setting sys-name="DUAL-COLOR">false</setting></property></setting-constraints><sub-feature sys-name="SPECIAL-MODES" allow="false" /><sub-feature sys-name="CREATE-PC-BROWSE-PDF" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="SCAN-TO-EXTERNAL-MEMORY" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="MONOCHROME">false</setting><setting sys-name="FULL-COLOR">false</setting></property></setting-constraints><sub-feature sys-name="SPECIAL-MODES" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="DOC-FILING-PRINT" allow="false"><setting-constraints><property sys-name="color-mode"><setting sys-name="MONOCHROME">false</setting><setting sys-name="FULL-COLOR">false</setting><setting sys-name="DUAL-COLOR">false</setting><setting sys-name="SINGLE-COLOR">false</setting></property><property sys-name="prohibit-toner-save"><setting sys-name="NO-TONER-SAVE">false</setting><setting sys-name="TONER-SAVE-1">false</setting><setting sys-name="TONER-SAVE-2">false</setting><setting sys-name="TONER-SAVE-3">false</setting></property></setting-constraints><sub-feature sys-name="SPECIAL-MODES" allow="false" /><sub-feature sys-name="PREVIEW" allow="false" /><sub-feature sys-name="DISPLAY-LOGINUSER-FILES-ONLY" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="SHARP-OSA" allow="false"><sub-feature sys-name="STANDARD-APPLICATION" allow="false"><details><detail sys-name="APPLICATION-1">false</detail><detail sys-name="APPLICATION-2">false</detail><detail sys-name="APPLICATION-3">false</detail><detail sys-name="APPLICATION-4">false</detail><detail sys-name="APPLICATION-5">false</detail><detail sys-name="APPLICATION-6">false</detail><detail sys-name="APPLICATION-7">false</detail><detail sys-name="APPLICATION-8">false</detail><detail sys-name="APPLICATION-9">false</detail><detail sys-name="APPLICATION-10">false</detail><detail sys-name="APPLICATION-11">false</detail><detail sys-name="APPLICATION-12">false</detail><detail sys-name="APPLICATION-13">false</detail><detail sys-name="APPLICATION-14">false</detail><detail sys-name="APPLICATION-15">false</detail><detail sys-name="APPLICATION-16">false</detail></details></sub-feature></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="SECURITY" allow="false"><sub-feature sys-name="HIDDEN-PATTERN-DIRECT-ENTRY" allow="false" /></mfp-feature><mfp-feature xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="SETTINGS" allow="false"><sub-feature sys-name="SYSTEM-SETTINGS" allow="false"><details><detail sys-name="TOTAL-COUNT">false</detail><detail sys-name="DISPLAY-CONTRAST">false</detail><detail sys-name="CLOCK">false</detail><detail sys-name="KEYBOARD-SELECT">false</detail><detail sys-name="LIST-PRINT-USER">false</detail><detail sys-name="BYPASS-TRAY-EXCLUDED">false</detail><detail sys-name="BYPASS-TRAY">false</detail><detail sys-name="ADDRESS-CONTROL">false</detail><detail sys-name="FAX-DATA-RECEIVE">false</detail><detail sys-name="PRINTER-DEFAULT-SETTINGS">false</detail><detail sys-name="PCL-SETTINGS">false</detail><detail sys-name="PS-SETTINGS">false</detail><detail sys-name="DOC-FILING-CONTROL">false</detail><detail sys-name="USB-DEVICE-CHECK">false</detail><detail sys-name="USER-CONTROL">false</detail><detail sys-name="ENERGY-SAVE">false</detail><detail sys-name="OPERATION-SETTINGS">false</detail><detail sys-name="DEVICE-SETTINGS">false</detail><detail sys-name="COPY-SETTINGS">false</detail><detail sys-name="NETWORK-SETTINGS">false</detail><detail sys-name="PRINTER-SETTINGS">false</detail><detail sys-name="IMAGESEND-OPERATION-SETTINGS">false</detail><detail sys-name="IMAGESEND-SCANNER-SETTINGS">false</detail><detail sys-name="IMAGESEND-FAX-SETTINGS">false</detail><detail sys-name="IMAGESEND-IFAX-SETTINGS">false</detail><detail sys-name="DOC-FILING-SETTINGS">false</detail><detail sys-name="LIST-PRINT-ADMIN">false</detail><detail sys-name="SECURITY-SETTINGS">false</detail><detail sys-name="PRODUCT-KEY">false</detail><detail sys-name="ESCP-SETTINGS">false</detail><detail sys-name="ENABLE-DISABLE-SETTINGS">false</detail><detail sys-name="FIELD-SUPPORT-SYSTEM">false</detail><detail sys-name="CHANGE-ADMIN-PASSWORD">false</detail><detail sys-name="RETENTION-CALLING">false</detail><detail sys-name="OSA-SETTINGS">false</detail></details></sub-feature><sub-feature sys-name="WEB-SETTINGS" allow="false"><details><detail sys-name="DISPLAY-DEVICE">false</detail><detail sys-name="POWER-RESET">false</detail><detail sys-name="MACHINE-ID">false</detail><detail sys-name="APPLICATION-SETTINGS">false</detail><detail sys-name="REGISTER-PRE-SET-TEXT">false</detail><detail sys-name="EMAIL-ALERT">false</detail><detail sys-name="JOB-LOG">false</detail><detail sys-name="PORT-SETTINGS">false</detail><detail sys-name="STORAGE-BACKUP">false</detail><detail sys-name="CUSTOM-LINKS">false</detail><detail sys-name="OPERATION-MANUAL-DOWNLOAD">false</detail></details></sub-feature></mfp-feature></xmldocacl><xmldoclimits><LIMITS_TYPE sys-name="COPY"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="SINGLE-COLOR">0</limit><limit sys-name="DUAL-COLOR">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE><LIMITS_TYPE sys-name="PRINT"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="SINGLE-COLOR">0</limit><limit sys-name="DUAL-COLOR">0</limit><limit sys-name="TRIPLE-COLOR">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE><LIMITS_TYPE sys-name="LIST-PRINT"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE><LIMITS_TYPE sys-name="SCANNER"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE><LIMITS_TYPE sys-name="DOC-FILING-PRINT"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="SINGLE-COLOR">0</limit><limit sys-name="DUAL-COLOR">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE><LIMITS_TYPE sys-name="SCAN-TO-HDD"><property xmlns="urn:schemas-sc-jp:mfp:osa-1-1" sys-name="color-mode"><limit sys-name="MONOCHROME">0</limit><limit sys-name="DUAL-COLOR">0</limit><limit sys-name="FULL-COLOR">0</limit></property></LIMITS_TYPE></xmldoclimits><Generic>1.0.0.23</Generic><mfpwebservices><MFP_WEBSERVICE_TYPE name="MFPCoreWS" port="443">/MfpWebServices/MFPCoreWS.asmx</MFP_WEBSERVICE_TYPE></mfpwebservices><Credentials><account-id xmlns="urn:schemas-sc-jp:mfp:osa-1-1">ijoM</account-id><metadata xmlns="urn:schemas-sc-jp:mfp:osa-1-1"><password>wU5Zc1T</password></metadata></Credentials></DeviceSession>', CAST(0x0000A79B00A604FC AS DateTime), CAST(0x0000A7EC011BFD16 AS DateTime))

/****** Object:  Table [dbo].[DBThirdAuthConfig]    Script Date: 11/16/2017 08:47:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DBThirdAuthConfig](
	[DBConnectStr] [nvarchar](500) NULL,
	[DBSearchSql] [nvarchar](500) NULL,
	[DBWhereSql] [nvarchar](500) NULL,
	[MTGrpVal1] [nvarchar](50) NULL,
	[MTGrpVal2] [nvarchar](50) NULL,
	[GroupID1] [int] NULL,
	[GroupID2] [int] NULL,
	[AuthDBFlg] [int] NULL
) ON [PRIMARY]
GO
INSERT [dbo].[DBThirdAuthConfig] ([DBConnectStr], [DBSearchSql], [DBWhereSql], [MTGrpVal1], [MTGrpVal2], [GroupID1], [GroupID2], [AuthDBFlg]) VALUES (N'data source=202.120.84.198;uid=sa;pwd=chen@123;database=datalook;pooling=true;min pool size=5;max pool size=512;Connect Timeout=500;', N'select 
USERNAME as LoginName,
USERNAME as UserName,
CARDID as ICCardID,
''666666'' as Password,
''0'' as PinCode,
'' '' as Email,
PID as GroupID,
''0'' as RestrictionID,
MessageType as UserType
FROM User_Infor_Message
WHERE CARDID = @CARDID
ORDER BY IDNumber Desc', N'0,2,3,6', N'2', N'1,3,4,5', 0, 0, 0)

/****** Object:  Table [dbo].[DBThirdAuthSetting]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DBThirdAuthSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DBThirdAuthSetting](
	[DBAuthServerIP] [nvarchar](50) NULL,
	[DBAuthServerPort] [nchar](10) NULL,
	[DBUserName] [nchar](50) NULL,
	[DBPassword] [nchar](50) NULL,
	[DBAuthDBNM] [nchar](50) NULL,
	[DBTableNM] [nchar](50) NULL,
	[MTFullName] [nchar](50) NULL,
	[MTLoginName] [nchar](50) NULL,
	[MTPwd] [nchar](50) NULL,
	[MTIDCard] [nchar](50) NULL,
	[MTGroup] [nchar](50) NULL,
	[MTEmail] [nvarchar](50) NULL,
	[MTPINCode] [nchar](50) NULL,
	[IDCordRule] [int] NULL,
	[IDCardDataLen] [nvarchar](20) NULL,
	[IDCardADDPos] [nchar](10) NULL,
	[IDCardADDCHR] [nchar](10) NULL,
	[UserInfoJudgeRule] [int] NULL,
	[UserJudgeFeild] [nvarchar](20) NULL,
	[UserJudgeFeildVal] [nchar](50) NULL,
	[GroupID1] [int] NULL,
	[GroupID2] [int] NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[DBThirdAuthSetting] ([DBAuthServerIP], [DBAuthServerPort], [DBUserName], [DBPassword], [DBAuthDBNM], [DBTableNM], [MTFullName], [MTLoginName], [MTPwd], [MTIDCard], [MTGroup], [MTEmail], [MTPINCode], [IDCordRule], [IDCardDataLen], [IDCardADDPos], [IDCardADDCHR], [UserInfoJudgeRule], [UserJudgeFeild], [UserJudgeFeildVal], [GroupID1], [GroupID2]) VALUES (N'202.120.87.193\SQLEXPRESS', N'          ', N'sa                                                ', N'!qaz2wsx                                          ', N'datalook                                          ', N'User_Infor_Message                                ', N'USERNAME                                          ', N'IDSERIAL                                          ', N'                                                  ', N'CARDID                                            ', N'PID                                               ', N'', N'                                                  ', 0, N'10', N'1         ', N'A         ', 1, N'MessageType', N'0,2,3,6                                           ', 1, 2)
/****** Object:  Table [dbo].[CopyConfig]    Script Date: 10/31/2017 09:25:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyConfig]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CopyConfig](
	[CopyFileLocation] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[CopyConfig] ([CopyFileLocation]) VALUES (N'C:\SimpleEACopy\')
