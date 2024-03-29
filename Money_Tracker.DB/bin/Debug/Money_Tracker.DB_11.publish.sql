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
USE [$(DatabaseName)];


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
-- Insertion de données factices supplémentaires pour la table Users (noms d'acteurs fictifs)
INSERT INTO Users (Name, Firstname, Email) VALUES
('Depp', 'Johnny', 'johnny.depp@example.com'),
('Winslet', 'Kate', 'kate.winslet@example.com'),
('DiCaprio', 'Leonardo', 'leonardo.dicaprio@example.com'),
('Lawrence', 'Jennifer', 'jennifer.lawrence@example.com'),
('Smith', 'Will', 'will.smith@example.com'),
('Roberts', 'Julia', 'julia.roberts@example.com'),
('Hanks', 'Tom', 'tom.hanks@example.com'),
('Johansson', 'Scarlett', 'scarlett.johansson@example.com'),
('Damon', 'Matt', 'matt.damon@example.com'),
('Kidman', 'Nicole', 'nicole.kidman@example.com');


-- Insertion de données factices supplémentaires pour la table Homes
INSERT INTO Homes (User_ID, Name_Home) VALUES
(1, 'John  Second Home'),
(2, 'Alice  Vacation House'),
(3, 'Bob Family Residence');

INSERT INTO Home_User (Home_Id, User_Id, Owner_User_Id, Resident_User_Id)
VALUES
(1, 1, 1, 2), -- John (User_ID 1) possède la maison 1 et Alice (User_ID 2) y réside.
(2, 2, 2, 3), -- Alice (User_ID 2) possède la maison 2 et Bob (User_ID 3) y réside.
(3, 3, 3, 1), -- Bob (User_ID 3) possède la maison 3 et John (User_ID 1) y réside.
(1, 4, 1, 1), -- Nouvelle personne (User_ID 4) réside aussi dans la maison 1 de John (User_ID 1).
(2, 5, 2, 2), -- Nouvelle personne (User_ID 5) réside aussi dans la maison 2 de Alice (User_ID 2).
(3, 6, 3, 3); -- Nouvelle personne (User_ID 6) réside aussi dans la maison 3 de Bob (User_ID 3).


-- insertion de données factices pour la table categories
INSERT INTO Categories (Category_Name) VALUES
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

-- insertion de données factices pour la table expenses
INSERT INTO Expenses (Category_ID, User_ID, Home_ID, Amount, Description, Date_Expense) VALUES
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
(3, 1, 1, 125.00, 'Santé', '2023-12-25');
GO

GO
PRINT N'Mise à jour terminée.';


GO
