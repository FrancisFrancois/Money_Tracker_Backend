using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs
{
    // Classe ExpenseDTO : Utilisée pour représenter les données des dépenses dans les réponses API
    public class ExpenseDTO
    {
        public int Id { get; set; } // Identifiant unique de la dépense
        public int Category_Id { get; set; } // Identifiant de la catégorie de la dépense
        public int User_Id { get; set; } // Identifiant de l'utilisateur associé à la dépense
        public int Home_Id { get; set; } // Identifiant du domicile associé à la dépense
        public double Amount { get; set; } // Montant de la dépense
        public string Description { get; set; } = string.Empty; // Description de la dépense
        public DateTime Date_Expense { get; set; } // Date de la dépense
    }

    // Classe ExpenseDataDTO : Utilisée pour capturer les données de la dépense lors des requêtes de création ou de mise à jour 
    public class ExpenseDataDTO
    {
        [Required] // Champ obligatoire
        public int Category_Id { get; set; } // Identifiant de la catégorie

        [Required] // Champ obligatoire
        public int User_Id { get; set; } // Identifiant de l'utilisateur

        [Required] // Champ obligatoire
        public int Home_Id { get; set; } // Identifiant du domicile

        [Required] // Champ obligatoire
        public double Amount { get; set; } // Montant de la dépense

        public string Description { get; set; } = string.Empty; // Description optionnelle de la dépense

        [Required] // Champ obligatoire
        [DataType(DataType.Date)] // Spécifie que le champ est une date
        public DateTime Date_Expense { get; set; } // Date de la dépense
    }
}
