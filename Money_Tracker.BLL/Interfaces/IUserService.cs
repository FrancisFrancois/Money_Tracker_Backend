using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{

    public interface IUserService : ICrudService<int, User>
    {
        bool IsEmailOrPseudoExists(string email, string pseudo);
        bool ValidateLogin(string emailOrPseudo, string password);
    }
}