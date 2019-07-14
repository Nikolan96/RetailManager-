CREATE PROCEDURE [dbo].[spGetProductsByShopID]
	@ShopID nvarchar(128)
AS
begin
	set nocount on;
	select ID,ProductName, Category, [Description], PurchasePrice, RetailPrice, Tax, QuantityInStock, Category
	from [dbo].[Product]
	where ShopID = @ShopID;
end