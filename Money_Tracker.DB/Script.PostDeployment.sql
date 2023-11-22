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

---- Insertion de données factices pour la table Users
--INSERT INTO Users (Name, Firstname, Email)
--VALUES ('John', 'Doe', 'john@example.com'),
--       ('Alice', 'Smith', 'alice@example.com'),
--       ('Bob', 'Johnson', 'bob@example.com');

---- Insertion de données factices pour la table Categories
--INSERT INTO Categories (Category_Name)
--VALUES ('Food'),
--       ('Transportation'),
--       ('Utilities');

---- Insertion de données factices pour la table Homes
--INSERT INTO Homes (User_ID, Name_Home)
--VALUES (1, 'John''s Home'),
--       (2, 'Alice''s Home'),
--       (3, 'Bob''s Home');

---- Insertion de données factices pour la table Expenses
--INSERT INTO Expenses (Category_ID, User_ID, Home_ID, Amount, Description, Date_Expense)
--VALUES (1, 1, 1, 50.25, 'Groceries', '2023-11-01'),
--       (2, 2, 2, 30.50, 'Bus Fare', '2023-11-02'),
--       (3, 3, 3, 100.00, 'Electricity Bill', '2023-11-03');