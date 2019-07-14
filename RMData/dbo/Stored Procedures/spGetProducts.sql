CREATE PROCEDURE [dbo].spProduct_GetAll

AS
begin
	set nocount on;
	select ID, Tax, ProductName, [Description], RetailPrice, Margin, QuantityInStock, PurchasePrice, Category
	from dbo.Product
	order by ProductName;
end
