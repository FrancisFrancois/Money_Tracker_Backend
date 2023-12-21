using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    // Classe Expense : Représente une dépense
    public class Expense
    {
        public int Id { get; set; } // Identifiant unique de la dépense.

        public int Category_Id { get; set; } // Identifiant de la catégorie associée à la dépense.

        public int User_Id { get; set; } // Identifiant de l'utilisateur ayant effectué la dépense.

        public int Home_Id { get; set; } // Identifiant du domicile associé à la dépense.

        public double Amount { get; set; } // Montant de la dépense.

        public string Description { get; set; } = string.Empty; // Description de la dépense.

        public DateTime Date_Expense { get; set; } // Date à laquelle la dépense a été effectuée.
    }
}