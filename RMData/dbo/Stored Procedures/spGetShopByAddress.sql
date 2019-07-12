CREATE PROCEDURE [dbo].[spGetShopByAddress]
	@Address nvarchar(150)
AS
set nocount on;
begin
	SELECT * from Shop
	where [Address] = @Address
end
