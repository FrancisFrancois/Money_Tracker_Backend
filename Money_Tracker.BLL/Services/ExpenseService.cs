using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;


namespace Money_Tracker.BLL.Services
{
    // Classe ExpenseService : Implémente les opérations de gestion des dépenses spécifiées dans l'interface IExpenseService.
    public class ExpenseService : IExpenseService
    {
        // Référence au repository des dépenses pour l'interaction avec la base de données.
        private readonly IExpenseRepository _ExpenseRepository;

        // Constructeur pour injecter la dépendance du repository des dépenses.
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _ExpenseRepository = expenseRepository;
        }

        // Récupère toutes les dépenses et les convertit en modèles (Expense).
        public IEnumerable<Expense> GetAll()
        {
            // Utilise le repository pour récupérer toutes les dépenses et les convertit de l'entité vers le modèle Expense.
            return _ExpenseRepository.GetAll().Select(e => e.ToModel());
        }

        // Récupère une dépense spécifique par son ID et la convertit en modèle Expense.
        public Expense? GetById(int id)
        {
            // Utilise le repository pour trouver une dépense par son ID. Si trouvée, la convertit en modèle Expense, sinon renvoie null.
            return _ExpenseRepository.GetById(id)?.ToModel();
        }

        // Crée une nouvelle dépense.
        public Expense Create(Expense expense)
        {
            // Utilise le repository pour créer une nouvelle dépense dans la base de données et renvoie le modèle Expense créé.
            return _ExpenseRepository.Create(expense.ToEntity()).ToModel();
        }

        // Met à jour une dépense existante.
        public bool Update(int id, Expense expense)
        {
            // Tente de mettre à jour la dépense et renvoie un booléen indiquant si la mise à jour a réussi
            bool updated = _ExpenseRepository.Update(id, expense.ToEntity());
            if (!updated)
            {
                // Exception indique que l'opération de suppression a échoué 
                throw new NotFoundException("Expense not found");
            }
            return updated;
        }

        // Supprime une dépense par son ID.
        public bool Delete(int id)
        {
            // Tente de supprimer la dépense et renvoie un booléen indiquant si la suppression à jour a réussi
            bool deleted = _ExpenseRepository.Delete(id);
            if (!deleted)
            {
                // Exception indique que l'opération de suppression a échoué 
                throw new NotFoundException("Expense not found");
            }
            return deleted;
        }

        #region Unused

