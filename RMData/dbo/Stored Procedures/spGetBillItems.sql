CREATE PROCEDURE [dbo].[spGetBillItems]
	@BillId nvarchar(128)
AS
begin
	set nocount on;
	SELECT Id, ProductName, Category, [Description], Quantity, RetailPrice, ProductID FROM BillItem
	WHERE BillId = @BillId;
end
