﻿CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	[UserName] VARCHAR(200) NOT NULL,
	[Email] VARCHAR(250) UNIQUE NOT NULL,
	[PasswordHashed] VARCHAR(MAX) NOT NULL,
	[Role] VARCHAR(50) NOT NULL DEFAULT 'Visitor',
	[UserCreatedAt] DATETIME NOT NULL,
	[IsPublic] BIT NOT NULL,
	[LastConnection] DATETIME NOT NULL,
	[FK_Profile] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Profiles](Id)
)
