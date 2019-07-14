CREATE PROCEDURE [dbo].[spDeleteProduct]
	@ID nvarchar(150)
AS
begin
	set nocount on;
	delete from Product where Product.ID = @ID;
end

