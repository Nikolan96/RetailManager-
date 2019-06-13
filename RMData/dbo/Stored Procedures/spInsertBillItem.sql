CREATE PROCEDURE [dbo].[spInsertBillItem]
	@BillId nvarchar(128),
	@ProductName NVARCHAR(100),
	@Quantity int,
	@RetailPrice money,
	@Category NVARCHAR(50),
	@Description NVARCHAR(MAX)
AS
begin
    set nocount on;
	INSERT INTO BillItem(BillId,ProductName,Quantity,RetailPrice,Category,[Description])
	VALUES(@BillId, @ProductName,@Quantity,@RetailPrice,@Category,@Description)
end