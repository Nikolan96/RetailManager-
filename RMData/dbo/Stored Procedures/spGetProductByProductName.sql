﻿CREATE PROCEDURE [dbo].[spGetProductByProductName]
	@productName varchar(50)
AS
begin
	set nocount on;
	select Id, Tax, ProductName, [Description], RetailPrice, Margin, QuantityInStock, PurchasePrice, Category
	from dbo.Product
	where ProductName = @productName;
end