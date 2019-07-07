CREATE PROCEDURE [dbo].[spDeleteShop]
	@ID int
AS
begin
set nocount on;
	DELETE from Shop where ID = @ID;
end
