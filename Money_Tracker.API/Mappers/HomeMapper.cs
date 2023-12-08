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
        /// <param name="homeModel">Objet Home à mapper.</param>
        /// <returns>Objet HomeDTO mappé.</returns>
        public static HomeDTO ToDTO(this Home homeModel)
        {
            return new HomeDTO
            {
                Id = homeModel.Id,
                User_Id = homeModel.User_Id,
                Name_Home = homeModel.Name_Home,
            };
        }

        /// <summary>
        /// Convertit un objet Home en objet HomeFullDTO incluant les utilisateurs associés.
        /// </summary>
        /// <param name="homeModel">Objet Home à mapper.</param>
        /// <returns>Objet HomeFullDTO mappé.</returns>
        public static HomeFullDTO ToFullDTO(this Home homeModel)
        {
            return new HomeFullDTO
            {
                Id = homeModel.Id,
                User_Id = homeModel.User_Id,
                Name_Home = homeModel.Name_Home,
                Users = homeModel.Users.Select(u => u.ToHomeUserDTO())
            };
        }

        /// <summary>
        /// Convertit un objet HomeUser en objet HomeUserDTO représentant un utilisateur associé à une maison.
        /// </summary>
        /// <param name="homeModel">Objet HomeUser à mapper.</param>
        /// <returns>Objet HomeUserDTO mappé.</returns>
        public static HomeUserDTO ToHomeUserDTO(this HomeUser homeModel)
        {
            return new HomeUserDTO
            {
                User_Id = homeModel.User?.Id ?? 0,
                Home_Id = homeModel.Home_Id,
            };
        }
    }
}
