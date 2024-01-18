using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System.Data.Common;
using System;
using System.Data;

namespace Money_Tracker.DAL.Repositories
{
    // La classe UserRepository gère l'accès aux données des utilisateurs (User) dans la base de données.
    public class UserRepository : IUserRepository
    {
        // Connexion à la base de données utilisée pour exécuter les requêtes.
        private readonly DbConnection _DbConnection;

        // Constructeur pour initialiser la connexion à la base de données.
        public UserRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        // Méthode pour récupérer tous les utilisateurs de la base de données.
        public IEnumerable<User> GetAll()
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour sélectionner tous les utilisateurs.
                command.CommandText = "SELECT * FROM [User]";

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de l'enregistrement retenu par la requête.
                    while (reader.Read())
                    {
                        // Convertit chaque enregistrement en un objet User et le renvoie.
                        yield return UserMapper.Mapper(reader);
                    }
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }
        }

        // Methode pour sélectionner un utilisateur par son identifiant.
        public User? GetById(int id)
        {
            User? result = null;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL avec un paramètre pour l'identifiant.
                command.CommandText = "SELECT * FROM [User] WHERE [User_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé
                    if (reader.Read())
                    {
                        // Conversion de l'enregistrement en objet User
                        result = UserMapper.Mapper(reader);
                    }
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };

            // Renvoi de l'objet User sélectionne ou null si aucun n'a été trouvé.
            return result;
        }

        // Methode pour créer un nouvel utilisateur.
        public User Create(User user)
        {
            User result;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour insérer un nouvel utilisateur
                command.CommandText = "INSERT INTO [User] ([Name],[Firstname],[Pseudo],[Email],[Hash_Password],[Roles]) " +
                                       "OUTPUT INSERTED.* " +
                                       "VALUES (@name, @firstname, @pseudo, @email, @password, @roles)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("name", user.Lastname);
                command.addParamWithValue("firstname", user.Firstname);
                command.addParamWithValue("pseudo", user.Pseudo);
                command.addParamWithValue("email", user.Email);
                command.addParamWithValue("password", user.Password);
                command.addParamWithValue("roles", user.Roles);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();


                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Si aucun enregistrement n'est retournée après l'insertion.
                    if (!reader.Read())
                    {
                        // Lève une exception si aucune ligne n'est retournée.
                        throw new Exception("Erreur lors de l'ajout de l'utilisateur");
                    }

                    // Convertit l'enregistrement insérée en un objet User
                    result = UserMapper.Mapper(reader);
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
            // Renvoi de l'objet User trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Methode pour mettre à jour un utilisateur.
        public bool Update(int id, User user)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour mettre à jour un utilisateur.
                command.CommandText = "UPDATE [User] " +
                                      " SET [Name] = @name, " +
                                      "    [Firstname] = @firstname," +
                                      "    [Pseudo] = @pseudo, " +
                                      "    [Email] = @email, " +
                                      "    [Hash_Password] = @hash_password, " +
                                      "    [Roles] = @roles " +
                                      " WHERE [User_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("name", user.Lastname);
                command.addParamWithValue("firstname", user.Firstname);
                command.addParamWithValue("pseudo", user.Pseudo);
                command.addParamWithValue("email", user.Email);
                command.addParamWithValue("hash_password", user.Password);
                command.addParamWithValue("roles", user.Roles);
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrements modifiés.
                int nbRowUpdated = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été mis à jour, sinon faux.
                return nbRowUpdated == 1;
            }

        }

        // Methode pour supprimer un utilisateur de la base de données
        public bool Delete(int id)
        {
            // Création et configuration de la commande de base de données
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour supprimer un utilisateur par son identifiant.
                command.CommandText = "DELETE FROM [User] WHERE [User_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement supprimées.
                int nbRowDeleted = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été supprimé, sinon faux.
                return nbRowDeleted == 1;
            }
        }

        public bool isLivingInHouse(int id)
        {
            // Booléen pour vérifier si l'utilisateur habite dans une maison.
            bool isLiving = false;

            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Requête SQL pour vérifier si un utilisateur habite dans une maison.
                command.CommandText = "SELECT * FROM [Home_User] WHERE [User_Id] = @Id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("Id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé
                    if (reader.Read())
                    {
                        isLiving = true;
                    }
                }

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Renvoie true si l'utilisateur habite dans une maison, sinon false.
            return isLiving;
        }

        public User? GetUserByEmail(string email)
        {
            // Initialisation du résultat en tant que null.
            User? result = null;

            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Requête SQL pour obtenir un utilisateur par son email.
                command.CommandText = "SELECT * FROM [User] WHERE [Email] = @email";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("email", email);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé.
                    if (reader.Read())
                    {
                        // Convertit l'enregistrement insérée en un objet User
                        result = UserMapper.Mapper(reader) ?? throw new Exception("User by email not found");
                    }
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Renvoie l'utilisateur trouvé par son email ou null s'il n'existe pas.
            return result;
        }

        public User? GetUserByPseudo(string pseudo)
        {
            // Initialisation du résultat en tant que null.
            User? result = null;

            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Requête SQL pour obtenir un utilisateur par son pseudo.
                command.CommandText = "SELECT * FROM [User] WHERE [Pseudo] = @pseudo";
                command.addParamWithValue("pseudo", pseudo);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé.
                    if (reader.Read())
                    {
                        // Convertit l'enregistrement insérée en un objet User
                        result = UserMapper.Mapper(reader) ?? throw new Exception("User by Pseudo not found");
                    }
                }

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Renvoie l'utilisateur trouvé par son pseudo ou null s'il n'existe pas.
            return result;
        }

    }
}