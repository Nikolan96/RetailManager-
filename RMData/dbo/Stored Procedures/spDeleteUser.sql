CREATE PROCEDURE [dbo].[spDeleteUser]
	@ID int
AS
begin
	update [User] 
	set IsActive = 0
	where ID = @ID
end
