CREATE PROCEDURE [dbo].[spDeleteOrderItems]
	@OrderID nvarchar
AS
begin
	set nocount on;
	delete from [OrderItem] where OrderID = @OrderID;
end

