using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers
{
    /// <summary>
    /// Classe statique contenant des méthodes d'extension pour la conversion entre modèles User et UserDTO.
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Convertit un modèle User en UserDTO.
        /// </summary>
        /// <param name="model">Modèle User à convertir.</param>
        /// <returns>UserDTO correspondant au modèle donné.</returns>
        public static UserDTO ToDTO(this User model)
        {
            return new UserDTO
            {
                Id = model.Id,
                Lastname = model.Lastname,
                Firstname = model.Firstname,
                Pseudo = model.Pseudo,
                Email = model.Email,
                Password = model.Password,
                Roles = model.Roles,
            };
        }

        /// <summary>
        /// Convertit un UserDataDTO en modèle User.
        /// </summary>
        /// <param name="user">UserDataDTO à convertir.</param>
        /// <returns>Modèle User correspondant au UserDataDTO donné.</returns>
        public static User ToModel(this UserDataDTO user)
        {
            return new User
            {
                Id = user.Id,
                Lastname = user.Lastname,
                Firstname = user.Firstname,
                Pseudo = user.Pseudo,
                Email = user.Email,
                Password = user.Password,
                Roles = user.Roles,
            };
        }
    }
}