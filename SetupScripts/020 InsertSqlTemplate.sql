USE [EasyDb]
GO

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	1, 
	1, 
	1, 
	1, 
	1, 
	'Create', 
	'CreateTable', 
	2, 
	1, 
	0, 
	0
)

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	1, 
	1, 
	0, 
	1, 
	1, 
	'Insert', 
	'', 
	1, 
	2, 
	2, 
	0
)

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	1, 
	1, 
	1, 
	1, 
	1, 
	'Get', 
	'', 
	3, 
	2, 
	1, 
	0
)

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	1, 
	1, 
	1, 
	1, 
	1, 
	'Delete', 
	'', 
	4, 
	2, 
	4, 
	0
)

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	1, 
	1, 
	1, 
	1, 
	1, 
	'Insert', 
	'', 
	7, 
	2, 
	3, 
	0
)

INSERT INTO [SqlTemplate]
(
	[AddAnsiNulls],
	[AddQuotedIdentifier],
	[IncludeBeginEnd],
	[IncludeIfExistsDrop],
	[IncludeDrop],
	[PrefixNameWith],
	[PrefixNameForBulkSaveWith],
	[TemplateId],
	[ObjectTypeId],
	[StatementTypeId],
	[UseParametersToGenerateData]
)
VALUES
(
	0, 
	0, 
	0, 
	0, 
	0, 
	'Insert', 
	'', 
	9, 
	2, 
	2, 
	1
)

GO
