CREATE PROCEDURE [dbo].[spUserLookup]
	@ID nvarchar(128)
AS
begin
	set nocount on;
	select ID,FirstName, LastName, EmailAddress, CreatedDate
	from [dbo].[User]
	where ID = @ID;
end
