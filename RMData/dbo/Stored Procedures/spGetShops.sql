CREATE PROCEDURE [dbo].[spGetShops]

AS
begin 
set nocount on;
	SELECT * from [Shop];
end
