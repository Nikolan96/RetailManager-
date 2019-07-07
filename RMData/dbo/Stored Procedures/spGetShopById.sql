CREATE PROCEDURE [dbo].[spGetShopById]
	@Id int
AS
begin 
set nocount on;
	SELECT * from [Shop]
	where ID = @Id;
end
