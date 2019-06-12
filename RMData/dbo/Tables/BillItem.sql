CREATE TABLE [dbo].[BillItem]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[BillId] int NOT NULL FOREIGN KEY REFERENCES Bill(Id),
	[ProductName] NVARCHAR(100) NOT NULL, 
	[Category] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Quantity] int NOT NULL,
	[RetailPrice] money NOT NULL,

)
