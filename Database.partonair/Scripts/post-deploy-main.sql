-- Nettoyer les tables existantes
DELETE FROM [dbo].[Evaluations];
DELETE FROM [dbo].[Friendly];
DELETE FROM [dbo].[Contacts];
DELETE FROM [dbo].[Profiles];
DELETE FROM [dbo].[Users];

-- Insertion des utilisateurs
DECLARE @User1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @User2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @User3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @User4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @User5Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Users] 
    ([Id], [UserName], [Email], [PasswordHashed], [Role], [UserCreatedAt], [IsPublic], [LastConnection])
VALUES
    (@User1Id, 'Jean Dupont', 'jean.dupont@email.com', 'hash123456', 'Visitor', '2024-01-15', 1, '2024-02-15'),
    (@User2Id, 'Marie Martin', 'marie.martin@email.com', 'hash234567', 'Company', '2024-01-16', 1, '2024-02-16'),
    (@User3Id, 'Pierre Bernard', 'pierre.bernard@email.com', 'hash345678', 'Employee', '2024-01-17', 0, '2024-02-14'),
    (@User4Id, 'Sophie Dubois', 'sophie.dubois@email.com', 'hash456789', 'Visitor', '2024-01-18', 1, '2024-02-17'),
    (@User5Id, 'Lucas Petit', 'lucas.petit@email.com', 'hash567890', 'Employee', '2024-01-19', 1, '2024-02-15');

-- Insertion des profils
DECLARE @Profile1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Profile2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Profile3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Profile4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Profile5Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Profiles]
    ([Id], [ProfileDescription], [FK_User])
VALUES
    (@Profile1Id, 'Développeur web passionné avec 5 ans d''expérience', @User1Id),
    (@Profile2Id, 'Chef de projet IT senior', @User2Id),
    (@Profile3Id, 'Designer UX/UI freelance', @User3Id),
    (@Profile4Id, 'Développeuse Full Stack JavaScript', @User4Id),
    (@Profile5Id, 'Étudiant en informatique', @User5Id);

-- Mise à jour des FK_Profile dans Users
UPDATE [dbo].[Users] SET [FK_Profile] = @Profile1Id WHERE [Id] = @User1Id;
UPDATE [dbo].[Users] SET [FK_Profile] = @Profile2Id WHERE [Id] = @User2Id;
UPDATE [dbo].[Users] SET [FK_Profile] = @Profile3Id WHERE [Id] = @User3Id;
UPDATE [dbo].[Users] SET [FK_Profile] = @Profile4Id WHERE [Id] = @User4Id;
UPDATE [dbo].[Users] SET [FK_Profile] = @Profile5Id WHERE [Id] = @User5Id;

-- Insertion des contacts
DECLARE @Contact1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Contact2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Contact3Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Contacts]
    ([Id], [ContactName], [ContactEmail])
VALUES
    (@Contact1Id, 'Emma Wilson', 'emma.wilson@email.com'),
    (@Contact2Id, 'Thomas Anderson', 'thomas.anderson@email.com'),
    (@Contact3Id, 'Julie Brown', 'julie.brown@email.com');

-- Insertion des relations amicales
INSERT INTO [dbo].[Friendly]
    ([AddedAt], [FriendlyStatus], [FK_User], [Fk_Contact])
VALUES
    ('2024-01-20', 'Accepted', @User1Id, @Contact1Id),
    ('2024-01-21', 'Pending', @User2Id, @Contact2Id),
    ('2024-01-22', 'Accepted', @User3Id, @Contact3Id),
    ('2024-01-23', 'Accepted', @User4Id, @Contact1Id),
    ('2024-01-24', 'Pending', @User5Id, @Contact2Id);

-- Insertion des évaluations
INSERT INTO [dbo].[Evaluations]
    ([EvaluationCommentary], [EvaluationCreatedAt], [EvaluationValue], [FK_User], [FK_Contact])
VALUES
    ('Excellent travail en équipe', '2024-02-01', 5, @User1Id, @Contact1Id),
    ('Bonne communication', '2024-02-02', 4, @User2Id, @Contact2Id),
    ('Compétences techniques impressionnantes', '2024-02-03', 5, @User3Id, @Contact3Id),
    ('Collaboration efficace', '2024-02-04', 4, @User4Id, @Contact1Id),
    ('Très professionnel', '2024-02-05', 5, @User5Id, @Contact2Id);
