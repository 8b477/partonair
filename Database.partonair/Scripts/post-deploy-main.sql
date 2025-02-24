-- Nettoyer les tables existantes
DELETE FROM [dbo].[Evaluations];
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
    (@User1Id, 'Jean Dupont'   , 'jean.dupont@email.com'   , 'hash123456', 'Visitor' , '2024-01-15', 1, '2024-02-15'),
    (@User2Id, 'Marie Martin'  , 'marie.martin@email.com'  , 'hash234567', 'Company' , '2024-01-16', 1, '2024-02-16'),
    (@User3Id, 'Pierre Bernard', 'pierre.bernard@email.com', 'hash345678', 'Employee', '2024-01-17', 0, '2024-02-14'),
    (@User4Id, 'Sophie Dubois' , 'sophie.dubois@email.com' , 'hash456789', 'Visitor' , '2024-01-18', 1, '2024-02-17'),
    (@User5Id, 'Lucas Petit'   , 'lucas.petit@email.com'   , 'hash567890', 'Employee', '2024-01-19', 1, '2024-02-15');

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
    (@Profile2Id, 'Chef de projet IT senior'                          , @User2Id),
    (@Profile3Id, 'Designer UX/UI freelance'                          , @User3Id),
    (@Profile4Id, 'Développeuse Full Stack JavaScript'                , @User4Id),
    (@Profile5Id, 'Étudiant en informatique'                          , @User5Id);

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
DECLARE @Contact4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Contact5Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Contacts]
    ([Id], [ContactName], [ContactEmail], [AddedAt], [IsFriendly], [AcceptedAt], [IsBlocked], [BlockedAt], [ContactStatus], [FK_User], [FK_Contact])
VALUES
    (@Contact1Id, 'Emma Wilson'    , 'emma.wilson@email.com'    , '2024-01-20 10:30:00', 1, '2024-01-20 11:45:00', 0, NULL, 'Accepted', @User1Id, @User4Id),
    (@Contact2Id, 'Thomas Anderson', 'thomas.anderson@email.com', '2024-01-21 14:45:00', 0, NULL                 , 0, NULL, 'Pending' , @User2Id, @User5Id),
    (@Contact3Id, 'Julie Brown'    , 'julie.brown@email.com'    , '2024-01-22 09:15:00', 1, '2024-01-22 10:30:00', 0, NULL, 'Accepted', @User3Id, @User1Id),
    (@Contact4Id, 'Pierre Bernard' , 'pierre.bernard@email.com' , '2024-01-23 11:00:00', 1, '2024-01-23 13:20:00', 0, NULL, 'Accepted', @User4Id, @User1Id),
    (@Contact5Id, 'Sophie Dubois'  , 'sophie.dubois@email.com'  , '2024-01-24 16:30:00', 0, NULL                 , 0, NULL, 'Pending' , @User5Id, @User2Id);


-- Insertion des évaluations
DECLARE @Evaluation1Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Evaluation2Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Evaluation3Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Evaluation4Id UNIQUEIDENTIFIER = NEWID();
DECLARE @Evaluation5Id UNIQUEIDENTIFIER = NEWID();

INSERT INTO [dbo].[Evaluations]
    ([Id], [EvaluationCommentary], [EvaluationCreatedAt], [EvaluationValue], [FK_Owner], [FK_Sender])
VALUES
    (@Evaluation1Id, 'Excellent travail en équipe'            , '2024-02-01', 5, @User1Id, @User4Id   ),
    (@Evaluation2Id, 'Bonne communication'                    , '2024-02-02', 4, @User2Id, @User5Id   ),
    (@Evaluation3Id, 'Compétences techniques impressionnantes', '2024-02-03', 5, @User3Id, @User1Id   ),
    (@Evaluation4Id, 'Collaboration efficace'                 , '2024-02-04', 4, @User4Id, @User1Id),
    (@Evaluation5Id, 'Très professionnel'                     , '2024-02-05', 5, @User5Id, @User2Id   );
