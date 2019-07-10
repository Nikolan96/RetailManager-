CREATE PROCEDURE [dbo].[spGetBillsByShopId]

	@ShopID int

AS
begin
	set nocount on;
	select Bill.ID, Bill.CreatedDate, Bill.ShopId, Bill.Total, Bill.Paid, Bill.Change, [User].FirstName, [User].LastName
	from [dbo].[Bill] inner join [dbo].[User] on Bill.UserId = [User].ID
	where Bill.ShopID = @ShopID
	order by Bill.CreatedDate desc;
end
