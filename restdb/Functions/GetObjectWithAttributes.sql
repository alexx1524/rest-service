CREATE FUNCTION [dbo].[GetObjectWithAttributes]
(
	@oid bigint
)
RETURNS @returntable TABLE
(
	oid     bigint,
	[type]  nvarchar(MAX),
	[atype] smallint NOT NULL,
	[name]  nvarchar(MAX),
	svalue  nvarchar(MAX) NULL,
	fvalue  float NULL,
	dtvalue datetime2 NULL
)

AS
BEGIN
	INSERT @returntable

	SELECT o.Oid, o.[Type], a.[Type] AS AType, a.Name, a.SValue, a.FValue, a.DTValue
	FROM [dbo].[objects] AS o
		INNER JOIN [dbo].[attrs] AS a On a.Oid = o.Oid
	WHERE o.Oid = @oid
	RETURN
END
