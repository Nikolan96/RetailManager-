CREATE PROCEDURE [dbo].[spGetUserByEmail]
	@EmailAddress nvarchar(128)
AS
begin
	set nocount on;
	select ID,FirstName, LastName, EmailAddress, [Password], [Role], ShopID, CreatedDate
	from [dbo].[User]
	where EmailAddress = @EmailAddress;
end
