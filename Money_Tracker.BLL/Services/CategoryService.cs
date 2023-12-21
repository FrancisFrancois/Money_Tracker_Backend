using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Mappers;
using Money_Tracker.BLL.Models;
using Money_Tracker.DAL.Interfaces;


namespace Money_Tracker.BLL.Services
{
    // Classe CategoryService : Implémente les opérations de gestion des catégories spécifiées dans l'interface ICategoryService
    public class CategoryService : ICategoryService
    {
        // Référence au repository de catégories pour l'interaction avec la base de données
        private readonly ICategoryRepository _CategoryRepository;

        // Constructeur pour injecter la dépendance du repository de catégories
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _CategoryRepository = categoryRepository;
        }

        // Récupère toutes les catégories et les convertit en modèles
        public IEnumerable<Category> GetAll()
        {
            // Utilise le repository pour récupérer toutes les catégories et les convertir en modèles
            return _CategoryRepository.GetAll().Select(c => c.ToModel());
        }

        // Récupère une catégorie spécifique par son ID et la convertit en modèle
        public Category? GetById(int id)
        {
            // Utilise le repository pour trouver une catégorie par son ID et la convertir en modèle, renvoie null si non trouvée
            return _CategoryRepository.GetById(id)?.ToModel();
        }

        // Crée une nouvelle catégorie
        public Category Create(Category category)
        {
            // Utilise le repository pour créer une nouvelle catégorie dans la base de données et renvoie le modèle créé
            return _CategoryRepository.Create(category.ToEntity()).ToModel();
        }

        // Met à jour une catégorie existante
        public bool Update(int id, Category category)
        {
            // Tente de mettre à jour la catégorie et renvoie un booléen indiquant si la mise à jour a réussi
            bool updated = _CategoryRepository.Update(id, category.ToEntity());
            if (!updated)
            {
                // Exception indique que l'opération de suppression a échoué 
                throw new NotFoundException("Category not found");
            }
            return updated;
        }

        // Supprime une catégorie par son ID
        public bool Delete(int id)
        {
            // Tente de mettre à jour la catégorie et renvoie un booléen indiquant si la suppression a réussi
            bool deleted = _CategoryRepository.Delete(id);
            if (!deleted)
            {
                // Exception indique que l'opération de suppression a échoué 
                throw new NotFoundException("Category not found");
            }
            return deleted;
        }
    }
}
