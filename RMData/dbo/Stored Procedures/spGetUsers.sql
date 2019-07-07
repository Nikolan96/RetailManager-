CREATE PROCEDURE [dbo].[spGetUsers]

AS
begin
	set nocount on;
	select ID,FirstName, LastName, EmailAddress, [Password], [Role], ShopID, CreatedDate
	from [dbo].[User];
end
