using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;


namespace Money_Tracker.BLL.Services
{
    // Classe HomeService : Implémente les opérations de gestion des maisons spécifiées dans l'interface IHomeService
    public class HomeService : IHomeService
    {
        // Référence au repository des catégories et des utilisateurs pour l'interaction avec la base de données
        private readonly IHomeRepository _HomeRepository;
        private readonly IUserRepository _UserRepository;

        // Constructeur pour injecter les dépendances des repositories
        public HomeService(IHomeRepository homeRepository, IUserRepository userRepository)
        {
            _HomeRepository = homeRepository;
            _UserRepository = userRepository;
        }

        // Récupère tous les domiciles et les convertit en modèles
        public IEnumerable<Home> GetAll()
        {
            // Utilise le repository pour récupérer tous les domiciles et les convertit en modèles Home
            return _HomeRepository.GetAll().Select(h => h.ToModel());
        }

        // Récupère un domicile spécifique par son ID et le convertir en modèle
        public Home? GetById(int id)
        {
            // Utilise le repository pour trouver une maison par son ID et la convertir en modèle, renvoie null si non trouvée
            Home? home = _HomeRepository.GetById(id)?.ToModel();

            // Si le domicile est trouvé, récupère et assigne les utilisateurs associés au domicile
            if (home is not null)
            {
                // Utilise _HomeRepository pour obtenir les utilisateurs associés au domicile
                // et on les joint avec les données des utilisateurs obtenues par _UserRepository
                IEnumerable<HomeUser> homeUsers = _HomeRepository.GetUsers(home.Id)
                    .Join(_UserRepository.GetAll(), hs => hs.User_Id, u => u.Id, (hs, u) =>
                    {
                        HomeUser hsModel = hs.ToModel();
                        hsModel.User = u.ToModel();
                        return hsModel;
                    });
                // Assignation des utilisateurs associés au modèle Home
                home.Users = homeUsers;
            }

            return home;
        }

        // Crée un nouveau domicile
        public Home Create(Home home)
        {
            // Utilise le repository pour créer une nouvelle maison dans la base de données et renvoie le modèle créé
            return _HomeRepository.Create(home.ToEntity()).ToModel();
        }

        // Met à jour un domicile existant
        public bool Update(int id, Home home)
        {
            // Tente de mettre à jour le domicile et renvoie un booléen indiquant si la mise à jour a réussi
            bool updated = _HomeRepository.Update(id, home.ToEntity());
            if (!updated)
            {
                // Exception indique que l'opération de suppression a échoué 
                throw new NotFoundException("Home not found");
            }
            return updated;
        }

        // Supprime un domicile par son ID
        public bool Delete(int id)
        {
            // Tente de supprimer le domicile et renvoie un booléen indiquant si la suppression a réussi
            bool deleted = _HomeRepository.Delete(id);
            if (!deleted)
            {
                // Si la suppression échoue, une exception est levée
                throw new NotFoundException("Home not found");
            }

            return deleted;
        }
    }
}
