CREATE PROCEDURE [dbo].[AddObjectWithAttributes]
(
	@parentid  BIGINT, 
	@type_name NVARCHAR(max),
	@attrs [dbo].[Attrs] READONLY
) 

AS
   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
   
   DECLARE @oid BIGINT
   
   BEGIN TRANSACTION
      EXEC [dbo].AddObject @parentid = @parentid, @type = @type_name, @newid = @oid OUTPUT;

	  INSERT INTO [dbo].[attrs](Oid, [Type], Name, FValue, SValue, DTValue)
	  SELECT @oid, a.[type], a.name, a.fvalue, a.svalue, a.dtvalue 
	  FROM @attrs AS a

   COMMIT

RETURN @oid
