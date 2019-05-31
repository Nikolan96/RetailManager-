CREATE PROCEDURE [dbo].[spInsertProduct]

	@ProductName nvarchar(100),
	@Description nvarchar(200),
	@PurchasePrice money,
	@RetailPrice money,
	@Tax int,
	@Quantity int,
	@Category nvarchar(100)

as
begin
	set nocount on;
	INSERT INTO Product(ProductName, [Description], PurchasePrice, RetailPrice, Tax, Margin, QuantityInStock, Category)
	values (@ProductName,@Description,@PurchasePrice,@RetailPrice,@Tax, @RetailPrice-@purchasePrice,@Quantity,@Category);
end
