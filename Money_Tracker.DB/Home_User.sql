CREATE TABLE [dbo].[Home_User]
(
	[Home_Id] INT,
    [User_Id] INT,
    [Owner_User_Id] INT,
    [Resident_User_Id] INT,
    CONSTRAINT PK_Home_User PRIMARY KEY ([Home_Id],[User_Id]),
    CONSTRAINT FK_Home_User_Home FOREIGN KEY ([Home_Id]) REFERENCES Home([Home_Id]) ON DELETE CASCADE,
    CONSTRAINT FK_Home_User_User FOREIGN KEY ([User_Id]) REFERENCES [User]([User_Id]),
    CONSTRAINT FK_Home_User_Owner FOREIGN KEY ([Owner_User_Id]) REFERENCES [User]([User_Id]),
    CONSTRAINT FK_Home_User_Resident FOREIGN KEY ([Resident_User_Id]) REFERENCES [User]([User_Id]),
)
