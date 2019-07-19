CREATE PROCEDURE [dbo].[spDeleteBill]
	@Id NVARCHAR(128)
AS
begin
	set nocount on;
	delete from Bill where Bill.ID = @Id;
end

