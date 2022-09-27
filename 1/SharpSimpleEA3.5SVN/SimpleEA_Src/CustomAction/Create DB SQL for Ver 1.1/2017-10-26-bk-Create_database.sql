/*****************************************************************************/
/* System           : SIMPLE EA SYSTEM DB CREATE                             */
/* Name             : DB CREATE SCRIPT                                       */
/* Description      :                                                        */
/* Author & Date    : SES  2010/07/08                                        */
/* Update           :                                                        */
/* Notes            :                                                        */
/*****************************************************************************/

/****************************************/
/*           CREATE DTATBASE            */
/****************************************/

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

/****************************************/
/*            CREATE TABLE              */
/*             AND Add Defulat Record   */
/****************************************/
USE [SimpleEA]
GO
/****** Object:  Table [dbo].[UserPayDetail]    Script Date: 07/09/2014 15:16:48 ******/
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
	[CreateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserPayDetail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 07/09/2014 15:16:48 ******/
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
	[Password] [nvarchar](10) NOT NULL,
	[ICCardID] [nvarchar](20) NULL,
	[PinCode] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[GroupID] [int] NOT NULL,
	[RestrictionID] [int] NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[LoginName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[UserInfo] ([ID], [UserName], [LoginName], [Password], [ICCardID], [PinCode], [Email], [GroupID], [RestrictionID], [CreateTime], [UpdateTime]) VALUES (0, N'管理员', N'admin', N'admin', N'admin', N'1', N'', -1, 0, CAST(0x002D247F018B8185 AS DateTime), CAST(0x002D247F018B8185 AS DateTime))
/****** Object:  Table [dbo].[SettinServerIP]    Script Date: 07/09/2014 15:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettinServerIP]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettinServerIP](
	[IPAddress] [nvarchar](15) NULL,
	[TongxinType] [nvarchar](1) NULL,
	[TongxinAddr] [nvarchar](100) NULL,
	[LoginType] [nvarchar](1) NULL,
        [ColLimit] [int]  NULL,
	[PaperLimit] [int]  NULL,
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


/****** Object:  Default [DF_SettinServerIP_TongxinType]    Script Date: 05/07/2015 19:38:16 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SettinServerIP_TongxinType]') AND parent_object_id = OBJECT_ID(N'[dbo].[SettinServerIP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_TongxinType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] ADD  CONSTRAINT [DF_SettinServerIP_TongxinType]  DEFAULT ((0)) FOR [TongxinType]
END

BEGIN
INSERT [dbo].[SettinServerIP] ([ColLimit], [PaperLimit] ) VALUES (0, 0)
END

End
GO


/****** Object:  Default [DF_SettinServerIP_TongxinAddr]    Script Date: 05/07/2015 19:38:16 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SettinServerIP_TongxinAddr]') AND parent_object_id = OBJECT_ID(N'[dbo].[SettinServerIP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_TongxinAddr]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] ADD  CONSTRAINT [DF_SettinServerIP_TongxinAddr]  DEFAULT ('') FOR [TongxinAddr]
END


End
GO
/****** Object:  Default [DF_SettinServerIP_LoginType]    Script Date: 05/07/2015 19:38:16 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_SettinServerIP_LoginType]') AND parent_object_id = OBJECT_ID(N'[dbo].[SettinServerIP]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_SettinServerIP_LoginType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[SettinServerIP] ADD  CONSTRAINT [DF_SettinServerIP_LoginType]  DEFAULT ((0)) FOR [LoginType]
END


End
GO

/****** Object:  Table [dbo].[SettingManagement]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[SettingDisp]    Script Date: 07/09/2014 15:16:48 ******/
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
	[Dis_A3_A4] [nchar](10) NULL
) ON [PRIMARY]
END
GO
INSERT [dbo].[SettingDisp] ([Dis_U_UserName], [Dis_U_LoginName], [Dis_U_GroupName], [Dis_U_CardID], [Dis_U_Restrict], [Dis_G_GroupName], [Dis_G_Number], [Dis_G_Restrict], [Dis_R_Restrict], [Dis_R_Copy], [Dis_R_Print], [Dis_R_Scan], [Dis_R_Fax], [Dis_Job_Total], [Dis_Job_CopyTotal], [Dis_Job_PrintTotal], [Dis_Job_ScanTotal], [Dis_Job_FaxTotal], [Dis_Result_Copy], [Dis_Result_Print], [Dis_Result_Scan], [Dis_Result_Fax], [Dis_Result_Other], [Dis_Log_MaxCount], [Dis_Avai_Borrow], [Dis_Count_mode], [Dis_A3_A4]) VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, N'0         ')
/****** Object:  Table [dbo].[RestrictionInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[RestrictionInfo]    Script Date: 07/09/2014 15:16:48 ******/
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
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_RestrictionInfo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[RestrictionInfo] ([ID], [RestrictionName], [AllQuota], [ColorQuota], [OverLimit], [CreateTime], [UpdateTime]) VALUES (-1, N' 继承用户组', CAST(0.00 AS Numeric(7, 2)), CAST(0.00 AS Numeric(7, 2)), CAST(0.00 AS Numeric(7, 2)), CAST(0x0000A32500ABD80B AS DateTime), CAST(0x0000A32500ABD80B AS DateTime))
INSERT [dbo].[RestrictionInfo] ([ID], [RestrictionName], [AllQuota], [ColorQuota], [OverLimit], [CreateTime], [UpdateTime]) VALUES (0, N' 通用配额', CAST(100.00 AS Numeric(7, 2)), CAST(50.00 AS Numeric(7, 2)), CAST(10.00 AS Numeric(7, 2)), CAST(0x002D247F018B8185 AS DateTime), CAST(0x0000A33000A921F0 AS DateTime))
/****** Object:  Table [dbo].[PriceMaster]    Script Date: 07/09/2014 15:16:48 ******/
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
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_PriceMaster] PRIMARY KEY CLUSTERED 
(
	[PriceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

INSERT [dbo].[PriceMaster] ([PriceID], [PriceNM], [PriceCalMode], [CreateTime], [UpdateTime]) VALUES (0, N' 默认价格', 0, CAST(0x0000A31001371232 AS DateTime), CAST(0x0000A33B00B80883 AS DateTime))

/****** Object:  Table [dbo].[PriceDetail]    Script Date: 07/09/2014 15:16:48 ******/
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
	[CreateTime] [datetime] NOT NULL,
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

/****** Object:  Table [dbo].[OSAErrorInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[MFPUserRes]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[MFPPrintTask]    Script Date: 07/09/2014 15:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MFPPrintTask]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MFPPrintTask](
	[MFPPrintTaskID] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](30) NOT NULL,
	[DiskFileName] [nchar](16) NOT NULL,
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
/****** Object:  Table [dbo].[MFPInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
	[Label] [int] NULL,
 CONSTRAINT [PK_MFPInformation] PRIMARY KEY CLUSTERED 
(
	[SerialNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[LogInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[JobTypeInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[JobInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[GroupInfo]    Script Date: 07/09/2014 15:16:48 ******/
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
INSERT [dbo].[GroupInfo] ([ID], [GroupName], [RestrictionID], [CreateTime], [UpdateTime]) VALUES (-1, N'admin', 0, CAST(0x002D247F018B8185 AS DateTime), CAST(0x002D247F018B8185 AS DateTime))
INSERT [dbo].[GroupInfo] ([ID], [GroupName], [RestrictionID], [CreateTime], [UpdateTime]) VALUES (0, N'无所属', 0, CAST(0x002D247F018B8185 AS DateTime), CAST(0x002D247F018B8185 AS DateTime))
/****** Object:  Table [dbo].[FunctionTypeInformation]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Table [dbo].[DeviceSession]    Script Date: 07/09/2014 15:16:48 ******/
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
/****** Object:  Default [DF_MFPInformation_Label]    Script Date: 07/09/2014 15:16:48 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_MFPInformation_Label]') AND parent_object_id = OBJECT_ID(N'[dbo].[MFPInformation]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_MFPInformation_Label]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MFPInformation] ADD  CONSTRAINT [DF_MFPInformation_Label]  DEFAULT ((0)) FOR [Label]
END


End
GO
/****** Object:  Default [DF_PriceDetail_CreateTime]    Script Date: 07/09/2014 15:16:48 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_PriceDetail_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[PriceDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PriceDetail_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PriceDetail] ADD  CONSTRAINT [DF_PriceDetail_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_PriceMaster_CreateTime]    Script Date: 07/09/2014 15:16:48 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_PriceMaster_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[PriceMaster]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PriceMaster_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PriceMaster] ADD  CONSTRAINT [DF_PriceMaster_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_RestrictionInfo_CreateTime]    Script Date: 07/09/2014 15:16:48 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_RestrictionInfo_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[RestrictionInfo]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_RestrictionInfo_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RestrictionInfo] ADD  CONSTRAINT [DF_RestrictionInfo_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
/****** Object:  Default [DF_UserPayDetail_CreateTime]    Script Date: 07/09/2014 15:16:48 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_UserPayDetail_CreateTime]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserPayDetail]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserPayDetail_CreateTime]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserPayDetail] ADD  CONSTRAINT [DF_UserPayDetail_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]
END


End
GO
