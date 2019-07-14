CREATE PROCEDURE [dbo].[spUpdateProductQuantityCanceled]
    @ID nvarchar(150),
	@QuantitySold int
AS
begin
	set nocount on;
	Update Product 
	Set QuantityInStock = QuantityInStock + @QuantitySold
	where ID = @ID;
end
