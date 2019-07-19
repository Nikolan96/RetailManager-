CREATE PROCEDURE [dbo].[spDeleteOrder]
	@ID NVARCHAR(128)
AS
begin
	delete [Order]
	where ID = @ID;
end