CREATE PROCEDURE [dbo].[spUpdateProductQuantitySold]
    @Id nvarchar(150),
	@QuantitySold int
AS
begin
	set nocount on;
	Update Product 
	Set QuantityInStock = QuantityInStock - @QuantitySold
	where Id = @Id;
end
