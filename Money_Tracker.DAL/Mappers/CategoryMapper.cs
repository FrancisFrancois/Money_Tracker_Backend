using Money_Tracker.DAL.Entities;
using System.Data;

namespace Money_Tracker.DAL.Mappers
{
    // Classe CategoryMapper : Contient des méthodes pour mapper des enregistrements de base de données vers des objets
    public class CategoryMapper
    {
        // Méthode pour mapper un enregistrement de base de données (IDataRecord) vers un objet Category.
        public static Category Mapper(IDataRecord record)
        {
            // Crée et renvoie un nouvel objet Category avec les données extraites de l'enregistrement IDataRecord.
            return new Category
            {
                // Extraction et affectation de l'identifiant de la catégorie.
                Id = (int)record["Category_Id"],

                // Extraction et affectation du nom de la catégorie.
                Category_Name = (string)record["Category_Name"]
            };
        }
    }
}
