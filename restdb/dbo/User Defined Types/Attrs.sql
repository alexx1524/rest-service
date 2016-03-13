CREATE TYPE [dbo].[Attrs] AS TABLE (
    [type]    SMALLINT       NOT NULL,
    [name]    NVARCHAR (MAX) NOT NULL,
    [svalue]  NVARCHAR (MAX) NULL,
    [fvalue]  FLOAT (53)     NULL,
    [dtvalue] DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([type] ASC));

