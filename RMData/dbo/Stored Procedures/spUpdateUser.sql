CREATE PROCEDURE [dbo].[spUpdateUser]

	@ID int,
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@EmailAddress nvarchar(100),
	@Password nvarchar(100),
	@Role nvarchar(100),
	@ShopId int

as
begin
	set nocount on;
	UPDATE [User]
	Set 
	FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, @Password = @Password, [Role] = @Role, ShopID = @ShopId
	where ID = @ID
end
