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

            };
        }
    }
}
