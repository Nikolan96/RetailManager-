CREATE PROCEDURE [dbo].[spDeleteProduct]
	@id nvarchar(150)
AS
begin
	set nocount on;
	delete from Product where Product.Id = @id;
end

