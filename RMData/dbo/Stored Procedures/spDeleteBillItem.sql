CREATE PROCEDURE [dbo].[spDeleteBillItem]
	@BillId nvarchar(128)
AS
begin
	set nocount on;
	delete from BillItem where BillId = @BillId;
end

