USE [SimpleEA]
GO

/****** Object:  Table [dbo].[PrintCopy]    Script Date: 07/28/2017 15:03:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PrintCopy](
	[ID] [int] NOT NULL,
	[Time] [datetime] NULL,
	[UserID] [int] NULL,
	[CopyType] [int] NULL,
	[CopyFile] [nvarchar](50) NULL,
	[Finished] [char](10) NULL,
	[CopyTimes] [nchar](10) NULL,
 CONSTRAINT [PK_PrintCopy] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

