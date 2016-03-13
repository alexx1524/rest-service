CREATE FUNCTION [dbo].[GetPumps]
(
	@oid bigint
)
RETURNS @returntable TABLE
(
	oid bigint
)
AS
BEGIN	
	WITH objCTE AS 
	(
		SELECT id as Oid, [type] FROM [dbo].[GetObjectSubitems](@oid)
		ORDER BY [level] ASC
		OFFSET 1 ROWS
	),
	attrs AS 
	(
		SELECT distinct Cte.Oid, Cte.[type] AS OType, A.[Type] AS AType, A.Name, A.FValue, A.DTValue
		FROM objCTE AS Cte 
		INNER JOIN [dbo].[attrs] AS A ON A.Oid = Cte.Oid
	)


	INSERT @returntable
	SELECT distinct A.Oid 
	FROM attrs As A
	WHERE (A.OType = N'Насос' AND A.Name = N'Масса' AND A.FValue > 10)

	INTERSECT

	SELECT distinct A.Oid
	FROM attrs As A
	WHERE (A.OType = N'Насос' AND A.Name = N'Дата установки' AND A.DTValue < GETDATE())

	RETURN
END
