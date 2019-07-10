CREATE PROCEDURE [dbo].[spInsertUser]
	@FirstName nvarchar(100),
	@LastName nvarchar(100),
	@Password nvarchar(200),
	@EmailAddress nvarchar(100),
	@ShopId int null,
	@Role nvarchar(100)
AS
begin
set nocount on;
	
	Insert into [User](FirstName, LastName, [Password], EmailAddress, [Role], ShopID)
	values (@FirstName, @LastName, @Password, @EmailAddress, @Role, @ShopId);
end

