CREATE PROCEDURE [dbo].[spInsertOrderItem]
	@OrderID nvarchar(128),
	@ProductName NVARCHAR(100),
	@Quantity int,
	@ProductID nvarchar(150)

AS
begin
    set nocount on;
	INSERT INTO OrderItem(OrderID, ProductName, Quantity, ProductID)
	VALUES(@OrderID, @ProductName, @Quantity, @ProductID)
end