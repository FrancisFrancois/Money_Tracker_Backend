using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IHomeService : Définit les opérations pour la gestion des maisons
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface IUserService : ICrudService<int, User>
    {
        // Méthode pour vérifier si un email ou pseudo existe déjà.
        bool IsEmailOrPseudoExists(string email, string pseudo);

        // Méthode pour valider les informations de connexion d'un utilisateur.
        // Vérifie si le email/pseudo et mot de passe correspond à un utilisateur enregistré.
        bool ValidateLogin(string emailOrPseudo, string password);
    }
}