CREATE PROCEDURE [dbo].[spUpdateProduct]

	@Id int,
	@ProductName nvarchar(100),
	@Description nvarchar(200),
	@PurchasePrice money,
	@RetailPrice money,
	@Tax int,
	@QuantityInStock int,
	@Category nvarchar(100)

as
begin
	set nocount on;
	UPDATE Product
	Set 
	ProductName = @ProductName, Category = @Category, Description = @Description, PurchasePrice = @PurchasePrice, RetailPrice = @RetailPrice, Tax = @Tax,
	QuantityInStock = @QuantityInStock,  Margin = @RetailPrice-@purchasePrice
	where Id = @Id
end
