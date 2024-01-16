using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface ICategoryService : Définit les opérations spécifiques pour la gestion des catégories
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface ICategoryService : ICrudService<int, Category>
    {
        IEnumerable<Category> GetAll();
        // Actuellement, cette interface n'ajoute pas de méthodes supplémentaires spécifiques aux catégories.
        // Elle hérite de toutes les fonctionnalités CRUD de base de ICrudService.
    }
}
