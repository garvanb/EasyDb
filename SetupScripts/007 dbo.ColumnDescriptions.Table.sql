USE [EasyDb]
GO
/****** Object:  Table [dbo].[ColumnDescriptions]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ColumnDescriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[DataType] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO
