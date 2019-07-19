CREATE PROCEDURE [dbo].[spDeleteOrderItem]
	@ID int
AS
begin
	set nocount on;
	delete from [OrderItem] where ID = @ID;
end