        //    public IEnumerable<Expense> GetExpensesByDay(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByDay(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByWeek(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByWeek(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByMonth(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByMonth(date).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByYear(DateTime date)
        //    {
        //        return _ExpenseRepository.GetExpensesByYear(date).Select(e => e.ToModel());
        //    }
        //    public double GetTotalExpensesByDay(DateTime date)
        //    {
        //        return GetExpensesByDay(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByWeek(DateTime date)
        //    {
        //        return GetExpensesByWeek(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByMonth(DateTime date)
        //    {
        //        return GetExpensesByMonth(date).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByYear(DateTime date)
        //    {
        //        return GetExpensesByYear(date).Sum(expense => expense.Amount);
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByDay(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByDay(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByWeek(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByWeek(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByMonth(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByMonth(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public IEnumerable<Expense> GetExpensesByCategoryByYear(DateTime date, int categoryId)
        //    {
        //        return _ExpenseRepository.GetExpensesByCategoryByYear(date, categoryId).Select(e => e.ToModel());
        //    }

        //    public double GetTotalExpensesByCategoryByDay(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByDay(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByWeek(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByWeek(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByMonth(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByMonth(date, categoryId).Sum(expense => expense.Amount);
        //    }

        //    public double GetTotalExpensesByCategoryByYear(DateTime date, int categoryId)
        //    {
        //        return GetExpensesByCategoryByYear(date, categoryId).Sum(expense => expense.Amount);
        //    }

        #endregion

        // Méthode pour obtenir les dépenses pour un jour spécifique, avec des options de filtrage.
        public IEnumerable<Expense> GetExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                // Définit les limites de la journée pour la récupération des dépenses.
                // La variable 'startDate' est fixée au début de la journée spécifiée par 'date'.
                DateTime startDate = date.Date;

                // La variable 'endDate' est fixée à la fin de la même journée.
                // 'AddDays(1)' ajoute un jour à 'startDate', et 'AddSeconds(-1)' soustrait une seconde,
                // ce qui donne le dernier moment de la journée spécifiée.
                DateTime endDate = startDate.AddDays(1).AddSeconds(-1);

                // Récupère les dépenses pour cette journée en utilisant la méthode 'GetExpenses' du repository
                // Les dépenses sont récupérées entre 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                // Les résultats sont convertis en modèles 'Expense' à l'aide de la méthode 'ToModel'.
                return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel()); 
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors de la récupération des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get expenses by day", ex);
            }
        }


        // Méthode pour obtenir les dépenses pour une semaine spécifique, avec des options de filtrage.
        public IEnumerable<Expense> GetExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                // Calcule le début de la semaine basée sur la date donnée.
                // 'AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday)' calcule le lundi de la semaine actuelle,
                // En soustrayant le nombre de jours écoulés depuis le lundi.
                DateTime startDate = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

                // Calcule la fin de la semaine (le dimanche suivant).
                // 'AddDays(7)' ajoute sept jours à 'startDate' (le lundi), puis 'AddSeconds(-1)' soustrait une seconde pour arriver à la fin du dimanche.
                DateTime endDate = startDate.AddDays(7).AddSeconds(-1);

                // Récupère les dépenses pour cette semaine en utilisant la méthode 'GetExpenses' du repository
                // Les dépenses sont récupérées pour la semaine définie par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                // Les résultats sont convertis en modèles 'Expense' à l'aide de la méthode 'ToModel'.
                return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors de la récupération des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get expenses by week", ex);
            }
        }


        // Méthode pour obtenir les dépenses pour un mois spécifique, avec des options de filtrage.
        public IEnumerable<Expense> GetExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                // Définit le début du mois basé sur la date donnée.
                // Utilise l'année et le mois de la date fournie, mais fixe le jour à 1, début du mois.
                DateTime startDate = new DateTime(date.Year, date.Month, 1);

                // Définit la fin du mois.
                // 'AddMonths(1)' ajoute un mois à 'startDate' pour se déplacer au début du mois suivant,
                // puis 'AddDays(-1)' soustrait un jour pour revenir au dernier jour du mois actuel.
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                // Récupère les dépenses pour ce mois en utilisant la méthode 'GetExpenses' du repository
                // Les dépenses sont récupérées pour le mois défini par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                // Les résultats sont convertis en modèles 'Expense' à l'aide de la méthode 'ToModel'.
                return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel()); 
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors de la récupération des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get expenses by month", ex);
            }
        }


        // Méthode pour obtenir les dépenses pour une année spécifique, avec des options de filtrage.
        public IEnumerable<Expense> GetExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                // Définit le début de l'année basé sur la date donnée.
                // Utilise l'année de la date fournie, mais fixe le mois et le jour au début de l'année (1er janvier).
                DateTime startDate = new DateTime(date.Year, 1, 1);

                // Définit la fin de l'année.
                // Utilise l'année de la date fournie et fixe le mois et le jour à la fin de l'année (31 décembre).
                DateTime endDate = new DateTime(date.Year, 12, 31);

                // Récupère les dépenses pour cette année en utilisant la méthode 'GetExpenses' du repository
                // Les dépenses sont récupérées pour l'année définie par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                // Les résultats sont convertis en modèles 'Expense' à l'aide de la méthode 'ToModel'.
                return _ExpenseRepository.GetExpenses(startDate, endDate, homeId, userId, categoryId).Select(e => e.ToModel());
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors de la récupération des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get expenses by year", ex);
            }
        }

        // Méthode pour calculer le total des dépenses pour un jour spécifique, avec des options de filtrage.
        public double GetTotalExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {
            try
            {
                // Définit les limites de la journée pour la récupération des dépenses.
                // La variable 'startDate' est fixée au début de la journée spécifiée par 'date'.
                DateTime startDate = date.Date;

                // La variable 'endDate' est fixée à la fin de la même journée.
                // 'AddDays(1)' ajoute un jour à 'startDate', et 'AddSeconds(-1)' soustrait une seconde,
                // ce qui donne le dernier moment de la journée spécifiée.
                DateTime endDate = startDate.AddDays(1).AddSeconds(-1);

                // Calcule le total des dépenses pour cette journée en utilisant la méthode 'CalculateTotalExpenses' du repository
                // Les dépenses sont récupérées pour le jour défini défini par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors du calcul du total des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get total expenses by day", ex);
            }
        }


        public double GetTotalExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
                // Calcule le début de la semaine basée sur la date donnée.
                // 'AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday)' calcule le lundi de la semaine actuelle,
                // En soustrayant le nombre de jours écoulés depuis le lundi.
                DateTime startDate = date.AddDays(-(int)date.DayOfWeek + (int)DayOfWeek.Monday);

                // Calcule la fin de la semaine (le dimanche suivant).
                // 'AddDays(7)' ajoute sept jours à 'startDate' (le lundi), puis 'AddSeconds(-1)' soustrait une seconde pour arriver à la fin du dimanche.
                DateTime endDate = startDate.AddDays(7).AddSeconds(-1);

                // Calcule le total des dépenses pour cette semaine en utilisant la méthode 'CalculateTotalExpenses' du repository
                // Les dépenses sont récupérées pour la semaine définie par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors du calcul du total des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get total expenses by week", ex);
            }
        }

        public double GetTotalExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
                // Définit le début du mois basé sur la date donnée.
                // Utilise l'année et le mois de la date fournie, mais fixe le jour à 1, début du mois.
                DateTime startDate = new DateTime(date.Year, date.Month, 1);

                // Définit la fin du mois.
                // 'AddMonths(1)' ajoute un mois à 'startDate' pour se déplacer au début du mois suivant,
                // puis 'AddDays(-1)' soustrait un jour pour revenir au dernier jour du mois actuel.
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                // Calcule le total des dépenses pour ce mois en utilisant la méthode 'CalculateTotalExpenses' du repository
                // Les dépenses sont récupérées pour le mois défini par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors du calcul du total des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get total expenses by month", ex);
            }
        }

        public double GetTotalExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null)
        {   
            try
            {
                // Définit le début de l'année basé sur la date donnée.
                // Utilise l'année de la date fournie, mais fixe le mois et le jour au début de l'année (1er janvier).
                DateTime startDate = new DateTime(date.Year, 1, 1);

                // Définit la fin de l'année.
                // Utilise l'année de la date fournie et fixe le mois et le jour à la fin de l'année (31 décembre).
                DateTime endDate = new DateTime(date.Year, 12, 31);

                // Calcule le total des dépenses pour cette année en utilisant la méthode 'CalculateTotalExpenses' du repository
                // Les dépenses sont récupérées pour l'année définie par 'startDate' et 'endDate'.
                // Les paramètres optionnels 'homeId', 'userId' et 'categoryId' permettent de filtrer les dépenses
                // Par domicile, utilisateur ou catégorie, si ces valeurs sont fournies.
                return _ExpenseRepository.CalculateTotalExpenses(startDate, endDate, homeId, userId, categoryId);
            }
            catch (Exception ex)
            {
                // En cas d'erreur lors du calcul du total des dépenses, une exception est levée.
                // L'exception originale 'ex' est incluse pour fournir des détails supplémentaires sur l'erreur survenue.
                throw new Exception("Failed to get total expenses by year", ex);
            }
        }
    }
}
