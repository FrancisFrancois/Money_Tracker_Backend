using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Mappers.Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;


namespace Money_Tracker.BLL.Services
{
    /// <summary>
    /// Service responsable de la gestion des opérations liées aux maisons (Home).
    /// </summary>
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _HomeRepository;
        private readonly IUserRepository _UserRepository;

        /// <summary>
        /// Initialise une nouvelle instance de la classe HomeService.
        /// </summary>
        /// <param name="homeRepository">Le repository des maisons.</param>
        /// <param name="userRepository">Le repository des utilisateurs.</param>
        public HomeService(IHomeRepository homeRepository, IUserRepository userRepository)
        {
            _HomeRepository = homeRepository;
            _UserRepository = userRepository;
        }

        // Méthodes pour gérer les opérations CRUD des maisons

        /// <summary>
        /// Récupère toutes les maisons et les renvoie sous forme de modèles Home.
        /// </summary>
        /// <returns>Une collection de modèles Home.</returns>
        public IEnumerable<Home> GetAll()
        {
            return _HomeRepository.GetAll().Select(h => h.ToModel());
        }

        /// <summary>
        /// Récupère une maison par son identifiant avec les utilisateurs associés et renvoie un modèle Home.
        /// </summary>
        /// <param name="id">L'identifiant de la maison à récupérer.</param>
        /// <returns>Un modèle Home avec les utilisateurs associés.</returns>
        public Home? GetById(int id)
        {
            Home? home = _HomeRepository.GetById(id)?.ToModel();

            if (home is not null)
            {
                IEnumerable<HomeUser> homeUsers = _HomeRepository.GetUsers(home.Id)
                    .Join(_UserRepository.GetAll(), hs => hs.User_Id, u => u.Id, (hs, u) =>
                    {
                        HomeUser hsModel = hs.ToModel();
                        hsModel.User = u.ToModel();
                        return hsModel;
                    });
                home.Users = homeUsers;
            }

            return home;
        }

        /// <summary>
        /// Crée une nouvelle maison à partir du modèle Home spécifié.
        /// </summary>
        /// <param name="home">Le modèle Home à créer.</param>
        /// <returns>Un modèle Home nouvellement créé.</returns>
        public Home Create(Home home)
        {
            return _HomeRepository.Create(home.ToEntity()).ToModel();
        }

        /// <summary>
        /// Met à jour une maison avec l'identifiant spécifié en utilisant le modèle Home spécifié.
        /// </summary>
        /// <param name="id">L'identifiant de la maison à mettre à jour.</param>
        /// <param name="home">Le modèle Home mis à jour.</param>
        /// <returns>True si la mise à jour a réussi, sinon False.</returns>
        public bool Update(int id, Home home)
        {
            bool updated = _HomeRepository.Update(id, home.ToEntity());
            if (!updated)
            {
                throw new NotFoundException("Home not found");
            }
            return updated;
        }

        /// <summary>
        /// Supprime une maison avec l'identifiant spécifié.
        /// </summary>
        /// <param name="id">L'identifiant de la maison à supprimer.</param>
        /// <returns>True si la suppression a réussi, sinon False.</returns>
        public bool Delete(int id)
        {
            bool deleted = _HomeRepository.Delete(id);
            if (!deleted)
            {
                throw new NotFoundException("Home not found");
            }

            return deleted;
        }
    }
}
