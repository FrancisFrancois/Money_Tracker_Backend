using Microsoft.IdentityModel.Tokens;
using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Money_Tracker.BLL.Services
{
    /// <summary>
    /// Service pour la gestion des utilisateurs.
    /// Implémente les opérations définies dans l'interface IUserService.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <returns>Une collection de modèles d'utilisateurs.</returns>
        public IEnumerable<User> GetAll()
        {
            return _UserRepository.GetAll().Select(u => u.ToModel());
        }

        /// <summary>
        /// Récupère un utilisateur par son ID.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à récupérer.</param>
        /// <returns>Le modèle de l'utilisateur correspondant à l'ID donné.</returns>
        public User? GetById(int id)
        {
            return _UserRepository.GetById(id)?.ToModel();
        }

        /// <summary>
        /// Insère un nouvel utilisateur.
        /// </summary>
        /// <param name="user">Le modèle de l'utilisateur à insérer.</param>
        /// <returns>Le modèle de l'utilisateur inséré.</returns>
        public User Create(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            return _UserRepository.Create(user.ToEntity()).ToModel();
        }

        /// <summary>
        /// Met à jour les informations d'un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à mettre à jour.</param>
        /// <param name="user">Nouvelles informations de l'utilisateur.</param>
        /// <returns>Booléen indiquant si la mise à jour a réussi.</returns>
        public bool Update(int id, User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            bool updated = _UserRepository.Update(id, user.ToEntity());
            if (!updated)
            {
                throw new Exception("User Not Found");
            }
            return updated;
        }

        /// <summary>
        /// Supprime un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer.</param>
        /// <returns>Booléen indiquant si la suppression a réussi.</returns>
        public bool Delete(int id)
        {
            if (_UserRepository.isLivingInHouse(id))
            {
                throw new AlreadyLivingException("User is already attributed to the house");
            }

            bool deleted = _UserRepository.Delete(id);
            if (!deleted)
            {
                throw new Exception("User Not Found");
            }
            return deleted;
        }

        public bool IsEmailOrPseudoExists(string email, string pseudo)
        {
            var userByEmail = _UserRepository.GetUserByEmail(email);
            var userByPseudo = _UserRepository.GetUserByPseudo(pseudo);

            return userByEmail != null || userByPseudo != null;
        }

        private bool IsEmail(string emailOrPseudo)
        {
            return emailOrPseudo.Contains("@");
        }

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

            if (user != null)
            {
                return VerifyPassword(password, user.Password);
            }

            return false;
        }


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

    }
}