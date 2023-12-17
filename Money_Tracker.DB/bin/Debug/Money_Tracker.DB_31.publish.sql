﻿/*
Script de déploiement pour Money_Tracker

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Money_Tracker"
:setvar DefaultFilePrefix "Money_Tracker"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Création de la base de données $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Création de Table [dbo].[Category]...';


GO
CREATE TABLE [dbo].[Category] (
    [Category_Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Category_Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Category_Id] ASC),
    CONSTRAINT [UK_Category_Name] UNIQUE NONCLUSTERED ([Category_Name] ASC)
);


GO
PRINT N'Création de Table [dbo].[Expense]...';


GO
CREATE TABLE [dbo].[Expense] (
    [Expense_Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Category_Id]  INT            NOT NULL,
    [User_Id]      INT            NOT NULL,
    [Home_Id]      INT            NOT NULL,
    [Amount]       FLOAT (53)     NULL,
    [Description]  NVARCHAR (300) NULL,
    [Date_Expense] DATE           NULL,
    CONSTRAINT [PK_Expense] PRIMARY KEY CLUSTERED ([Expense_Id] ASC)
);


GO
PRINT N'Création de Table [dbo].[Home]...';


GO
CREATE TABLE [dbo].[Home] (
    [Home_Id]   INT           IDENTITY (1, 1) NOT NULL,
    [User_Id]   INT           NOT NULL,
    [Name_Home] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Home] PRIMARY KEY CLUSTERED ([Home_Id] ASC)
);


GO
PRINT N'Création de Table [dbo].[Home_User]...';


GO
CREATE TABLE [dbo].[Home_User] (
    [Home_Id] INT NOT NULL,
    [User_Id] INT NOT NULL,
    CONSTRAINT [PK_Home_User] PRIMARY KEY CLUSTERED ([Home_Id] ASC, [User_Id] ASC)
);


GO
PRINT N'Création de Table [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [User_Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [Firstname]     NVARCHAR (50) NOT NULL,
    [Pseudo]        NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (50) NOT NULL,
    [Hash_Password] CHAR (100)    NOT NULL,
    [Roles]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([User_Id] ASC),
    CONSTRAINT [UK_User__Email] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [UK_User__Pseudo] UNIQUE NONCLUSTERED ([Pseudo] ASC)
);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Expense_Category]...';


GO
ALTER TABLE [dbo].[Expense]
    ADD CONSTRAINT [FK_Expense_Category] FOREIGN KEY ([Category_Id]) REFERENCES [dbo].[Category] ([Category_Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Expense_Home]...';


GO
ALTER TABLE [dbo].[Expense]
    ADD CONSTRAINT [FK_Expense_Home] FOREIGN KEY ([Home_Id]) REFERENCES [dbo].[Home] ([Home_Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Expense_User]...';


GO
ALTER TABLE [dbo].[Expense]
    ADD CONSTRAINT [FK_Expense_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([User_Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User]...';


GO
ALTER TABLE [dbo].[Home]
    ADD CONSTRAINT [FK_Home_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([User_Id]) ON DELETE CASCADE;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User_Home]...';


GO
ALTER TABLE [dbo].[Home_User]
    ADD CONSTRAINT [FK_Home_User_Home] FOREIGN KEY ([Home_Id]) REFERENCES [dbo].[Home] ([Home_Id]) ON DELETE CASCADE;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User_User]...';


GO
ALTER TABLE [dbo].[Home_User]
    ADD CONSTRAINT [FK_Home_User_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([User_Id]);


GO
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

-- insertion de données factices pour la table users
INSERT INTO [User] (Name, Firstname, Pseudo, Email, Hash_Password, Roles)
VALUES 
('Depp', 'Johnny', 'johnnydepp', 'johnny.depp@example.com', 'hashed_password_1', 'admin'),
('Winslet', 'Kate', 'katewinslet', 'kate.winslet@example.com', 'hashed_password_2', 'admin'),
('DiCaprio', 'Leonardo', 'leonardodicaprio', 'leonardo.dicaprio@example.com', 'hashed_password_3', 'admin'),
('Lawrence', 'Jennifer', 'jenniferlawrence', 'jennifer.lawrence@example.com', 'hashed_password_4', 'invite'),
('Smith', 'Will', 'willsmith', 'will.smith@example.com', 'hashed_password_5', 'invite'),
('Roberts', 'Julia', 'juliaroberts', 'julia.roberts@example.com', 'hashed_password_6', 'invite'),
('Hanks', 'Tom', 'tomhanks', 'tom.hanks@example.com', 'hashed_password_7', 'invite'),
('Johansson', 'Scarlett', 'scarlettjohansson', 'scarlett.johansson@example.com', 'hashed_password_8', 'invite'),
('Damon', 'Matt', 'mattdamon', 'matt.damon@example.com', 'hashed_password_9', 'invite');


-- Insertion de données factices supplémentaires pour la table Homes
INSERT INTO Home (USER_ID, Name_Home) VALUES
(1, 'Johnny House'),
(2, 'Kate Vacation House'),
(3, 'Leonardo Family Residence');


-- insertion de données factices pour la table categories
INSERT INTO Category (Category_Name) VALUES
('Alimentation'),
('Transport'),
('Services publics'),
('Logement'),
('Divertissement'),
('Voyages'),
('Santé'),
('Éducation'),
('Vêtements'),
('Autres');

-- insertion de données factices pour la home_user
INSERT INTO Home_User (Home_Id, User_Id)
VALUES
(1, 1), 
(2, 2), 
(3, 3), 
(1, 4), 
(2, 5),
(3, 6); 


-- insertion de données factices pour la table expenses
INSERT INTO Expense (Category_ID, User_ID, Home_ID, Amount, Description, Date_Expense) VALUES
(1, 1, 1, 40.75, 'Légumes', '2023-11-05'),
(2, 2, 2, 25.00, 'Tickets de métro', '2023-11-06'),
(3, 3, 3, 85.50, 'Facture d''eau', '2023-11-08'),
(1, 1, 1, 55.30, 'Fruits', '2023-11-10'),
(2, 2, 2, 20.00, 'Frais de taxi', '2023-11-12'),
(3, 3, 3, 95.20, 'Facture de gaz', '2023-11-15'),
(1, 2, 2, 30.00, 'Courses', '2023-11-17'),
(2, 3, 3, 15.50, 'Train ticket', '2023-11-18'),
(3, 1, 1, 70.80, 'Électricité', '2023-11-20'),
(1, 2, 2, 45.25, 'Restaurant', '2023-11-22'),
(2, 3, 3, 18.00, 'Stationnement', '2023-11-24'),
(3, 1, 1, 60.30, 'Internet', '2023-11-26'),
(1, 2, 2, 38.50, 'Café', '2023-11-28'),
(2, 3, 3, 22.75, 'Bus', '2023-11-29'),
(3, 1, 1, 75.00, 'Téléphone', '2023-12-01'),
(1, 2, 2, 43.20, 'Livres', '2023-12-03'),
(2, 3, 3, 28.50, 'Cinéma', '2023-12-05'),
(3, 1, 1, 90.60, 'Assurance', '2023-12-07'),
(1, 2, 2, 50.25, 'Cadeau', '2023-12-09'),
(2, 3, 3, 35.00, 'Magasinage', '2023-12-11'),
(3, 1, 1, 105.00, 'Impôts', '2023-12-13'),
(1, 2, 2, 60.75, 'Gym', '2023-12-15'),
(2, 3, 3, 42.90, 'Restauration rapide', '2023-12-17'),
(3, 1, 1, 115.50, 'Réparations', '2023-12-19'),
(1, 2, 2, 70.20, 'Événement', '2023-12-21'),
(2, 3, 3, 58.30, 'Fournitures de bureau', '2023-12-23'),
(3, 1, 1, 125.00, 'Santé', '2023-12-25'),
(1, 1, 1, 37.45, 'Voyage', '2023-12-27'),
(2, 2, 2, 12.75, 'Cinéma', '2023-12-28'),
(3, 3, 3, 55.90, 'Restaurant', '2023-12-29'),
(1, 2, 2, 29.50, 'Cadeau', '2023-12-30'),
(2, 3, 3, 18.25, 'Frais de livraison', '2023-12-31'),
(3, 1, 1, 70.10, 'Assurance auto', '2024-01-01'),
(1, 2, 2, 42.75, 'Fournitures scolaires', '2024-01-02'),
(2, 3, 3, 27.80, 'Frais médicaux', '2024-01-03'),
(3, 1, 1, 90.25, 'Abonnement streaming', '2024-01-04'),
(1, 2, 2, 38.60, 'Réparations voiture', '2024-01-05'),
(2, 3, 3, 22.00, 'Épicerie', '2024-01-06'),
(3, 1, 1, 65.75, 'Café', '2024-01-07'),
(1, 2, 2, 45.20, 'Électricité', '2024-01-08'),
(2, 3, 3, 18.90, 'Location de DVD', '2024-01-09'),
(3, 1, 1, 72.30, 'Voyage', '2024-01-10'),
(1, 2, 2, 55.10, 'Frais de taxi', '2024-01-11'),
(2, 3, 3, 32.50, 'Restaurant', '2024-01-12'),
(3, 1, 1, 80.75, 'Cadeau', '2024-01-13'),
(1, 2, 2, 40.20, 'Frais de téléphone', '2024-01-14'),
(2, 3, 3, 28.90, 'Gym', '2024-01-15'),
(3, 1, 1, 95.00, 'Courses', '2024-01-16'),
(1, 2, 2, 42.60, 'Facture électricité', '2024-01-17'),
(2, 3, 3, 19.75, 'Tickets de concert', '2024-01-18'),
(3, 1, 1, 68.40, 'Facture de gaz', '2024-01-19'),
(1, 2, 2, 58.90, 'Frais de train', '2024-01-20'),
(2, 3, 3, 35.25, 'Cinéma', '2024-01-21'),
(3, 1, 1, 75.50, 'Frais médicaux', '2024-01-22'),
(1, 2, 2, 44.75, 'Abonnement magazine', '2024-01-23'),
(2, 3, 3, 21.30, 'Frais de transport', '2024-01-24'),
(3, 1, 1, 82.60, 'Restaurant', '2024-01-25'),
(1, 2, 2, 49.20, 'Assurance habitation', '2024-01-26'),
(2, 3, 3, 30.75, 'Facture de téléphone', '2024-01-27'),
(3, 1, 1, 88.00, 'Fournitures de bureau', '2024-01-28'),
(1, 2, 2, 42.40, 'Cadeau anniversaire', '2024-01-29'),
(2, 3, 3, 25.60, 'Assurance maladie', '2024-01-30'),
(3, 1, 1, 78.25, 'Frais de stationnement', '2024-01-31'),
(1, 2, 2, 36.80, 'Cinéma', '2024-02-01'),
(2, 3, 3, 18.50, 'Frais de repas', '2024-02-02'),
(3, 1, 1, 70.90, 'Facture eau', '2024-02-03'),
(1, 2, 2, 52.30, 'Impôts fonciers', '2024-02-04'),
(2, 3, 3, 27.40, 'Internet', '2024-02-05'),
(3, 1, 1, 98.60, 'Frais de vélo', '2024-02-06'),
(1, 2, 2, 40.75, 'Café', '2024-02-07'),
(2, 3, 3, 22.90, 'Cadeau de Noël', '2024-02-08'),
(3, 1, 1, 65.50, 'Cours de musique', '2024-02-09'),
(1, 2, 2, 48.20, 'Frais de taxi', '2024-02-10'),
(2, 3, 3, 29.75, 'Frais médicaux', '2024-02-11'),
(3, 1, 1, 84.30, 'Restaurant', '2024-02-12'),
(1, 2, 2, 50.80, 'Frais de téléphone', '2024-02-13'),
(2, 3, 3, 33.20, 'Fournitures de bureau', '2024-02-14'),
(3, 1, 1, 72.90, 'Assurance automobile', '2024-02-15'),
(1, 2, 2, 45.60, 'Frais de jardinage', '2024-02-16'),
(2, 3, 3, 20.75, 'Facture de gaz', '2024-02-17'),
(3, 1, 1, 68.10, 'Café', '2024-02-18'),
(1, 2, 2, 38.90, 'Abonnement streaming', '2024-02-19'),
(2, 3, 3, 26.20, 'Cadeau anniversaire', '2024-02-20'),
(3, 1, 1, 94.75, 'Frais de téléphone', '2024-02-21'),
(1, 2, 2, 48.60, 'Facture électricité', '2024-02-22'),
(2, 3, 3, 30.10, 'Assurance santé', '2024-02-23'),
(3, 1, 1, 79.40, 'Frais de transport', '2024-02-24'),
(1, 2, 2, 42.80, 'Facture eau', '2024-02-25'),
(2, 3, 3, 22.95, 'Voyage', '2024-02-26'),
(3, 1, 1, 67.20, 'Cadeau de mariage', '2024-02-27'),
(1, 2, 2, 56.50, 'Frais de stationnement', '2024-02-28'),
(1, 1, 1, 55.30, 'Fruits', '2024-03-01'),
(2, 2, 2, 20.00, 'Frais de taxi', '2024-03-02'),
(3, 3, 3, 95.20, 'Facture de gaz', '2024-03-03'),
(1, 2, 2, 30.00, 'Courses', '2024-03-04'),
(2, 3, 3, 15.50, 'Train ticket', '2024-03-05'),
(3, 1, 1, 70.80, 'Électricité', '2024-03-06'),
(1, 2, 2, 45.25, 'Restaurant', '2024-03-07'),
(2, 3, 3, 18.00, 'Stationnement', '2024-03-08'),
(3, 1, 1, 60.30, 'Internet', '2024-03-09'),
(1, 2, 2, 38.50, 'Café', '2024-03-10'),
(2, 3, 3, 22.75, 'Bus', '2024-03-11'),
(3, 1, 1, 75.00, 'Téléphone', '2024-03-12'),
(1, 2, 2, 43.20, 'Livres', '2024-03-13'),
(2, 3, 3, 28.50, 'Cinéma', '2024-03-14'),
(3, 1, 1, 90.60, 'Assurance', '2024-03-15'),
(1, 2, 2, 50.25, 'Cadeau', '2024-03-16'),
(2, 3, 3, 35.00, 'Magasinage', '2024-03-17'),
(3, 1, 1, 105.00, 'Impôts', '2024-03-18'),
(1, 2, 2, 60.75, 'Gym', '2024-03-19')
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Mise à jour terminée.';


GO
