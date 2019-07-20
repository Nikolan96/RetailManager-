CREATE PROCEDURE [dbo].[spDeleteOrderItems]
	@OrderID nvarchar(150)
AS
begin
	set nocount on;
	delete from [OrderItem] where OrderID = @OrderID;
end

