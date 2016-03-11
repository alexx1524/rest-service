CREATE TABLE [dbo].[svalues]
(
	[Id] BIGINT IDENTITY NOT NULL PRIMARY KEY,
	[AttrId] BIGINT NOT NULL,
	Value NVARCHAR(MAX) NOT NULL, 
    [CSValue] AS CHECKSUM(Value), 
    CONSTRAINT [FK_svalues_attrs] FOREIGN KEY ([AttrId]) REFERENCES [attributes]([Id])
)

GO

CREATE INDEX [svalues_csvalue] ON [dbo].[svalues] ([CSValue])
