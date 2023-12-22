using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IHomeService : Définit les opérations pour la gestion des maisons
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface IUserService : ICrudService<int, User>
    {
        bool IsEmailOrPseudoExists(string email, string pseudo);
        // Méthode pour vérifier si un email ou pseudo existe déjà.

        bool ValidateLogin(string emailOrPseudo, string password);
        // Méthode pour valider les informations de connexion d'un utilisateur.
        // Vérifie si le email/pseudo et mot de passe correspond à un utilisateur enregistré.

        string GetUserRole(string emailOrPseudo);
        // Récupère le rôle d'un utilisateur à partir de son email ou pseudo.
        // Cette méthode est importante pour déterminer les autorisations et accès de l'utilisateur dans l'application.

        User Register(User user);
        // Enregistre un nouvel utilisateur dans le système.
        // Cette méthode est utilisée lors de la création d'un nouveau compte utilisateur.
        // Elle s'occupe de la sécurisation du mot de passe et de l'attribution d'un rôle par défaut.
    }
}