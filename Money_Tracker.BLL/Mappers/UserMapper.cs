using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    public static class UserMapper
    {
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
