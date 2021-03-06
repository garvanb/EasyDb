USE [EasyDb]
GO
/****** Object:  StoredProcedure [dbo].[EditTemplates]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditTemplates]
	@Id int,
	@IsStandardTemplate bit,
	@LanguageId int,
	@Name nvarchar(50)

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE
		Templates
	SET
		IsStandardTemplate = @IsStandardTemplate,
		LanguageId = @LanguageId,
		Name = @Name
	WHERE
		Id = @Id
END


GO
