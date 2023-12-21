using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System.Data.Common;


namespace Money_Tracker.DAL.Repositories
{
    // La classe CategoryRepository gère l'accès aux données des catégories (Category) dans la base de données.
    public class CategoryRepository : ICategoryRepository
    {
        // Connexion à la base de données utilisée pour exécuter les requêtes.
        private readonly DbConnection _DbConnection;

        // Constructeur pour initialiser la connexion à la base de données.
        public CategoryRepository(DbConnection dbconnection)
        {
            _DbConnection = dbconnection;
        }

        // Méthode pour récupérer toutes les catégories de la base de données.
        public IEnumerable<Category> GetAll()
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour sélectionner toutes les catégories.
                command.CommandText = "SELECT * FROM [Category]";

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de l'enregistrement retournée par la requête.
                    while (reader.Read())
                    {
                        // Convertit chaque enregistrement en un objet Expense et le renvoie.
                        yield return CategoryMapper.Mapper(reader);
                    }
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
        }


        // Méthode pour récuperer une catégorie spécifique par son identifiant.
        public Category? GetById(int id)
        {
            Category? result = null;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL avec un paramètre pour l'identifiant.
                command.CommandText = "SELECT * FROM [Category] WHERE [Category_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé.
                    if (reader.Read())
                    {
                        // Conversion de l'enregistrement en objet Category
                        result = CategoryMapper.Mapper(reader);
                    }
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };

            // Renvoi de l'objet Category trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Méthode pour créer une nouvelle catégorie dans la base de données.
        public Category Create(Category category)
        {
            Category result;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour insérer une nouvelle catégorie.
                command.CommandText = "INSERT INTO [Category] ([Category_Name]) OUTPUT INSERTED.* VALUES (@category_name)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("category_name", category.Category_Name);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Si aucun enregistrement n'est retournée après l'insertion.
                    if (!reader.Read())
                    {
                        // Lève une exception si aucune ligne n'est retournée.
                        throw new Exception("Erreur lors de l'ajout de la catégorie");
                    }

                    // Convertit l'enregistrement insérée en un objet Category
                    result = CategoryMapper.Mapper(reader);
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };

            // Renvoi de l'objet Category trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Méthode pour mettre à jour une catégorie existante dans la base de données.
        public bool Update(int id, Category category)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour mettre à jour une catégorie spécifique.
                command.CommandText =
                    "UPDATE [Category] SET [Category_Name] = @category_name WHERE [Category_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("category_name", category.Category_Name);
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


        // Méthode pour supprimer une catégorie de la base de données.
        public bool Delete(int id)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour supprimer une catégorie par son identifiant.
                command.CommandText = "DELETE FROM [Category] WHERE [Category_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement affectées.
                int nbRowDeleted = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été supprimé, sinon faux.
                return nbRowDeleted == 1;
            };
        }
    }
}
