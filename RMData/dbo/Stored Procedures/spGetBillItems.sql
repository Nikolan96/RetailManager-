CREATE PROCEDURE [dbo].[spGetBillItems]
	@BillId int
AS
begin
	set nocount on;
	SELECT * FROM BillItem
	WHERE BillId = @BillId;
end
