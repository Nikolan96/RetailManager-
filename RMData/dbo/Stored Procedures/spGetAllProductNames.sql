CREATE PROCEDURE [dbo].[spGetAllProductNames]

	@ShopID int

AS
begin
	set nocount on;
	select ProductName
	from dbo.Product
	where ShopID = @ShopID
	order by ProductName;
end

