CREATE PROCEDURE [dbo].[spInsertBill]
	@ShopId int,
	@Total money,
	@Paid money,
	@Change money,
	@UserId nvarchar(128)
AS
begin
    set nocount on;
	INSERT INTO Bill(ShopId, Total, Paid, Change, UserId)
	VALUES(@ShopId,@Total,@Paid,@Change,@UserId);
end