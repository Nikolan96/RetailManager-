CREATE PROCEDURE [dbo].[spInsertProduct]

	@ID nvarchar(150),
	@ProductName nvarchar(100),
	@Description nvarchar(200),
	@PurchasePrice money,
	@RetailPrice money,
	@Tax int,
	@Quantity int,
	@Category nvarchar(100),
	@ShopID int

as
begin
	set nocount on;
	INSERT INTO Product(ID,ProductName, [Description], PurchasePrice, RetailPrice, Tax, Margin, QuantityInStock, Category, ShopID)
	values (@ID,@ProductName,@Description,@PurchasePrice,@RetailPrice,@Tax, @RetailPrice-@purchasePrice,@Quantity,@Category, @ShopID);
end
