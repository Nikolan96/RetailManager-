CREATE PROCEDURE [dbo].[spGetAllProductNamesAndQuantities]

	@ShopID int

AS
begin
	set nocount on;
	select ProductName, QuantityInStock
	from dbo.Product
	where ShopID = @ShopID
	order by ProductName;
end

