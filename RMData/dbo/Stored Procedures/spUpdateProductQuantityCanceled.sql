CREATE PROCEDURE [dbo].[spUpdateProductQuantityCanceled]
    @ID int,
	@QuantitySold int
AS
begin
	set nocount on;
	Update Product 
	Set QuantityInStock = QuantityInStock + @QuantitySold
	where Id = @ID;
end
