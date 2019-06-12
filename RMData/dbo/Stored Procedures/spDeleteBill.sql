CREATE PROCEDURE [dbo].[spDeleteBill]
	@id int
AS
begin
	set nocount on;
	delete from Bill where Bill.Id = @id;
end

