using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs
{
    // Classe CategoryDTO : Utilisée pour représenter les données des catégories dans les réponses API
    public class CategoryDTO
    {
        public int Id { get; set; }  // Identifiant unique de la catégorie
        public string Category_Name { get; set; } = string.Empty;  // Le nom de la catégorie
    }

    // Classe CategoryDataDTO : Utilisée pour capturer les données de la catégorie lors des requêtes de création ou de mise à jour
    public class CategoryDataDTO
    {
        
        [Required] // Champ obligatoire
        
        public string Category_Name { get; set; } = string.Empty;  // Le nom de la catégorie
    }
}
