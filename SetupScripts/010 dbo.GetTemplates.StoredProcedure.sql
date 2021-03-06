USE [EasyDb]
GO
/****** Object:  StoredProcedure [dbo].[GetTemplates]    Script Date: 17/05/2017 13:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTemplates] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		dbo.Templates.Name, dbo.Templates.LanguageId, dbo.Templates.IsStandardTemplate, dbo.Templates.Id, 
		dbo.SqlTemplate.AddAnsiNulls, dbo.SqlTemplate.IncludeBeginEnd, 
		dbo.SqlTemplate.AddQuotedIdentifier, dbo.SqlTemplate.IncludeIfExistsDrop, 
		dbo.SqlTemplate.IncludeDrop, dbo.SqlTemplate.PrefixNameWith, dbo.SqlTemplate.PrefixNameForBulkSaveWith, 
		dbo.SqlTemplate.ObjectTypeId, dbo.SqlTemplate.StatementTypeId, dbo.SqlTemplate.UseParametersToGenerateData,
		dbo.SqlTemplate.Id AS SqlTemplateId, dbo.CSharpTemplate.ClassNameSuffix, 
		dbo.CSharpTemplate.[Namespace], dbo.CSharpTemplate.IncludeUsings, 
		dbo.CSharpTemplate.IncludeProperties, dbo.CSharpTemplate.IncludeEmptyConstructor, 
		dbo.CSharpTemplate.IncludeOtherConstructor, dbo.CSharpTemplate.Id AS CSharpTemplateId
	FROM   
		dbo.Templates LEFT OUTER JOIN
        dbo.CSharpTemplate ON dbo.Templates.Id = dbo.CSharpTemplate.TemplateId LEFT OUTER JOIN
        dbo.SqlTemplate ON dbo.Templates.Id = dbo.SqlTemplate.TemplateId
END


GO
