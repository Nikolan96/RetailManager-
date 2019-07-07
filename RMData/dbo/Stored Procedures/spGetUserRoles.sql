CREATE PROCEDURE [dbo].[spGetUserRoles]

AS
begin
set nocount on;
	Select [Role] from [User] group by [Role];
end
