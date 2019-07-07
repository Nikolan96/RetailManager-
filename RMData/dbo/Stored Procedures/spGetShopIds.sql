CREATE PROCEDURE [dbo].[spGetShopIds]

AS
begin
set nocount on;
	SELECT ID from Shop;
end
