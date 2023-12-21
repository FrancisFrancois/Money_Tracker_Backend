using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    // Classe CategoryMapper : Fournit des méthodes statiques pour mapper entre les modèles de la BLL et les entités de la DAL.
    public static class CategoryMapper
    {
        // Convertit une entité Category (DAL) en un modèle Category (BLL).
        public static Models.Category ToModel(this Entities.Category entity)
        {
            return new Models.Category
            {
                Id = entity.Id, // Mappe l'identifiant de l'entité vers le modèle.
                Category_Name = entity.Category_Name // Mappe le nom de la catégorie de l'entité vers le modèle.
            };
        }

        // Convertit un modèle Category (BLL) en une entité Category (DAL).
        public static Entities.Category ToEntity(this Models.Category model)
        {
            return new Entities.Category
            {
                Id = model.Id, // Mappe l'identifiant du modèle vers l'entité.
                Category_Name = model.Category_Name // Mappe le nom de la catégorie du modèle vers l'entité.
            };
        }
    }
}
