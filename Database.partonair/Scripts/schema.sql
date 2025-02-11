-- schema.sql

-- Crée la base de données si elle n'existe pas déjà
IF DB_ID('partonair') IS NULL
BEGIN
    CREATE DATABASE partonair;
END;

-- Bascule sur la base de données partonair
USE partonair;

-- Supprime les tables si elles existent déjà (dans l'ordre pour éviter les problèmes de clés étrangères)
IF OBJECT_ID('dbo.Evaluations', 'U') IS NOT NULL DROP TABLE dbo.Evaluations;
IF OBJECT_ID('dbo.Friendly', 'U') IS NOT NULL DROP TABLE dbo.Friendly;
IF OBJECT_ID('dbo.Contacts', 'U') IS NOT NULL DROP TABLE dbo.Contacts;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.Profiles', 'U') IS NOT NULL DROP TABLE dbo.Profiles;


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
    [FK_User] UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY ([FK_User]) REFERENCES [Users]([Id])
);

-- Ajout de la clé étrangère après la création de Profiles
ALTER TABLE [dbo].[Users] 
ADD FOREIGN KEY ([FK_Profile]) REFERENCES [Profiles]([Id]);

CREATE TABLE [dbo].[Contacts] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [ContactName] VARCHAR(200) NOT NULL,
    [ContactEmail] VARCHAR(250) NOT NULL
);

CREATE TABLE [dbo].[Friendly] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [AddedAt] DATETIME NOT NULL,
    [RemovedAt] DATETIME,
    [FriendlyStatus] VARCHAR(50) DEFAULT 'Pending',
    [FK_User] UNIQUEIDENTIFIER NOT NULL,
    [Fk_Contact] UNIQUEIDENTIFIER NOT NULL,
    FOREIGN KEY ([FK_User]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([Fk_Contact]) REFERENCES [Contacts]([Id])
);

CREATE TABLE [dbo].[Evaluations] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    [EvaluationCommentary] NVARCHAR(MAX) NOT NULL,
    [EvaluationCreatedAt] DATETIME NOT NULL,
    [EvaluationUpdatedAt] DATETIME,
    [EvaluationValue] INT NOT NULL,
    [FK_User] UNIQUEIDENTIFIER NOT NULL,
    [FK_Contact] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT CHK_EvaluationValue_Range CHECK ([EvaluationValue] >= 1 AND [EvaluationValue] <= 5),
    FOREIGN KEY ([FK_User]) REFERENCES [Users]([Id]),
    FOREIGN KEY ([FK_Contact]) REFERENCES [Contacts]([Id])
);
