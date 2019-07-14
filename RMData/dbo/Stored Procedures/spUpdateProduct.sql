CREATE PROCEDURE [dbo].[spUpdateProduct]

	@ID nvarchar(150),
	@ProductName nvarchar(100),
	@Description nvarchar(200),
	@PurchasePrice money,
	@RetailPrice money,
	@Tax int,
	@Category nvarchar(100),
	@QuantityInStock int

as
begin
	set nocount on;
	UPDATE Product
	Set 
	ProductName = @ProductName, Category = @Category, Description = @Description, PurchasePrice = @PurchasePrice, RetailPrice = @RetailPrice, Tax = @Tax,
	Margin = @RetailPrice-@purchasePrice, QuantityInStock = @QuantityInStock
	where ID = @ID
end
