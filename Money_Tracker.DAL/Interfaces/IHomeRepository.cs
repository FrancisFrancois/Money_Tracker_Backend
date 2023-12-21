using Money_Tracker.DAL.Entities; 
using Money_Tracker.DAL.Entities.Money_Tracker.DAL.Entities;
using Money_Tracker.Tools.Interfaces; 

namespace Money_Tracker.DAL.Interfaces
{
    // Interface IHomeRepository : Définit les opérations spécifiques pour la gestion des maisons
    // Hérite de l'interface générique ICrud pour fournir des opérations CRUD standard
    public interface IHomeRepository : ICrud<int, Home>
    {
        // Méthode pour récupérer les utilisateurs d'une maison spécifique par leur ID utilisateur
        IEnumerable<HomeUser> GetUsers(int userId);
    }
}
