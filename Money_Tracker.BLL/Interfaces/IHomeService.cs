using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IHomeService : Définit les opérations pour la gestion des maisons
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface IHomeService : ICrudService<int, Home>
    {
        HomeUser AddUserToHome(HomeUser homeUser);
        bool RemoveUserFromHome(int homeId, int userId);
    }
}
