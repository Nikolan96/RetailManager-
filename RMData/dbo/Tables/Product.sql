CREATE TABLE [dbo].[Product]
(
	[ID] nvarchar(150) NOT NULL PRIMARY KEY , 
    [ProductName] NVARCHAR(100) NOT NULL, 
	[Description] NVARCHAR(MAX) NOT NULL,
	[PurchasePrice] MONEY NOT NULL,
	[RetailPrice] MONEY NOT NULL,
	[Tax] Money NOT NULL,
    [Margin] MONEY NOT NULL,	
	[QuantityInStock] INT NOT NULL DEFAULT 1,
	[Category] NVARCHAR(50) not null,
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [LastModified] DATETIME2 NOT NULL DEFAULT getutcdate(),
	[ShopID] int NOt NULL FOREIGN KEY References Shop(ID)
    
    
)
