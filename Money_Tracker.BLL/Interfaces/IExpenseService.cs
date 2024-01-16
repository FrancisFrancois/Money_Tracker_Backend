using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IExpenseService : Spécifie les opérations pour la gestion des dépenses
    // Hérite de ICrudService pour implémenter les opérations CRUD de base pour les objets Expense
    public interface IExpenseService : ICrudService<int, Expense>
    {
        #region Unused
        #endregion

        IEnumerable<Expense> GetAll(int userId);

        // Récupère les dépenses pour un jour donné, avec des options de filtrage par domicile, utilisateur, et catégorie
        IEnumerable<Expense> GetExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Récupère les dépenses pour une semaine donnée, avec des options de filtrage similaires
        IEnumerable<Expense> GetExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Récupère les dépenses pour un mois donné, avec des options de filtrage similaires
        IEnumerable<Expense> GetExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Récupère les dépenses pour une année donnée, avec des options de filtrage similaires
        IEnumerable<Expense> GetExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Calcule le total des dépenses pour un jour donné, avec des options de filtrage similaires
        double GetTotalExpensesByDay(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Calcule le total des dépenses pour une semaine donnée, avec des options de filtrage similaires
        double GetTotalExpensesByWeek(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Calcule le total des dépenses pour un mois donné, avec des options de filtrage similaires
        double GetTotalExpensesByMonth(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);

        // Calcule le total des dépenses pour une année donnée, avec des options de filtrage similaires
        double GetTotalExpensesByYear(DateTime date, int? homeId = null, int? userId = null, int? categoryId = null);
    }
}
