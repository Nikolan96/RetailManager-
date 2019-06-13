CREATE PROCEDURE [dbo].[spGetBills]
AS
begin
	set nocount on;
	select Id,CreatedDate, ShopId, Total, Paid, Change, UserId
	from [dbo].[Bill];
end
