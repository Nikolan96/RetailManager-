CREATE PROCEDURE [dbo].[spInsertOrderItem]
	@OrderID nvarchar(128),
	@ProductName nvarchar,
	@Quantity int

AS
begin
    set nocount on;
	INSERT INTO OrderItem(OrderID, ProductName, Quantity)
	VALUES(@OrderID, @ProductName, @Quantity)
end