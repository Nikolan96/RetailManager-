CREATE PROCEDURE [dbo].[spGetProductByID]
	@ID nvarchar(128)
AS
begin
	set nocount on;
	select ProductName, Category, [Description], PurchasePrice, RetailPrice, Tax, QuantityInStock, Category
	from [dbo].[Product]
	where ID = @ID;
end