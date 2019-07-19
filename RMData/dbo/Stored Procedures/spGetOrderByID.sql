CREATE PROCEDURE [dbo].[spGetOrderByID]
	@ID NVARCHAR(128)
AS
begin
	set nocount on;
	select *
	from [dbo].[Order]
	where ID = @ID;
end