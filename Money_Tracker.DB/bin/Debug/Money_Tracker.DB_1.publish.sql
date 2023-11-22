/*
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
USE [$(DatabaseName)];


GO
PRINT N'Début de la régénération de la table [dbo].[User]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_User] (
    [User_Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (50) NOT NULL,
    [Firstname]     NVARCHAR (50) NOT NULL,
    [Pseudo]        NVARCHAR (50) NOT NULL,
    [Email]         NVARCHAR (50) NOT NULL,
    [Hash_Password] CHAR (100)    NOT NULL,
    [Roles]         NVARCHAR (50) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_User1] PRIMARY KEY CLUSTERED ([User_Id] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_UK_User__Email1] UNIQUE NONCLUSTERED ([Email] ASC),
    CONSTRAINT [tmp_ms_xx_constraint_UK_User__Pseudo1] UNIQUE NONCLUSTERED ([Pseudo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[User])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] ON;
        INSERT INTO [dbo].[tmp_ms_xx_User] ([User_Id], [Name], [Firstname], [Pseudo], [Email], [Hash_Password], [Roles])
        SELECT   [User_Id],
                 [Name],
                 [Firstname],
                 [Pseudo],
                 [Email],
                 [Hash_Password],
                 [Roles]
        FROM     [dbo].[User]
        ORDER BY [User_Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_User] OFF;
    END

DROP TABLE [dbo].[User];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_User]', N'User';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_User1]', N'PK_User', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_UK_User__Email1]', N'UK_User__Email', N'OBJECT';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_UK_User__Pseudo1]', N'UK_User__Pseudo', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


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
PRINT N'Mise à jour terminée.';


GO
