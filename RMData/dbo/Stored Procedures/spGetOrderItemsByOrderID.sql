CREATE PROCEDURE [dbo].[spGetOrderItemsByOrderID]
	@OrderID NVARCHAR(128)
AS
begin
	set nocount on;
	select ID, ProductID, ProductName, Quantity
	from [dbo].[OrderItem]
	where OrderID = @OrderID;
end