CREATE PROCEDURE [dbo].[spDeleteProduct]
	@id int
AS
begin
	set nocount on;
	delete from Product where Product.Id = @id;
end

