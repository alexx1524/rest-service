CREATE VIEW [dbo].[TestView] AS 
WITH objCTE AS 
(
	SELECT O.Id, O.Name, O.ParentId, O.Type, 0 AS distance 
	FROM [dbo].objects AS O
	WHERE O.Id = 1

	UNION ALL

	SELECT O.Id, O.Name, O.ParentId, O.Type, C.distance + 1
	FROM [dbo].objects AS O INNER JOIN objCTE AS C ON C.Id = O.ParentId
)

SELECT distinct Cte.Id FROM objCTE AS Cte INNER JOIN [dbo].[attributes] AS A ON A.Oid = Cte.Id
WHERE (Cte.Type = N'1' AND A.Name = N'Масса' AND A.FValue > 10)
INTERSECT
SELECT distinct Cte.Id FROM objCTE AS Cte INNER JOIN [dbo].[attributes] AS A ON A.Oid = Cte.Id
WHERE (Cte.Type = N'1' AND A.Name = N'Дата установки' AND A.DTValue < GETDATE())	

