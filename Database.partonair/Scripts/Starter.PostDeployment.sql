/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- RESET
DELETE FROM Profiles;
DELETE FROM Users;
DELETE FROM Contacts;
DELETE FROM Friendly;
DELETE FROM Evaluations;


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

-- CONTACTS GUID
DECLARE @Contact1Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact2Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact3Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact4Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact5Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact6Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact7Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact8Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact9Guid   UNIQUEIDENTIFIER  = NEWID()
DECLARE @Contact10Guid  UNIQUEIDENTIFIER  = NEWID()

-- FRIENDLY GUID
DECLARE @Friendly1Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly2Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly3Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly4Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly5Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly6Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly7Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly8Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly9Guid  UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly10Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly11Guid UNIQUEIDENTIFIER  = NEWID()
DECLARE @Friendly12Guid UNIQUEIDENTIFIER  = NEWID()

-- EVALUATIONS GUID
DECLARE @Evaluation1Guid UNIQUEIDENTIFIER = NEWID()
DECLARE @Evaluation2Guid UNIQUEIDENTIFIER = NEWID()
DECLARE @Evaluation3Guid UNIQUEIDENTIFIER = NEWID()
DECLARE @Evaluation4Guid UNIQUEIDENTIFIER = NEWID()

-- USERS INSERT
IF NOT EXISTS (SELECT 1 FROM [Users])
BEGIN
    INSERT INTO [Users](Id,UserName,Email,PasswordHashed,IsPublic,[Role],LastConnection,UserCreatedAt,FK_Profile)
    VALUES 
        (@User1Guid  ,'jhon'    ,'jhon@mail.be'    ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE() ,NULL ),
        (@User2Guid  ,'jane'    ,'jane@mail.be'    ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE() ,NULL ),
        (@User3Guid  ,'bob'     ,'bob@mail.be'     ,'Test1234*' ,1 ,'Company'  ,GETDATE() ,GETDATE() ,NULL ),
        (@User4Guid  ,'alice'   ,'alice@mail.be'   ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE() ,NULL ),
        (@User5Guid  ,'charlie' ,'charlie@mail.be' ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE() ,NULL ),
        (@User6Guid  ,'diana'   ,'diana@mail.be'   ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE() ,NULL ),
        (@User7Guid  ,'evan'    ,'evan@mail.be'    ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE() ,NULL ),
        (@User8Guid  ,'fiona'   ,'fiona@mail.be'   ,'Test1234*' ,1 ,'Company'  ,GETDATE() ,GETDATE() ,NULL ),
        (@User9Guid  ,'george'  ,'george@mail.be'  ,'Test1234*' ,1 ,'Employee' ,GETDATE() ,GETDATE() ,NULL ),
        (@User10Guid ,'hannah'  ,'hannah@mail.be'  ,'Test1234*' ,0 ,'Visitor'  ,GETDATE() ,GETDATE() ,NULL );
END

-- PROFILES INSERT
IF NOT EXISTS (SELECT 1 FROM [Profiles])
BEGIN
    INSERT INTO [Profiles](Id, ProfileDescription, FK_User)
    VALUES
    (@ProfileEmployee1Guid ,N'Designer depuis 3 ans, j''aime créer des templates originaux et relever de nouveaux défis. N''hésitez pas à me contacter pour vos projets créatifs.', @User2Guid),
    (@ProfileEmployee2Guid ,N'Développeur full-stack avec 5 ans d''expérience. Passionné par les technologies web modernes et l''architecture logicielle.', @User5Guid),
    (@ProfileEmployee3Guid ,N'Jeune entreprise qui recherche tout forme de profils n''hésiter donc pas à nous contacter par message priver ou par mail.', @User6Guid),
    (@ProfileEmployee4Guid ,N'Spécialisé dans la transformation digitale des entreprises. Fort d''une expérience dans divers secteurs d''activité.', @User9Guid),
    (@ProfileCompany5Guid  ,N'Spécialiste en CI/CD, containerisation et orchestration. Passionné par l''amélioration continue des processus de déploiement.', @User3Guid),
    (@ProfileCompany6Guid  ,N'Notre entreprise PartOnAir, spécialisée dans la mise en relation, cherche activement des profils tech. Nous connectons les talents IT aux meilleures opportunités du marché, en mettant l''accent sur l''innovation et la croissance professionnelle.', @User8Guid);
