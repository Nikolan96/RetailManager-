CREATE PROCEDURE [dbo].[spGetBills]
AS
begin
	set nocount on;
	select Id,CreateDate, ShopId, Total, Paid, Change, UserId
	from [dbo].[Bill];
end
