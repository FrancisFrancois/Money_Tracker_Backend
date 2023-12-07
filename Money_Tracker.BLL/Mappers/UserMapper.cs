using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    /// <summary>
    /// Classe statique contenant des méthodes d'extension pour la conversion entre entités utilisateur et modèles utilisateur.
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Convertit une entité utilisateur en modèle utilisateur.
        /// </summary>
        /// <param name="entity">Entité utilisateur à convertir.</param>
        /// <returns>Modèle utilisateur correspondant à l'entité donnée.</returns>
        public static Models.User ToModel(this Entities.User entity)
        {
            return new Models.User
            {
                Id = entity.Id,
                Name = entity.Name,
                Firstname = entity.Firstname,
                Pseudo = entity.Pseudo,
                Email = entity.Email,
                Password = entity.Password,
                Roles = entity.Roles,
            };
        }

        /// <summary>
        /// Convertit un modèle utilisateur en entité utilisateur.
        /// </summary>
        /// <param name="model">Modèle utilisateur à convertir.</param>
        /// <returns>Entité utilisateur correspondant au modèle donné.</returns>
        public static Entities.User ToEntity(this Models.User model)
        {
            return new Entities.User
            {
                Id = model.Id,
                Name = model.Name,
                Firstname = model.Firstname,
                Pseudo = model.Pseudo,
                Email = model.Email,
                Password = model.Password,
                Roles = model.Roles,
            };
        }
    }
}