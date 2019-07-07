CREATE PROCEDURE [dbo].[spUpdateProductQuantity]
    @Id int,
	@Quantity int
AS
begin
	set nocount on;
	Update Product 
	Set QuantityInStock = @Quantity
	where Id = @Id;
end
