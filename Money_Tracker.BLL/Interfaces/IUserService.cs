using Money_Tracker.BLL.Models;

namespace Money_Tracker.BLL.Interfaces
{
    /// <summary>
    /// Interface définissant les opérations disponibles pour la gestion des utilisateurs.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <returns>Une collection de modèles d'utilisateurs.</returns>
        public IEnumerable<User> GetAll();

        /// <summary>
        /// Récupère un utilisateur par son ID.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à récupérer.</param>
        /// <returns>Le modèle de l'utilisateur correspondant à l'ID donné.</returns>
        public User? GetById(int id);

        /// <summary>
        /// Insère un nouvel utilisateur.
        /// </summary>
        /// <param name="user">Le modèle de l'utilisateur à insérer.</param>
        /// <returns>Le modèle de l'utilisateur inséré.</returns>
        public User Insert(User user);

        /// <summary>
        /// Met à jour les informations d'un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à mettre à jour.</param>
        /// <param name="user">Nouvelles informations de l'utilisateur.</param>
        /// <returns>Booléen indiquant si la mise à jour a réussi.</returns>
        public bool Update(int id, User user);

        /// <summary>
        /// Supprime un utilisateur.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer.</param>
        /// <returns>Booléen indiquant si la suppression a réussi.</returns>
        public bool Delete(int id);
    }
}