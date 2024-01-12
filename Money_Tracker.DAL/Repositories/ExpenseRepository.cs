using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Repositories
{
    // La classe ExpenseRepository gère l'accès aux données des dépenses (Expense) dans la base de données.
    public class ExpenseRepository : IExpenseRepository
    {
        // Connexion à la base de données.
        private readonly DbConnection _DbConnection;

        // Constructeur pour initialiser la connexion à la base de données.
        public ExpenseRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        // Méthode pour récupérer toutes les dépenses de la base de données.
        public IEnumerable<Expense> GetAll()
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour sélectionner toutes les dépenses.
                command.CommandText = "SELECT * FROM [Expense]";

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de enregistrement retournée par la requête.
                    while (reader.Read())
                    {
                        // Convertit chaque enregistrement en un objet Expense et le renvoie.
                        yield return ExpenseMapper.Mapper(reader);
                    }
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }
        }

        // Méthode pour récuperer une dépense spécifique par son identifiant.
        public Expense? GetById(int id)
        {
            Expense? result = null;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL avec un paramètre pour l'identifiant.
                command.CommandText = "SELECT * FROM [Expense] WHERE [Expense_Id] = @id";
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Vérification si un enregistrement est trouvé.
                    if (reader.Read())
                    {
                        // Conversion de l'enregistrement en objet Expense.
                        result = ExpenseMapper.Mapper(reader);
                    }
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
            // Renvoi de l'objet Expense trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Méthode pour créer une nouvelle dépense dans la base de données.
        public Expense Create(Expense expense)
        {
            Expense result;

            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour insérer une nouvelle dépense.
                command.CommandText = "INSERT INTO [Expense] ([Category_Id],[User_Id],[Home_id],[Amount],[Description],[Date_Expense]) " +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES(@category_id, @user_id, @home_id, @amount, @description, @date_expense)";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("category_id", expense.Category_Id);
                command.addParamWithValue("user_id", expense.User_Id);
                command.addParamWithValue("home_id", expense.Home_Id);
                command.addParamWithValue("amount", expense.Amount);
                command.addParamWithValue("description", expense.Description);
                command.addParamWithValue("date_expense", expense.Date_Expense);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Si aucun enregistrement n'est retournée après l'insertion.
                    if (!reader.Read())
                    {
                        // Lève une exception.
                        throw new Exception("Failed to create expense");
                    }

                    // Convertit l'enregistrement insérée en un objet Expense.
                    result = ExpenseMapper.Mapper(reader);
                };

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            };
            // Renvoi de l'objet Expense trouvé ou null si aucun n'a été trouvé.
            return result;
        }

        // Méthode pour mettre à jour une dépense existante dans la base de données.
        public bool Update(int id, Expense expense)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour mettre à jour une dépense spécifique.
                command.CommandText = "UPDATE [Expense] SET [Category_Id] = @category_id," +
                                             "[User_Id] = @user_id, " +
                                             "[Home_Id] = @home_id, " +
                                             "[Amount] = @amount, " +
                                             "[Description] = @description, " +
                                             "[Date_Expense] = @date_expense " +
                                      "WHERE [Expense_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("user_id", expense.User_Id);
                command.addParamWithValue("home_id", expense.Home_Id);
                command.addParamWithValue("category_id", expense.Category_Id);
                command.addParamWithValue("amount", expense.Amount);
                command.addParamWithValue("description", expense.Description);
                command.addParamWithValue("date_expense", expense.Date_Expense);
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement modifiés.
                int nbRowUpdated = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si un enregistrement a été mise à jour, sinon faux.
                return nbRowUpdated == 1;
            }
        }

        // Méthode pour supprimer une dépense de la base de données.
        public bool Delete(int id)
        {
            // Création et configuration de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Définition de la requête SQL pour supprimer une dépense par son identifiant.
                command.CommandText = "DELETE FROM [Expense] WHERE [Expense_Id] = @id";

                // Ajout des paramètres à la commande.
                command.addParamWithValue("id", id);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et obtention du nombre d'enregistrement supprimés.
                int nbRowDeleted = command.ExecuteNonQuery();

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();

                // Renvoi vrai si une ligne a été supprimée, sinon faux.
                return nbRowDeleted == 1;
            };
        }


        // Méthode pour obtenir les dépenses dans un intervalle de temps avec des options de filtrage.
        public IEnumerable<Expense> GetExpenses(DateTime startDate, DateTime endDate, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            // Création de la liste pour stocker les résultats.
            List<Expense> expenses = new List<Expense>();

            // Création de la commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Construction de la requête avec des critères de filtrage dynamiques pour obtenir les dépenses
                var query = new StringBuilder("SELECT * FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate");

                // Ajout de conditions supplémentaires à la requête si des critères de filtrage spécifiques sont fournis.
                if (homeId.HasValue)
                {
                    query.Append(" AND [Home_Id] = @homeId");
                    command.addParamWithValue("homeId", homeId.Value); // Ajout du paramètre homeId à la commande.
                }
                if (userId.HasValue)
                {
                    query.Append(" AND [User_Id] = @userId");
                    command.addParamWithValue("userId", userId.Value); // Ajout du paramètre userId à la commande.
                }
                if (categoryId.HasValue)
                {
                    query.Append(" AND [Category_Id] = @categoryId");
                    command.addParamWithValue("categoryId", categoryId.Value); // Ajout du paramètre categoryId à la commande.
                }

                // Affectation de la requête SQL à la commande.
                command.CommandText = query.ToString();
                command.addParamWithValue("startDate", startDate);
                command.addParamWithValue("endDate", endDate);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et création d'un lecteur de données.
                using (DbDataReader reader = command.ExecuteReader())
                {
                    // Lecture de chaque ligne retournée par la requête.
                    while (reader.Read())
                    {
                        // Ajout des objets Expense à la liste.
                        expenses.Add(ExpenseMapper.Mapper(reader));
                    }
                }

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Retourne la liste des dépenses.
            return expenses;
        }

        // Méthode pour calculer le total des dépenses dans un intervalle de temps avec des options de filtrage.
        public double CalculateTotalExpenses(DateTime startDate, DateTime endDate, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            double total = 0; // Variable pour stocker le total des dépenses.

            // Création d'une nouvelle commande de base de données.
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                // Construction de la requête avec des critères de filtrage dynamiques pour calculer des dépenses
                var query = new StringBuilder("SELECT SUM(Amount) FROM [Expense] WHERE [Date_Expense] >= @startDate AND [Date_Expense] <= @endDate");

                // La classe StringBuilder permet de modifier et de construire des chaînes de caractères de manière efficace.
                // Ici, elle est utilisée pour assembler la requête SQL avec les différents critères de filtrage.


                // Ajout de conditions supplémentaires à la requête si des critères de filtrage spécifiques sont fournis.
                if (homeId.HasValue)
                {
                    query.Append(" AND [Home_Id] = @homeId"); // Ajoute une condition pour filtrer par Home_Id.
                    command.addParamWithValue("homeId", homeId.Value); 
                }

                if (userId.HasValue)
                {
                    query.Append(" AND [User_Id] = @userId"); // Ajoute une condition pour filtrer par User_Id.
                    command.addParamWithValue("userId", userId.Value);
                }

       
                if (categoryId.HasValue)
                {
                    query.Append(" AND [Category_Id] = @categoryId"); // Ajoute une condition pour filtrer par Category_Id.
                    command.addParamWithValue("categoryId", categoryId.Value);
                }

                // La méthode Append de StringBuilder est utilisée pour ajouter ces conditions à la requête initiale sans créer de nouvelles chaînes à chaque fois.
                // Cela améliore les performances en réduisant la surcharge liée à la création d'objets chaîne de caractères.


                // Affectation de la requête SQL à la commande.
                command.CommandText = query.ToString();
                command.addParamWithValue("startDate", startDate);
                command.addParamWithValue("endDate", endDate);

                // Ouverture de la connexion à la base de données.
                _DbConnection.Open();

                // Exécution de la commande et récupération du résultat.
                var result = command.ExecuteScalar(); // ExecuteScalar est utilisé pour récupérer une seule valeur (ici, la somme des montants).

                // Calcul du total des dépenses.
                total = (result != DBNull.Value) ? Convert.ToDouble(result) : 0; // Convertit le résultat en double. Si le résultat est DBNull, affecte 0.

                // Fermeture de la connexion à la base de données.
                _DbConnection.Close();
            }

            // Renvoi du total calculé.
            return total;
        }
    }
}