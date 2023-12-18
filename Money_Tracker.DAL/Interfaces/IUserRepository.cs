using Money_Tracker.DAL.Entities;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.DAL.Interfaces
{
    public interface IUserRepository : ICrud<int, User>
    {
        public bool isLivingInHouse(int id);
        User GetUserByEmail(string email);
        User GetUserByPseudo(string pseudo);

    }
}
