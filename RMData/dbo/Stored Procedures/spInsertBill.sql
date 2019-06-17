CREATE PROCEDURE [dbo].[spInsertBill]
	@Shopid int,
	@Total money,
	@Paid money,
	@Change money,
	@UserId nvarchar(128),
	@CreatedDate DATETIME2,
	@ID nvarchar(128)
AS
begin
    set nocount on;
	INSERT INTO Bill(ShopId, Total, Paid, Change, UserId, CreatedDate,ID)
	VALUES(@Shopid,@Total,@Paid,@Change,@UserId, @CreatedDate,@ID);
end
