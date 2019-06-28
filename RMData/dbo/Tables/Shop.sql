CREATE TABLE [dbo].[Shop]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY,
	[Town] NVARCHAR(100) not null,
	[Address] NVARCHAR(100) not null,
	[NumOfEmployees] int default 0
)
