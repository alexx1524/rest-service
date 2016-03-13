CREATE TABLE [dbo].[objects] (
    [Oid]   BIGINT IDENTITY(1,1) NOT NULL,
    [Hid]   [sys].[hierarchyid]  NOT NULL,
    [Type]  NVARCHAR (MAX)       NOT NULL,
    [level] AS                  ([Hid].[GetLevel]()) PERSISTED,
    PRIMARY KEY CLUSTERED ([Oid] ASC)
);





GO

