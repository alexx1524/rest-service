CREATE VIEW [dbo].[TestView] AS 
WITH objCTE AS 
(
	SELECT O.Oid, O.Type, O.Hid, 0 AS distance 
	FROM [dbo].objects AS O
	WHERE O.Oid = 1
	/*
	UNION ALL

	SELECT O.Id, O.ParentId, O.Type, C.distance + 1
	FROM [dbo].objects AS O INNER JOIN objCTE AS C ON C.Id = O.ParentId*/
)

SELECT distinct Cte.Oid 
FROM objCTE AS Cte 
	INNER JOIN [dbo].[attributes] AS A ON A.Oid = Cte.Oid
	INNER JOIN [dbo].[fvalues] AS FV ON A.Id = FV.AttrId
WHERE (Cte.Type = N'1' AND A.Name = N'Масса' AND FV.Value > 10)

INTERSECT

SELECT distinct Cte.Oid
FROM objCTE AS Cte 
	INNER JOIN [dbo].[attributes] AS A ON A.Oid = Cte.Oid
	INNER JOIN [dbo].[dtvalues] AS DTV ON A.Id = DTV.Id
WHERE (Cte.Type = N'1' AND A.Name = N'Дата установки' AND DTV.Value < GETDATE())	
