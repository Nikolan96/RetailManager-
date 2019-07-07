CREATE PROCEDURE [dbo].[spInsertShop]
	@Town nvarchar(100),
	@Address nvarchar(100)
AS
begin 
set nocount on;
	Insert into Shop(Town, [Address])
	values (@Town, @Address);
end
