USE [EasyDb]
GO
/****** Object:  StoredProcedure [dbo].[EditSqlTemplate]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditSqlTemplate]
	@AddAnsiNulls bit,
	@AddQuotedIdentifier bit,
	@Id int,
	@IncludeBeginEnd bit,
	@IncludeDrop bit,
	@IncludeIfExistsDrop bit,
	@ObjectTypeId int,
	@PrefixNameForBulkSaveWith nvarchar(50),
	@PrefixNameWith nvarchar(50),
	@StatementTypeId int,
	@UseParametersToGenerateData bit

AS
BEGIN

	SET NOCOUNT ON;

	UPDATE
		SqlTemplate
	SET
		AddAnsiNulls = @AddAnsiNulls,
		AddQuotedIdentifier = @AddQuotedIdentifier,
		IncludeBeginEnd = @IncludeBeginEnd,
		IncludeDrop = @IncludeDrop,
		IncludeIfExistsDrop = @IncludeIfExistsDrop,
		ObjectTypeId = @ObjectTypeId,
		PrefixNameForBulkSaveWith = @PrefixNameForBulkSaveWith,
		PrefixNameWith = @PrefixNameWith,
		StatementTypeId = @StatementTypeId,
		UseParametersToGenerateData = @UseParametersToGenerateData
	WHERE
		Id = @Id
END


GO
