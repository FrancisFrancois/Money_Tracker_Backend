using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    // Classe ExpenseMapper : Fournit des méthodes statiques pour mapper entre les modèles de la BLL et les entités de la DAL
    public static class ExpenseMapper
    {
        // Convertit une entité Expense (DAL) en un modèle Expense (BLL).
        public static Models.Expense ToModel(this Entities.Expense entity)
        {
            return new Models.Expense
            {
                Id = entity.Id, // Mappe l'identifiant de l'entité vers le modèle.
                Category_Id = entity.Category_Id, // Mappe l'identifiant de la catégorie.
                User_Id = entity.User_Id, // Mappe l'identifiant de l'utilisateur.
                Home_Id = entity.Home_Id, // Mappe l'identifiant du domicile.
                Amount = entity.Amount, // Mappe le montant de la dépense.
                Description = entity.Description, // Mappe la description de la dépense.
                Date_Expense = entity.Date_Expense, // Mappe la date de la dépense.
            };
        }

        // Convertit un modèle Expense (BLL) en une entité Expense (DAL).
        public static Entities.Expense ToEntity(this Models.Expense model)
        {
            return new Entities.Expense
            {
                Id = model.Id, // Mappe l'identifiant du modèle vers l'entité.
                Category_Id = model.Category_Id, // Mappe l'identifiant de la catégorie.
                User_Id = model.User_Id, // Mappe l'identifiant de l'utilisateur.
                Home_Id = model.Home_Id, // Mappe l'identifiant du domicile.
                Amount = model.Amount, // Mappe le montant de la dépense.
                Description = model.Description, // Mappe la description de la dépense.
                Date_Expense = model.Date_Expense, // Mappe la date de la dépense.
            };
        }
    }
}
