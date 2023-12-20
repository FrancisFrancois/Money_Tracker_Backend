using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    // Classe UserMapper : Contient des méthodes statiques pour mapper les utilisateurs entre les modèles et les DTOs
    public static class UserMapper
    {
        // Convertit un Modèle User en UserDTO
        public static UserDTO ToDTO(this User model)
        {
            return new UserDTO
            {
                Id = model.Id, // Transfère l'ID du modèle vers le DTO
                Lastname = model.Lastname, // Transfère le nom de famille du modèle vers le DTO
                Firstname = model.Firstname, // Transfère le prénom du modèle vers le DTO
                Pseudo = model.Pseudo, // Transfère le pseudo du modèle vers le DTO
                Email = model.Email, // Transfère l'email du modèle vers le DTO
            };
        }

        // Convertit un UserDataDTO en Modèle User
        public static User ToModel(this UserDataDTO user)
        {
            return new User
            {
                Id = user.Id, // Transfère l'ID du DTO vers le modèle 
                Lastname = user.Lastname, // Transfère le nom de famille du DTO vers le modèle
                Firstname = user.Firstname, // Transfère le prénom du DTO vers le modèle
                Pseudo = user.Pseudo, // Transfère le pseudo du DTO vers le modèle
                Email = user.Email, // Transfère l'email du DTO vers le modèle
                Password = user.Password, // Transfère le mot de passe du DTO vers le modèle
                Roles = user.Roles, // Transfère les rôles du DTO vers le modèle
            };
        }

        // Convertit un RegisterDTO en Modèle User pour l'enregistrement d'un nouvel utilisateur
        public static User ToModel(this RegisterDTO user)
        {
            return new User
            {
                Lastname = user.Lastname, // Transfère le nom de famille du DTO vers le modèle
                Firstname = user.Firstname, // Transfère le prénom du DTO vers le modèle
                Pseudo = user.Pseudo, // Transfère le pseudo du DTO vers le modèle
                Email = user.Email, // Transfère l'email du DTO vers le modèle
                Password = user.Password, // Transfère le mot de passe du DTO vers le modèle
            };
        }
    }
}
