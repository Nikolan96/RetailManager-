CREATE PROCEDURE [dbo].[spInsertBill]
	@ShopID int,
	@Total money,
	@Paid money,
	@Change money,
	@UserId nvarchar(128),
	@CreatedDate DATETIME2,
	@ID nvarchar(128)

AS
begin
    set nocount on;
	INSERT INTO Bill(ShopID, Total, Paid, Change, UserId, CreatedDate,ID)
	VALUES(@ShopID,@Total,@Paid,@Change,@UserId, @CreatedDate,@ID);
end
