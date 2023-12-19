using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;

namespace Money_Tracker.API.Mappers;

    public static class UserMapper
    {

        public static UserDTO ToDTO(this User model)
        {
            return new UserDTO
            {
                Id = model.Id,
                Lastname = model.Lastname,
                Firstname = model.Firstname,
                Pseudo = model.Pseudo,
                Email = model.Email,
            };
        }

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

        public static User ToModel(this RegisterDTO user)
        {
            return new User
            {
                Lastname = user.Lastname,
                Firstname = user.Firstname,
                Pseudo = user.Pseudo,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }
