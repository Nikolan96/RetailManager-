CREATE PROCEDURE [dbo].[spDeleteBillItem]
	@BillId int
AS
begin
	set nocount on;
	delete from BillItem where BillId = @BillId;
end

