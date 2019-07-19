CREATE PROCEDURE [dbo].[spUpdateOrderItem]
	@ID int,
	@Quantity int
AS
begin
	set nocount on;
	update OrderItem
	set Quantity = @Quantity
	where ID = @ID;
end