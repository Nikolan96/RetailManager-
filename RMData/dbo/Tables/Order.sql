CREATE TABLE [dbo].[Order]
(
    [ID] nvarchar(150) NOT NULL PRIMARY KEY, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
	[ShopID] int NOT NULL foreign key references Shop(ID),
	[IsApproved] Bit NOT NULL
)
