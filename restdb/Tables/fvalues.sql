CREATE TABLE [dbo].[fvalues]
(
	[Id] BIGINT IDENTITY NOT NULL PRIMARY KEY,
	[AttrId] BIGINT NOT NULL,
	[Value] FLOAT NOT NULL, 
    CONSTRAINT [FK_fvalues_attrs] FOREIGN KEY ([AttrId]) REFERENCES [attributes]([Id])
)

GO

CREATE INDEX [fvalues_value] ON [dbo].[fvalues] ([Value])
