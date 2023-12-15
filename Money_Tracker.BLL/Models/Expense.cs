using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    /// <summary>
    /// Représente une dépense.
    /// </summary>
    public class Expense
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la dépense.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de la catégorie de la dépense.
        /// </summary>
        public int Category_Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur lié à la dépense.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant du domicile lié à la dépense.
        /// </summary>
        public int Home_Id { get; set; }

        /// <summary>
        /// Obtient ou définit le montant de la dépense.
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Obtient ou définit la description de la dépense.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit la date de la dépense.
        /// </summary>
        public DateTime Date_Expense { get; set; }
    }
}