CREATE PROCEDURE [dbo].[spInsertOrder]

	@ShopID int,
	@ID nvarchar(128)

AS
begin
    set nocount on;
	INSERT INTO [Order](ID, ShopID,IsApproved)
	VALUES(@ID, @ShopID, 0);
end
