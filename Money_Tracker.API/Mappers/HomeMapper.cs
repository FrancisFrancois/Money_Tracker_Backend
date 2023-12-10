using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    /// <summary>
    /// Classe contenant des méthodes pour mapper les objets Home et HomeUser vers leurs DTO correspondants.
    /// </summary>
    public static class HomeMapper
    {
        /// <summary>
        /// Convertit un objet Home en objet HomeDTO.
        /// </summary>
        /// <param name="model">Objet Home à mapper.</param>
        /// <returns>Objet HomeDTO mappé.</returns>
        public static HomeDTO ToDTO(this Home model)
        {
            return new HomeDTO
            {
                Id = model.Id,
                User_Id = model.User_Id,
                Name_Home = model.Name_Home,
            };
        }

        /// <summary>
        /// Convertit un objet Home en objet HomeFullDTO incluant les utilisateurs associés.
        /// </summary>
        /// <param name="model">Objet Home à mapper.</param>
        /// <returns>Objet HomeFullDTO mappé.</returns>
        public static HomeFullDTO ToFullDTO(this Home model)
        {
            return new HomeFullDTO
            {
                Id = model.Id,
                User_Id = model.User_Id,
                Name_Home = model.Name_Home,
                Users = model.Users.Select(u => u.ToHomeUserDTO())
            };
        }

        /// <summary>
        /// Convertit un objet HomeUser en objet HomeUserDTO représentant un utilisateur associé à une maison.
        /// </summary>
        /// <param name="model">Objet HomeUser à mapper.</param>
        /// <returns>Objet HomeUserDTO mappé.</returns>
        public static HomeUserDTO ToHomeUserDTO(this HomeUser model)
        {
            return new HomeUserDTO
            {
                User_Id = model.User?.Id ?? 0,
                Home_Id = model.Home_Id,
            };
        }

        /// <summary>
        /// Convertit un objet HomeDTO en un objet Home.
        /// </summary>
        /// <param name="home">L'objet HomeDTO à convertir.</param>
        /// <returns>Un nouvel objet Home avec les valeurs de l'objet HomeDTO.</returns>
        public static Home ToModel(this HomeDataDTO home)
        {
            return new Home
            {
                Id = 0,
                Name_Home = home.Name_Home,
            };
        }
    }
}

