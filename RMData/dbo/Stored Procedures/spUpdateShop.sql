CREATE PROCEDURE [dbo].[spUpdateShop]
    @Id int,
	@Town nvarchar(100),
	@Address nvarchar(100)
AS
begin
set nocount on;
	update Shop 
	Set Town = @Town, [Address] = @Address
	where ID = @Id;
end