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
(3, 1, 1, 125.00, 'Santé', '2023-12-25');
