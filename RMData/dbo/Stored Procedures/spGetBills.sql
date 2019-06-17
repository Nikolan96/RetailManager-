CREATE PROCEDURE [dbo].[spGetBills]
AS
begin
	set nocount on;
	select Bill.Id, Bill.CreatedDate, Bill.ShopId, Bill.Total, Bill.Paid, Bill.Change, [User].FirstName, [User].LastName
	from [dbo].[Bill] inner join [dbo].[User] on Bill.UserId = [User].ID
	order by Bill.CreatedDate desc;
end
