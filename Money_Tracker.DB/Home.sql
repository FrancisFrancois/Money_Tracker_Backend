﻿CREATE TABLE [dbo].[Home]
(
	[Home_Id] INT NOT NULL IDENTITY,
	[User_Id] INT NOT NULL,
	[Name_Home] NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_Home PRIMARY KEY ([Home_Id]),
	CONSTRAINT FK_Home_User FOREIGN KEY ([User_Id]) REFERENCES [User]([User_Id]) ON DELETE CASCADE

)
