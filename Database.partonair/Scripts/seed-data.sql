-- seed-data.sql

USE partonair;

-- Déclaration des GUID
DECLARE @User1Guid UNIQUEIDENTIFIER  = NEWID();
DECLARE @User2Guid UNIQUEIDENTIFIER  = NEWID();
DECLARE @ProfileEmployee1Guid UNIQUEIDENTIFIER  = NEWID();

-- Insérer les utilisateurs
INSERT INTO [dbo].[Users] (Id, UserName, Email, PasswordHashed, IsPublic, [Role], LastConnection, UserCreatedAt)
VALUES 
    (@User1Guid, 'jhon', 'jhon@mail.be', 'Test1234*', 0, 'Visitor', GETDATE(), GETDATE()),
    (@User2Guid, 'jane', 'jane@mail.be', 'Test1234*', 1, 'Employee', GETDATE(), GETDATE());

-- Insérer les profils
INSERT INTO [dbo].[Profiles] (Id, ProfileDescription, FK_User)
VALUES
    (@ProfileEmployee1Guid, N'Développeur full-stack avec 5 ans d''expérience.', @User2Guid);

-- Mettre à jour les utilisateurs avec les clés étrangères
UPDATE [dbo].[Users]
SET FK_Profile = @ProfileEmployee1Guid
WHERE Id = @User2Guid;

-- Insérer les contacts
INSERT INTO [dbo].[Contacts] (Id, ContactName, ContactEmail)
VALUES
    (NEWID(), 'jhon', 'jhon@mail.be'),
    (NEWID(), 'jane', 'jane@mail.be');
