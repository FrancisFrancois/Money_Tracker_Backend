using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    // Classe HomeMapper : Contient des méthodes statiques pour mapper les maisons entre les modèles et les DTOs
    public static class HomeMapper
    {
        // Convertit un Modèle Home en HomeDTO
        public static HomeDTO ToDTO(this Home model)
        {
            return new HomeDTO
            {
                Id = model.Id, // Transfère l'ID du modèle Home vers HomeDTO
                User_Id = model.User_Id, // Transfère l'ID de l'utilisateur du modèle Home vers HomeDTO
                Name_Home = model.Name_Home // Transfère le nom du domicile du modèle Home vers HomeDTO
            };
        }

        // Convertit un Modèle Home en HomeFullDTO, incluant les utilisateurs associés
        public static HomeFullDTO ToFullDTO(this Home model)
        {
            return new HomeFullDTO
            {
                Id = model.Id, // Transfère l'ID du modèle Home vers HomeFullDTO
                User_Id = model.User_Id, // Transfère l'ID de l'utilisateur du modèle Home vers HomeFullDTO
                Name_Home = model.Name_Home, // Transfère le nom du domicile
                Users = model.Users.Select(u => u.ToHomeUserDTO()) // Convertit les utilisateurs associés en HomeUserDTO
            };
        }

        // Convertit un Modèle HomeUser en HomeUserDTO
        public static HomeUserDTO ToHomeUserDTO(this HomeUser model)
        {
            return new HomeUserDTO
            {
                User_Id = model.User?.Id ?? 0, // Transfère l'ID de l'utilisateur (vérifie si l'utilisateur n'est pas null)
                Home_Id = model.Home_Id // Transfère l'ID du domicile associé
            };
        }

        // Convertit un HomeDataDTO en Modèle Home
        public static Home ToModel(this HomeDataDTO home)
        {
            return new Home
            {
                Id = 0, // L'ID est défini à 0, généralement auto-généré dans la base de données
                Name_Home = home.Name_Home, // Transfère le nom du domicile du DTO vers le modèle
                User_Id = home.User_Id // Transfère l'ID de l'utilisateur du DTO vers le modèle
            };
        }
    }
}
