using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;
using System.Security.Claims;

namespace Money_Tracker.BLL.Services
{
    // Classe UserService gérant les opérations liées aux utilisateurs
    public class UserService : IUserService
    {
        // Référence au repository utilisateur pour interagir avec la base de données
        private readonly IUserRepository _UserRepository;

        // Constructeur initialisant le repository utilisateur
        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        // Récupère la liste de tous les utilisateurs de la base de données
        public IEnumerable<User> GetAll()
        {
            // Convertit chaque entité utilisateur en modèle utilisateur
            return _UserRepository.GetAll().Select(u => u.ToModel());
        }

        // Récupère un utilisateur spécifique par son ID
        public User? GetById(int id)
        {
            // Cherche l'utilisateur par ID et le convertit en modèle si trouvé
            return _UserRepository.GetById(id)?.ToModel();
        }

        // Crée et enregistre un nouvel utilisateur
        public User Create(User user)
        {   

            // Hache le mot de passe avant de l'enregistrer
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Crée l'utilisateur dans la base de données et renvoie le modèle créé
            return _UserRepository.Create(user.ToEntity()).ToModel();
        }

        // Met à jour les informations d'un utilisateur existant
        public bool Update(int id, User user)
        {
            // Hash le mot de passe pour la mise à jour
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Tente de mettre à jour l'utilisateur et lève une exception si non trouvé
            bool updated = _UserRepository.Update(id, user.ToEntity());
            if (!updated)
            {
                throw new Exception("User Not Found");
            }
            return updated;
        }

        // Supprime un utilisateur de la base de données
        public bool Delete(int id)
        {
            // Vérifie d'abord si l'utilisateur est associé à une maison
            if (_UserRepository.isLivingInHouse(id))
            {
                throw new AlreadyLivingException("User is already attributed to the house");
            }

            // Tente de supprimer l'utilisateur et lève une exception si non trouvé
            bool deleted = _UserRepository.Delete(id);
            if (!deleted)
            {
                throw new Exception("User Not Found");
            }
            return deleted;
        }

        // Vérifie si l'email ou le pseudo est déjà utilisé
        public bool IsEmailOrPseudoExists(string email, string pseudo)
        {
            var userByEmail = _UserRepository.GetUserByEmail(email);
            var userByPseudo = _UserRepository.GetUserByPseudo(pseudo);

            return userByEmail != null || userByPseudo != null;
        }

        // Détermine si la chaîne donnée est un email
        private bool IsEmail(string emailOrPseudo)
        {
            return emailOrPseudo.Contains("@");
        }

        // Valide les informations de connexion de l'utilisateur
        public bool ValidateLogin(string emailOrPseudo, string password)
        {
            User? user;
            if (IsEmail(emailOrPseudo))
            {
                user = _UserRepository.GetUserByEmail(emailOrPseudo)?.ToModel();
            }
            else
            {
                user = _UserRepository.GetUserByPseudo(emailOrPseudo)?.ToModel();
            }

            // Vérifie si l'utilisateur existe et que le mot de passe correspond
            if (user != null)
            {
                return VerifyPassword(password, user.Password);
            }

            return false;
        }

        // Vérifie si le mot de passe donné correspond au hash stocké
        private bool VerifyPassword(string password, string storedHash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, storedHash);
            }
            catch
            {
                return false;
            }
        }

        // Récupère le rôle d'un utilisateur
        public string GetUserRole(string emailOrPseudo)
        {
            // Essayer de récupérer l'utilisateur par email. Si non trouvé, essayer par pseudo.
            // Cette approche permet de gérer les utilisateurs qui se connectent soit avec leur email soit avec leur pseudo.
            User? user = _UserRepository.GetUserByEmail(emailOrPseudo)?.ToModel() ?? _UserRepository.GetUserByPseudo(emailOrPseudo)?.ToModel();

            // Vérifie si l'utilisateur a été trouvé.
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Retourner le rôle de l'utilisateur.
            return user.Roles;
        }

        // Enregistre un nouvel utilisateur
        public User Register(User user)
        {
            // Hache le mot de passe avant de l'enregistrer
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Attribuer automatiquement le rôle 'Manager' lors de l'inscription
            user.Roles = "MANAGER";

            // Crée l'utilisateur dans la base de données et renvoie le modèle créé
            return _UserRepository.Create(user.ToEntity()).ToModel();
        }

        public int? GetUserId(string emailOrPseudo)
        {
            User? user;
            if (IsEmail(emailOrPseudo))
            {
                user = _UserRepository.GetUserByEmail(emailOrPseudo)?.ToModel();
            }
            else
            {
                user = _UserRepository.GetUserByPseudo(emailOrPseudo)?.ToModel();
            }

            return user?.Id;
        }



    }
}
