CREATE PROC AddObject
(
	@parentid bigint, 
	@type nvarchar(max),
	@newid BIGINT = NULL OUTPUT

) 
AS 
BEGIN
   DECLARE @mObjectNode hierarchyid, 
           @lc hierarchyid

   SELECT @mObjectNode = Hid
   FROM [dbo].[objects] 
   WHERE Oid = @parentid

   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

   BEGIN TRANSACTION
      SELECT @lc = max(Hid) 
      FROM [dbo].[objects]
      WHERE Hid.GetAncestor(1) = @mObjectNode;

      INSERT [dbo].[objects] (Hid, [Type])
	  OUTPUT INSERTED.Oid
      VALUES(@mObjectNode.GetDescendant(@lc, NULL), @type)

	  SET @newid = SCOPE_IDENTITY();
   COMMIT
END ;