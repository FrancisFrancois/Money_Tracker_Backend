using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    // Classe Category : Représente une catégorie de dépense
    public class Category
    {
        public int Id { get; set; } // Identifiant unique de la catégorie

        public string Category_Name { get; set; } = string.Empty; // Nom de la catégorie. 

    }
}

