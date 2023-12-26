using Money_Tracker.DAL.Entities; 
using Money_Tracker.Tools.Interfaces; 

namespace Money_Tracker.DAL.Interfaces
{
    // Interface IHomeRepository : Définit les opérations spécifiques pour la gestion des maisons
    // Hérite de l'interface générique ICrud pour fournir des opérations CRUD standard
    public interface IHomeRepository : ICrud<int, Home>
    {
        // Méthode pour récupérer les utilisateurs d'une maison spécifique par leur ID utilisateur
        IEnumerable<HomeUser> GetUsers(int userId);

        // Méthode pour ajouter une nouvelle association HomeUser dans la table de jointure.
        HomeUser AddUserToHome(HomeUser homeUser);

        // Méthode pour supprimer une association HomeUser existante de la table de jointure.
        bool RemoveUserFromHome(int homeId, int userId);
    }
}
