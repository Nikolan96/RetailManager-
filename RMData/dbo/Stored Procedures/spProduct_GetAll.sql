﻿CREATE PROCEDURE [dbo].spProduct_GetAll

AS
begin
	set nocount on;
	select id, ProductName, [Description], RetailPrice, QuantityInStock
	from dbo.Product
	order by ProductName;
end
