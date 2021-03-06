USE [EasyDb]
GO
/****** Object:  Table [dbo].[CSharpTemplate]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CSharpTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[ClassNameSuffix] [nvarchar](50) NOT NULL,
	[Namespace] [nvarchar](50) NOT NULL,
	[IncludeUsings] [bit] NOT NULL,
	[IncludeProperties] [bit] NOT NULL,
	[IncludeEmptyConstructor] [bit] NOT NULL,
	[IncludeOtherConstructor] [bit] NOT NULL
) ON [PRIMARY]

GO
