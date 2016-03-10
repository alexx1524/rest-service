CREATE TABLE [dbo].[objects]
(
	[Id]       BIGINT NOT NULL PRIMARY KEY,
	[Name]     NVARCHAR(MAX) NOT NULL,
	[ParentId] BIGINT NULL,
	[Type]     NVARCHAR(50) NOT NULL
)
