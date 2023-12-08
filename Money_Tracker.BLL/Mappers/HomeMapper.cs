using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;
using System.Runtime.CompilerServices;

namespace Money_Tracker.BLL.Mappers
{
    /// <summary>
    /// Classe statique utilisée pour mapper les objets entre les entités et les modèles Home.
    /// </summary>
    public static class HomeMapper
    {
        /// <summary>
        /// Convertit une entité Home en un modèle Home.
        /// </summary>
        /// <param name="entity">L'entité Home à convertir.</param>
        /// <returns>Un objet de type Models.Home.</returns>
        public static Models.Home ToModel(this Entities.Home entity)
        {
            return new Models.Home
            {
                Id = entity.Id,
                User_Id = entity.User_Id,
                Name_Home = entity.Name_Home,
            };
        }

        /// <summary>
        /// Convertit un modèle Home en une entité Home.
        /// </summary>
        /// <param name="model">Le modèle Home à convertir.</param>
        /// <returns>Un objet de type Entities.Home.</returns>
        public static Entities.Home ToEntity(this Models.Home model)
        {
            return new Entities.Home
            {
                Id = model.Id,
                User_Id = model.User_Id,
                Name_Home = model.Name_Home,
            };
        }
    }
}