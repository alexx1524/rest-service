CREATE TABLE [dbo].[dtvalues]
(
	[Id] BIGINT IDENTITY NOT NULL PRIMARY KEY, 
    [AttrId] BIGINT NOT NULL,
	[Value] DATETIME2(7) NOT NULL, 
    CONSTRAINT [FK_dtvalues_attrs] FOREIGN KEY ([AttrId]) REFERENCES [attributes]([Id])
)

GO

CREATE INDEX [dtvalues_value] ON [dbo].[dtvalues] ([Value])
