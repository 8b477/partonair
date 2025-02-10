-- Starter.PostDeployment.sql

-- Ensure we're using the correct database
USE partonair;
GO

-- DROP EXISTING TABLES IF THEY EXIST (IN CORRECT ORDER TO HANDLE FOREIGN KEY CONSTRAINTS)
IF OBJECT_ID('dbo.Evaluations', 'U') IS NOT NULL DROP TABLE dbo.Evaluations;
IF OBJECT_ID('dbo.Friendly', 'U') IS NOT NULL DROP TABLE dbo.Friendly;
IF OBJECT_ID('dbo.Contacts', 'U') IS NOT NULL DROP TABLE dbo.Contacts;
IF OBJECT_ID('dbo.Profiles', 'U') IS NOT NULL DROP TABLE dbo.Profiles;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

-- CREATE TABLES IN CORRECT ORDER
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

-- Add foreign key to Users table after Profiles creation
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

-- USERS GUID
DECLARE @User1Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User2Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User3Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User4Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User5Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User6Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User7Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User8Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User9Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @User10Guid UNIQUEIDENTIFIER  = NEWID()

-- PROFILES GUID
DECLARE @ProfileEmployee1Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @ProfileEmployee2Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @ProfileEmployee3Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @ProfileEmployee4Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @ProfileCompany5Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @ProfileCompany6Guid  UNIQUEIDENTIFIER  = NEWID()

-- INSERT USERS WITH EXPLICIT GUIDS
INSERT INTO [dbo].[Users] (Id, UserName, Email, PasswordHashed, IsPublic, [Role], LastConnection, UserCreatedAt)
VALUES 
    (@User1Guid  ,'jhon'    ,'jhon@mail.be'    ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE()),
    (@User2Guid  ,'jane'    ,'jane@mail.be'    ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE()),
    (@User3Guid  ,'bob'     ,'bob@mail.be'     ,'Test1234*' ,1 ,'Company'  ,GETDATE() ,GETDATE()),
    (@User4Guid  ,'alice'   ,'alice@mail.be'   ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE()),
    (@User5Guid  ,'charlie' ,'charlie@mail.be' ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE()),
    (@User6Guid  ,'diana'   ,'diana@mail.be'   ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE()),
    (@User7Guid  ,'evan'    ,'evan@mail.be'    ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE()),
    (@User8Guid  ,'fiona'   ,'fiona@mail.be'   ,'Test1234*' ,1 ,'Company'  ,GETDATE() ,GETDATE()),
    (@User9Guid  ,'george'  ,'george@mail.be'  ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE()),
    (@User10Guid ,'hannah'  ,'hannah@mail.be'  ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE());

-- INSERT PROFILES
INSERT INTO [dbo].[Profiles] (Id, ProfileDescription, FK_User)
VALUES
    (@ProfileEmployee1Guid ,N'Designer depuis 3 ans, j''aime créer des templates originaux et relever de nouveaux défis. N''hésitez pas à me contacter pour vos projets créatifs.', @User2Guid),
    (@ProfileEmployee2Guid ,N'Développeur full-stack avec 5 ans d''expérience. Passionné par les technologies web modernes et l''architecture logicielle.', @User5Guid),
    (@ProfileEmployee3Guid ,N'Jeune entreprise qui recherche tout forme de profils n''hésiter donc pas à nous contacter par message priver ou par mail.', @User6Guid),
    (@ProfileEmployee4Guid ,N'Spécialisé dans la transformation digitale des entreprises. Fort d''une expérience dans divers secteurs d''activité.', @User9Guid),
    (@ProfileCompany5Guid  ,N'Spécialiste en CI/CD, containerisation et orchestration. Passionné par l''amélioration continue des processus de déploiement.', @User3Guid),
    (@ProfileCompany6Guid  ,N'Notre entreprise PartOnAir, spécialisée dans la mise en relation, cherche activement des profils tech. Nous connectons les talents IT aux meilleures opportunités du marché, en mettant l''accent sur l''innovation et la croissance professionnelle.', @User8Guid);

-- UPDATE USERS WITH PROFILE FOREIGN KEYS
UPDATE [dbo].[Users]
SET FK_Profile = CASE 
    WHEN Id = @User2Guid  THEN @ProfileEmployee1Guid
    WHEN Id = @User5Guid  THEN @ProfileEmployee2Guid
    WHEN Id = @User6Guid  THEN @ProfileEmployee3Guid
    WHEN Id = @User9Guid  THEN @ProfileEmployee4Guid
    WHEN Id = @User3Guid  THEN @ProfileCompany5Guid
    WHEN Id = @User8Guid  THEN @ProfileCompany6Guid
    ELSE FK_Profile
END
WHERE Id IN (@User2Guid, @User5Guid, @User6Guid, @User9Guid, @User3Guid, @User8Guid);

-- INSERT CONTACTS
INSERT INTO [dbo].[Contacts] (Id, ContactName, ContactEmail)
VALUES
    (NEWID(), 'jhon', 'jhon@mail.be'),
    (NEWID(), 'jane', 'jane@mail.be'),
    (NEWID(), 'bob', 'bob@mail.be'),
    (NEWID(), 'alice', 'alice@mail.be'),
    (NEWID(), 'charlie', 'charlie@mail.be'),
    (NEWID(), 'diana', 'diana@mail.be'),
    (NEWID(), 'evan', 'evan@mail.be'),
    (NEWID(), 'fiona', 'fiona@mail.be'),
    (NEWID(), 'george', 'george@mail.be'),
    (NEWID(), 'hannah', 'hannah@mail.be');

-- ADDITIONAL LOGIC TO BE INSERTED BY YOU
-- Insert Friendly and Evaluations data here

-- Optional: Add indexes for performance
CREATE NONCLUSTERED INDEX IX_Users_Email ON [dbo].[Users] (Email);
CREATE NONCLUSTERED INDEX IX_Profiles_User ON [dbo].[Profiles] (FK_User);