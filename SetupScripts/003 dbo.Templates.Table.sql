USE [EasyDb]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsStandardTemplate] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LanguageId] [int] NOT NULL
) ON [PRIMARY]

GO
