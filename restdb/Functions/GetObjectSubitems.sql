CREATE FUNCTION [dbo].[GetObjectSubitems]
(
	@oid bigint
)
RETURNS @returntable TABLE
(
	id bigint,
	[type] nvarchar(MAX),
	[level] smallint
)
AS
BEGIN
	DECLARE @parent_hid HIERARCHYID;
 
	SELECT @parent_hid = Hid 
	FROM objects 
	WHERE Oid = @oid;	

	INSERT @returntable
	SELECT Oid, [Type], [level]
	FROM [dbo].[objects] 
	WHERE Hid.IsDescendantOf(@parent_hid) = 1
	ORDER BY [level];

	RETURN
END
