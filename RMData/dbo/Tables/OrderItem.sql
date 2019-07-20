﻿CREATE TABLE [dbo].[OrderItem]
(
    [ID] INT NOT NULL PRIMARY KEY IDENTITY, 
	[OrderID] nvarchar(150) NOT NULL FOREIGN KEY REFERENCES [Order](ID),
	[ProductID] nvarchar(150) NOT NULL FOREIGN KEY REFERENCES [Product](ID),
	[ProductName] NVARCHAR(100) NOT NULL, 
	[Quantity] int NOT NULL
)