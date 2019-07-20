CREATE PROCEDURE [dbo].[spGetOrderItemByID]
	@ID int
AS
begin
	set nocount on;
	select ID, OrderID, ProductID, ProductName, Quantity
	from [dbo].[OrderItem]
	where ID = @ID;
end