END

-- USERS UPDATE
UPDATE Users
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


-- CONTACTS INSERT
IF NOT EXISTS (SELECT 1 FROM [Contacts])
BEGIN
    INSERT INTO [Contacts](Id,ContactName,ContactEmail)
    VALUES
            (@Contact1Guid   ,'jhon'    ,'jhon@mail.be'    ),
            (@Contact2Guid   ,'jane'    ,'jane@mail.be'    ),
            (@Contact3Guid   ,'bob'     ,'bob@mail.be'     ),
            (@Contact4Guid   ,'alice'   ,'alice@mail.be'   ),
            (@Contact5Guid   ,'charlie' ,'charlie@mail.be' ),
            (@Contact6Guid   ,'diana'   ,'diana@mail.be'   ),
            (@Contact7Guid   ,'evan'    ,'evan@mail.be'    ),
            (@Contact8Guid   ,'fiona'   ,'fiona@mail.be'   ),
            (@Contact9Guid  ,'george'  ,'george@mail.be'   ),
            (@Contact10Guid  ,'hannah'  ,'hannah@mail.be'  );
END

-- FRIENDLY INSERT
IF NOT EXISTS (SELECT 1 FROM [Friendly])
BEGIN
    INSERT INTO [Friendly](Id,AddedAt,FriendlyStatus,RemovedAt,FK_User,Fk_Contact)
    VALUES 
            (@Friendly1Guid  ,GETDATE() ,'Accepted' ,NULL ,@User2Guid,  @Contact1Guid  ),
            (@Friendly2Guid  ,GETDATE() ,'Accepted' ,NULL ,@User1Guid,  @Contact2Guid  ),

            (@Friendly3Guid  ,GETDATE() ,'Accepted' ,NULL ,@User3Guid,  @Contact1Guid  ),
            (@Friendly4Guid  ,GETDATE() ,'Accepted' ,NULL ,@User1Guid,  @Contact3Guid  ),

            (@Friendly5Guid  ,GETDATE() ,'Pending' ,NULL ,@User4Guid,  @Contact1Guid  ),
            (@Friendly6Guid  ,GETDATE() ,'Pending' ,NULL ,@User1Guid,  @Contact4Guid  ),

            (@Friendly7Guid  ,GETDATE() ,'Pending' ,NULL ,@User6Guid,  @Contact5Guid  ),
            (@Friendly8Guid  ,GETDATE() ,'Pending' ,NULL ,@User5Guid,  @Contact6Guid  ),

            (@Friendly9Guid  ,GETDATE() ,'Pending' ,NULL ,@User8Guid,  @Contact7Guid  ),
            (@Friendly10Guid ,GETDATE() ,'Pending' ,NULL ,@User7Guid,  @Contact8Guid  ),

            (@Friendly11Guid ,GETDATE() ,'Ignored' ,DATEADD(HOUR, 1, GETDATE()) ,@User9Guid,  @Contact10Guid ),
            (@Friendly12Guid ,GETDATE() ,'Ignored' ,DATEADD(HOUR, 1, GETDATE()) ,@User10Guid, @Contact9Guid  );
END

-- EVALUATIONS INSERT
IF NOT EXISTS (SELECT 1 FROM [Evaluations])
BEGIN
    INSERT INTO [Evaluations](Id,EvaluationCommentary,EvaluationValue,EvaluationCreatedAt,EvaluationUpdatedAt,FK_Contact,FK_User)
    VALUES
            (@Evaluation1Guid, N'Très pro et efficaces, je le recommande',5,GETDATE(),NULL,@Contact1Guid,@User2Guid ),
            (@Evaluation2Guid, N'Super taff',5,GETDATE(),NULL,@Contact1Guid,@User3Guid ),
            (@Evaluation3Guid, N'Déçus',1,GETDATE(),NULL,@Contact5Guid,@User6Guid ),
            (@Evaluation4Guid, N'Bon rapport qualité prix',3,GETDATE(),NULL,@Contact10Guid,@User9Guid );
END
