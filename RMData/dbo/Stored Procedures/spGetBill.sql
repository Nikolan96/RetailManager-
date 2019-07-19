CREATE PROCEDURE [dbo].[spGetBill]
	@Id NVARCHAR(128)
AS
begin
	set nocount on;
	select ID, CreatedDate, ShopId, Total, Paid, Change, UserId
	from [dbo].[Bill]
	where ID = @Id;
end
