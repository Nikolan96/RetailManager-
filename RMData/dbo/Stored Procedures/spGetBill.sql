CREATE PROCEDURE [dbo].[spGetBill]
	@Id int
AS
begin
	set nocount on;
	select Id,CreateDate, ShopId, Total, Paid, Change, UserId
	from [dbo].[Bill]
	where ID = @ID;
end
