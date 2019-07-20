CREATE PROCEDURE [dbo].[spInsertOrder]

	@ShopID int,
	@ID nvarchar(128),
	@CreatedDate DATETIME2

AS
begin
    set nocount on;
	INSERT INTO [Order](ID, ShopID,IsApproved, CreatedDate)
	VALUES(@ID, @ShopID, 0, @CreatedDate);
end
