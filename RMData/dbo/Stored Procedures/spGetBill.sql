CREATE PROCEDURE [dbo].[spGetBill]
	@Id NVARCHAR(128)
AS
begin
	set nocount on;
	select Id, CreatedDate, ShopId, Total, Paid, Change, UserId
	from [dbo].[Bill]
	where Id = @Id;
end
