using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IHomeService : Définit les opérations pour la gestion des maisons
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface IHomeService : ICrudService<int, Home>
    {
        // Actuellement, cette interface n'ajoute pas de méthodes supplémentaires spécifiques aux catégories.
        // Elle hérite de toutes les fonctionnalités CRUD de base de ICrudService..
    }
}
