CREATE PROCEDURE [dbo].[spProductLookup]
	@iD nvarchar(128)
AS
begin
	set nocount on;
	select ProductName, Category, [Description], PurchasePrice, RetailPrice, Tax
	from [dbo].[Product]
	where ID = @ID;
end