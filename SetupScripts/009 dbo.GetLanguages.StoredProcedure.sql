USE [EasyDb]
GO
/****** Object:  StoredProcedure [dbo].[GetLanguages]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLanguages]

AS
BEGIN

	SET NOCOUNT ON;

	SELECT
		Id,
		Name
	FROM
		[Languages]
END


GO
