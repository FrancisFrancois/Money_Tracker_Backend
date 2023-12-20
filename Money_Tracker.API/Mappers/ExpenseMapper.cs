using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    // Classe ExpenseMapper : Contient des méthodes statiques pour mapper les dépenses entre les modèles et les DTOs
    public static class ExpenseMapper
    {
        // Convertit un Modèle Expense en ExpenseDTO
        public static ExpenseDTO ToDTO(this Expense model)
        {
            return new ExpenseDTO
            {
                Id = model.Id, // Transfère l'ID du modèle vers le DTO
                Category_Id = model.Category_Id, // Transfère l'ID de la catégorie
                User_Id = model.User_Id, // Transfère l'ID de l'utilisateur
                Home_Id = model.Home_Id, // Transfère l'ID du domicile
                Amount = model.Amount, // Transfère le montant de la dépense
                Description = model.Description, // Transfère la description de la dépense
                Date_Expense = model.Date_Expense // Transfère la date de la dépense
            };
        }

        // Convertit un ExpenseDataDTO en Modèle Expense
        public static Expense ToModel(this ExpenseDataDTO expense)
        {
            return new Expense
            {
                Category_Id = expense.Category_Id, // Transfère l'ID de la catégorie du DTO vers le Modèle
                User_Id = expense.User_Id, // Transfère l'ID de l'utilisateur
                Home_Id = expense.Home_Id, // Transfère l'ID du domicile
                Amount = expense.Amount, // Transfère le montant de la dépense
                Description = expense.Description, // Transfère la description de la dépense
                Date_Expense = expense.Date_Expense // Transfère la date de la dépense
            };
        }
    }
}
