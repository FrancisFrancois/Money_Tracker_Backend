using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace Money_Tracker.DAL.Repositories
{
    // La classe CategoryRepository gère l'accès aux données des maisons (Home) dans la base de données.
    public class HomeRepository : IHomeRepository
    {
        // Connexion à la base de données utilisée pour exécuter les requêtes.
        private readonly DbConnection _DbConnection;

        // Constructeur pour initialiser la connexion à la base de données.
        public HomeRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        // Méthode pour récupérer toutes les maisons de la base de données.
        public IEnumerable<Home> GetAll()
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour sélectionner toutes les maisons.
                command.CommandText = "SELECT * FROM [Home]";

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de l'enregistrement retenu par la requête.
                    while (reader.Read())
                    {
                        // Convertit chaque enregistrement en un objet Home et le renvoie.
                        yield return HomeMapper.Mapper(reader);
                    }
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }
        }

        // Methode pour sélectionner une maison spécifique par son identifiant.
        public Home? GetById(int id)
        {
            Home? result = null;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL avec un paramètre pour l'identifiant.
                command.CommandText = "SELECT * FROM [Home] WHERE [Home_Id] = @id";

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
                        // Conversion de l'enregistrement en objet Home
                        result = HomeMapper.Mapper(reader);
                    }
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
            // Renvoi de l'objet Home trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Méthode pour créer une nouvelle maison dans la base de données.
        public Home Create(Home home)
        {
            Home result;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour insérer une nouvelle maison.
                command.CommandText = "INSERT INTO [Home] ([User_Id],[Name_Home]) " +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES (@user_id, @name_home)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("@user_id", home.User_Id);
                command.addParamWithValue("@name_home", home.Name_Home);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de l'enregistrement retenu par la requête.
                    if (!reader.Read())
                    {
                        // Lève une exception si aucune ligne n'est retournée
                        throw new Exception("Erreur lors de l'ajout de la maison");
                    }

                    // Conversion de l'enregistrement en objet Home
                    result = HomeMapper.Mapper(reader);
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour insérer l'utilisateur et la maison dans Home_User
                command.CommandText = "INSERT INTO [Home_User] ([Home_Id],[User_Id])" +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES (@home_id, @user_id)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("@home_id", result.Id); // Utilise l'ID de la maison créée.
                command.addParamWithValue("@user_id", home.User_Id); // ID de l'utilisateur.

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande pour ajouter dans la table de jointure.
                command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Renvoi de l'objet Home trouvé ou null si aucun n'a été trouvé
            return result;
        }

        // Methode pour mettre à jour une maison dans la base de données.
        public bool Update(int id, Home home)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour mettre à jour une maison.
                command.CommandText =
                    "UPDATE [Home]" +
                    " SET [Name_Home] = @name_home" +
                    " WHERE [Home_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("name_home", home.Name_Home);
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrements affectés.
                int nbRowUpdated = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été mis à jour, sinon faux.
                return nbRowUpdated == 1;
            }
        }

        // Methode pour supprimer une maison de la base de données.
        public bool Delete(int id)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour supprimer une maison par son identifiant.
                command.CommandText = "DELETE FROM [Home] WHERE [Home_Id] = @id";

                // Définition de la requête SQL pour supprimer une maison par son identifiant.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement affectés.
                int nbRowDeleted = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été supprimé, sinon faux.
                return nbRowDeleted == 1;
            };
        }

        // Methode pour recuperer les utilisateurs d'une maison
        public IEnumerable<HomeUser> GetUsers(int userId)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand()) 
            {
                // Définition de la requête SQL pour sélectionner les utilisateurs d'une maison spécifique.
                command.CommandText = "SELECT * FROM [Home_User] WHERE [Home_id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", userId);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de l'enregistrement retenu par la requête.
                    while (reader.Read())
                    {
                        // Convertit chaque enregistrement en un objet HomeUser
                        yield return HomeUserMapper.MapperHS(reader);
                    }
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

        }

        // Methode pour ajouter un utilisateur à une maison

        public HomeUser AddUserToHome(HomeUser homeUser)
        {
            HomeUser result;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour ajouter un utilisateur à une maison
                command.CommandText = "INSERT INTO [Home_User] ([Home_Id],[User_Id]) " +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES (@home_id, @user_id)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("home_id", homeUser.Home_Id);
                command.addParamWithValue("user_id", homeUser.User_Id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Si aucun enregistrement n'est retournée après l'insertion.
                    if (!reader.Read())
                    {
                        // Lève une exception si aucune ligne n'est retournée.
                        throw new Exception("Erreur lors de l'ajout de l'utilisateur à la maison");
                    }
                    // Convertit l'enregistrement insérée en un objet Home_User
                    result = HomeUserMapper.MapperHS(reader);
                };
                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
            // Renvoi de l'objet Home_User trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Methode pour supprimer un utilisateur d'une maison
        public bool RemoveUserFromHome(int homeId, int userId)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour supprimer un utilisateur d'une maison
                command.CommandText = "DELETE FROM [Home_User] WHERE [Home_Id] = @home_id AND [User_Id] = @user_id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("home_id", homeId);
                command.addParamWithValue("user_id", userId);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement affectés.
                int nbRowDeleted = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été supprimé, sinon faux.
                return nbRowDeleted == 1;
            }
        }

    }
}

