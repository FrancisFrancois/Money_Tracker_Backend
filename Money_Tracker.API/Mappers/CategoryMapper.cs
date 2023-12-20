using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    // Classe CategoryMapper : Contient des méthodes statiques pour mapper les catégories entre les Modèles et les DTOs
    public static class CategoryMapper
    {
        // Convertit un modèle Category en CategoryDTO
        public static CategoryDTO ToDTO(this Category model)
        {
            return new CategoryDTO
            {
                Id = model.Id, // Transfère l'ID du Modèle vers le DTO
                Category_Name = model.Category_Name // Transfère le nom de la catégorie du modèle vers le DTO
            };
        }

        // Convertit un CategoryDataDTO en Modèle Category
        public static Category ToModel(this CategoryDataDTO category)
        {
            return new Category
            {
                Category_Name = category.Category_Name // Transfère le nom de la catégorie du DTO vers le Modèle

                // Note : L'ID n'est pas transféré car il est généralement auto-généré dans la base de données pour les nouvelles catégories
            };
        }
    }
}
