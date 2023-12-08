using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    namespace Money_Tracker.BLL.Mappers
    {
        public static class HomeUserMapper
        {
            /// <summary>
            /// Convertit une entité HomeUser en un modèle HomeUser.
            /// </summary>
            /// <param name="entity">L'entité HomeUser à convertir.</param>
            /// <returns>Un objet de type Models.HomeUser.</returns>
            public static Models.HomeUser ToModel(this Entities.HomeUser entity)
            {
                return new Models.HomeUser
                {
                    User_Id = entity.User_Id,
                    Home_Id = entity.Home_Id                
                };
            }

            /// <summary>
            /// Convertit un modèle HomeUser en une entité HomeUser.
            /// </summary>
            /// <param name="model">Le modèle HomeUser à convertir.</param>
            /// <returns>Un objet de type Entities.HomeUser.</returns>
            public static Entities.HomeUser ToEntity(this Models.HomeUser model)
            {
                return new Entities.HomeUser
                {
                    User_Id = model.User_Id,
                    Home_Id = model.Home_Id
                };
            }
        }
    }
}