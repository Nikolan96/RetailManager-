CREATE PROCEDURE [dbo].[spGetOrderItemByID]
	@ID int
AS
begin
	set nocount on;
	select ID, ProductName, Quantity
	from [dbo].[OrderItem]
	where ID = @ID;
end