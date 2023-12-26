using Money_Tracker.API.DTOs;
using Money_Tracker.BLL.Models;
using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.API.Mappers
{
    public static class HomeUserMapper
    {
        public static HomeUserDTO ToDTO(this HomeUser model)
        {
            return new HomeUserDTO
            {
                User_Id = model.User?.Id ?? 0,  // Assurez-vous que la propriété dans votre DTO est UserId, pas User_Id
                Home_Id = model.Home_Id
            };
        }

        public static HomeUser ToModel(this HomeUserDTO homeUser)
        {
            return new HomeUser
            {
                User = new User { Id = homeUser.User_Id }, // Utilisez la propriété UserId du DTO
                Home_Id = homeUser.Home_Id,
            };
        }
    }
}
