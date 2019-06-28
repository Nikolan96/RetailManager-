CREATE TABLE [dbo].[Bill]
(
	[ID] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
	[ShopID] int NOT NULL foreign key references Shop(ID),
	[Total] MONEY NOT NULL,
	[Paid] MONEY NOT NULL,
	[Change] Money NOT NULL,
    [UserId] int NOT NULL FOREIGN KEY REFERENCES [User](ID) 
)
