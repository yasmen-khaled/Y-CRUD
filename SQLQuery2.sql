CREATE TABLE [dbo].[users]
(
	[id] INT PRIMARY KEY IDENTITY(1,1),
    [full_Name] NVARCHAR(100) NOT NULL,
    [contact] VARCHAR(20) NOT NULL,
    [email] NVARCHAR(255) UNIQUE NOT NULL,
    [bith_date] DATE NOT NULL,
	[date_insert] DATETIME DEFAULT GETDATE(),
    [date_update] DATETIME DEFAULT GETDATE()
)

