CREATE TABLE [dbo].[BillItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[BillId] nvarchar(128) NOT NULL FOREIGN KEY REFERENCES Bill(ID),
	[ProductName] NVARCHAR(100) NOT NULL, 
	[ProductID] int,
	[Category] NVARCHAR(50) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Quantity] int NOT NULL,
	[RetailPrice] money NOT NULL,

)
