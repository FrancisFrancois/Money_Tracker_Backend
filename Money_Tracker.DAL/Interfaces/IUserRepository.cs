using Money_Tracker.DAL.Entities; 
using Money_Tracker.Tools.Interfaces;  
namespace Money_Tracker.DAL.Interfaces
{
    // Interface IUserRepository : Définit les opérations spécifiques pour la gestion des utilisateurs
    // Hérite de l'interface générique ICrud pour fournir des opérations CRUD standard
    public interface IUserRepository : ICrud<int, User>
    {
        // Méthode pour vérifier si un utilisateur vit dans une maison spécifique en utilisant son ID
        public bool isLivingInHouse(int id);

        // Méthode pour récupérer un utilisateur par son adresse e-mail
        User GetUserByEmail(string email);

        // Méthode pour récupérer un utilisateur par son pseudo 
        User GetUserByPseudo(string pseudo);
    }
}
