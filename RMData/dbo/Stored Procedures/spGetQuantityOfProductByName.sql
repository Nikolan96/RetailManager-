CREATE PROCEDURE [dbo].[spGetQuantityOfProductByName]
	@ProductName nvarchar

AS
begin
	set nocount on;
	select QuantityInStock
	from dbo.Product
	where ProductName = @ProductName;
end
