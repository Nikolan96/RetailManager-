CREATE PROCEDURE [dbo].[spGetAllProductNames]
AS
begin
	set nocount on;
	select ProductName
	from dbo.Product
	order by ProductName;
end

