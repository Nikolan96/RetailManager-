CREATE PROCEDURE [dbo].[spApproveOrder]
	
	@ID nvarchar(128)
AS
begin
	set nocount on;
	update [Order]
	set IsApproved = 1
	where ID = @ID;
end