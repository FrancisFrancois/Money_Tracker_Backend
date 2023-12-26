using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.API.Mappers
{
    // Classe HomeUserMapper : Contient des méthodes statiques pour mapper les utilisateurs appartenant à une maison entre les modèles et les DTOs
    public static class HomeUserMapper
    {
        // Convertit un Modèle HomeUser en HomeUserDTO
        public static HomeUserDTO ToDTO(this HomeUser model)
        {
            return new HomeUserDTO
            {
                User_Id = model.User?.Id ?? 0,  // Transfère l'ID de l'utilisateur du modèle HomeUser vers HomeUserDTO
                Home_Id = model.Home_Id  // Transfère l'ID de la maison du modèle HomeUser vers HomeUserDTO
            };
        }

        // Convertit un HomeUserDTO en Modèle HomeUser
        public static HomeUser ToModel(this HomeUserDTO homeUser)
        {
            return new HomeUser
            {
                User = new User { Id = homeUser.User_Id }, // Transfère l'ID de l'utilisateur du DTO vers le modèle
                Home_Id = homeUser.Home_Id, // Transfère l'ID de la maison du DTO vers le modèle
            };
        }
    }
}
