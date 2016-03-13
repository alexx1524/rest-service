CREATE TABLE [dbo].[attrs]
(
	[Id]    BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Oid]   BIGINT NOT NULL,
	[Type]  SMALLINT NOT NULL,
	[Name]  NVARCHAR(MAX) NOT NULL,
	FValue  FLOAT NULL,
	SValue  NVARCHAR(MAX) NULL,
	DTValue DATETIME2(7) NULL, 
    CONSTRAINT [FK_attrs_objects] FOREIGN KEY ([Oid]) REFERENCES [dbo].[objects]([Oid])
)

GO
CREATE INDEX [IX_attrs_fvalue] ON [dbo].[attrs] ([FValue])
GO
CREATE INDEX [IX_attrs_dtvalue] ON [dbo].[attrs] ([DTValue])
GO
CREATE INDEX [IX_attrs_Oid] ON [dbo].[attrs] ([Oid])
