CREATE TABLE [dbo].[User]
(
    [ID] int NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(256) NOT NULL, 
	[Password] NVARCHAR(256) not null,
	[Role] NVARCHAR(50) not null,
	[ShopID] int FOREIGN KEY REFERENCES Shop(ID) ,
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate()
)
