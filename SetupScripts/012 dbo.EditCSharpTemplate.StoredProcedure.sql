USE [EasyDb]
GO
/****** Object:  StoredProcedure [dbo].[EditCSharpTemplate]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditCSharpTemplate]
	@Id int,
	@ClassNameSuffix nvarchar(50),
	@Namespace nvarchar(50),
	@IncludeUsings bit,
	@IncludeProperties bit,
	@IncludeEmptyConstructor bit,
	@IncludeOtherConstructor bit

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE
		[CSharpTemplate]
	SET
		[ClassNameSuffix] = @ClassNameSuffix,
		[Namespace] = @Namespace,
		[IncludeUsings] = @IncludeUsings,
		[IncludeProperties] = @IncludeProperties,
		[IncludeEmptyConstructor] = @IncludeEmptyConstructor,
		[IncludeOtherConstructor] = @IncludeOtherConstructor
	WHERE
		[Id] = @Id
END


GO
