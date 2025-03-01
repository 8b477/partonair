﻿CREATE TABLE [dbo].[Contacts]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	[ContactName] VARCHAR(200) NOT NULL,
	[ContactEmail] VARCHAR(250) NOT NULL,
	[AddedAt] DATETIME NOT NULL,
	[IsFriendly] BIT NOT NULL,
	[AcceptedAt] DATETIME,
	[IsBlocked] BIT NOT NULL,
	[BlockedAt] DATETIME,
	[ContactStatus] VARCHAR(50) DEFAULT 'Pending' NOT NULL,
	[Id_Sender] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[Id_Receiver] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL
)
