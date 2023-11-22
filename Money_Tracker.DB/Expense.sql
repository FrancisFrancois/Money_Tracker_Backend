CREATE TABLE [dbo].[Expense]
(
	[Expense_Id] INT NOT NULL IDENTITY,
	[Category_Id] INT NOT NULL,
	[User_Id] INT NOT NULL,
	[Home_Id] INT NOT NULL,
	[Amount] FLOAT,
	[Description] NVARCHAR(300),
	[Date_Expense] DATE,
	CONSTRAINT PK_Expense PRIMARY KEY ([Expense_Id]),
	CONSTRAINT FK_Expense_Category FOREIGN KEY ([Category_Id]) REFERENCES[Category]([Category_Id]),
	CONSTRAINT FK_Expense_Home FOREIGN KEY ([Home_Id]) REFERENCES [Home]([Home_Id]),
	CONSTRAINT FK_Expense_User FOREIGN KEY ([User_Id]) REFERENCES [User]([User_Id])

)
