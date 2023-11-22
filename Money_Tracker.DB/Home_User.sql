CREATE TABLE [dbo].[Home_User]
(
	[Home_Id] INT,
	[User_Id] INT,
	CONSTRAINT PK_Home_User PRIMARY KEY ([Home_Id],[User_Id]),
    CONSTRAINT FK_Home_User__Home FOREIGN KEY ([Home_Id]) REFERENCES Home([Home_Id]) ON DELETE CASCADE,
    CONSTRAINT FK_Home_User__User FOREIGN KEY ([User_Id]) REFERENCES [User]([User_Id])
)
