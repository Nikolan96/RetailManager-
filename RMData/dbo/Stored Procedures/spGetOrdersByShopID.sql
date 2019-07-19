CREATE PROCEDURE [dbo].[spGetOrdersByShopID]
	@ShopID int
AS
begin
	set nocount on;
	select *
	from [dbo].[Order]
	where ShopID = @ShopID;
end