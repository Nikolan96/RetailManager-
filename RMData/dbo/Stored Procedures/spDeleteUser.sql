CREATE PROCEDURE [dbo].[spDeleteUser]
	@ID int
AS
begin
	Delete from [User] where ID = @ID;
end
