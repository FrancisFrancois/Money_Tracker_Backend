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
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User]...';


GO
ALTER TABLE [dbo].[Home]
    ADD CONSTRAINT [FK_Home_User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([User_Id]) ON DELETE CASCADE;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User__Home]...';


GO
ALTER TABLE [dbo].[Home_User]
    ADD CONSTRAINT [FK_Home_User__Home] FOREIGN KEY ([Home_Id]) REFERENCES [dbo].[Home] ([Home_Id]) ON DELETE CASCADE;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Home_User__User]...';


GO
ALTER TABLE [dbo].[Home_User]
    ADD CONSTRAINT [FK_Home_User__User] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[User] ([User_Id]) ON DELETE CASCADE;


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
