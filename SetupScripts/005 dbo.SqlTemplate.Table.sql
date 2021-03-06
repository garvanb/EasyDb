USE [EasyDb]
GO
/****** Object:  Table [dbo].[SqlTemplate]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SqlTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddAnsiNulls] [bit] NOT NULL,
	[AddQuotedIdentifier] [bit] NOT NULL,
	[IncludeBeginEnd] [bit] NOT NULL,
	[IncludeIfExistsDrop] [bit] NOT NULL,
	[IncludeDrop] [bit] NOT NULL,
	[PrefixNameWith] [nvarchar](50) NOT NULL,
	[PrefixNameForBulkSaveWith] [nvarchar](50) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[ObjectTypeId] [int] NOT NULL,
	[StatementTypeId] [int] NOT NULL,
	[UseParametersToGenerateData] [bit] NULL
) ON [PRIMARY]

GO
