﻿CREATE PROCEDURE [dbo].spProduct_GetAll

AS
begin
	set nocount on;
	select Id, Tax, ProductName, [Description], RetailPrice, QuantityInStock, PurchasePrice, Category, Margin
	from dbo.Product
	order by ProductName;
end
