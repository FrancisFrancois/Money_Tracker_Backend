using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    /// <summary>
    /// Représente une catégorie.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la catégorie.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la catégorie.
        /// </summary>
        public string Category_Name { get; set; } = string.Empty;
    }
}
