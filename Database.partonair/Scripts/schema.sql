-- Crée la base de données si elle n'existe pas déjà
IF DB_ID('partonair') IS NULL
BEGIN
    CREATE DATABASE partonair;
END;

-- Bascule sur la base de données partonair
USE partonair;

-- Création des tables
CREATE TABLE [dbo].[Users] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [UserName] VARCHAR(200) NOT NULL,
    [Email] VARCHAR(250) UNIQUE NOT NULL,
    [PasswordHashed] VARCHAR(MAX) NOT NULL,
    [Role] VARCHAR(50) NOT NULL DEFAULT 'Visitor',
    [UserCreatedAt] DATETIME NOT NULL,
    [IsPublic] BIT NOT NULL,
    [LastConnection] DATETIME NOT NULL,
    [FK_Profile] UNIQUEIDENTIFIER
);

CREATE TABLE [dbo].[Profiles] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [ProfileDescription] NVARCHAR(MAX) NOT NULL,
	[FK_User] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL
);

-- Ajout de la clé étrangère après la création de Profiles
ALTER TABLE [dbo].[Users] 
ADD FOREIGN KEY ([FK_Profile]) REFERENCES [Profiles]([Id]);

CREATE TABLE [dbo].[Contacts]
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
);

CREATE TABLE [dbo].[Evaluations]
(
	[Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
	[EvaluationCommentary] NVARCHAR(MAX) NOT NULL,
	[EvaluationCreatedAt] DATETIME NOT NULL,
	[EvaluationUpdatedAt] DATETIME,
	[EvaluationValue] INT NOT NULL,
	CONSTRAINT CHK_EvaluationValue_Range CHECK ([EvaluationValue] >= 1 AND [EvaluationValue] <= 5),
	[FK_Owner] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL,
	[FK_Sender] UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [Users](Id) NOT NULL
);
