using Money_Tracker.DAL.Entities;  
using Money_Tracker.Tools.Interfaces;  

namespace Money_Tracker.DAL.Interfaces
{
    // Interface ICategoryRepository : Définit les opérations spécifiques pour la gestion des catégories
    // Hérite de l'interface générique ICrud pour fournir des opérations CRUD standard
    public interface ICategoryRepository : ICrud<int, Category>
    {
        // Cette interface étend l'interface ICrud en spécifiant les types de clé génériques (int) et d'entité générique (Category).

        // Par conséquent, toutes les méthodes définies dans l'interface ICrud doivent être implémentées ici.
        // Cela inclut les méthodes pour créer, lire, mettre à jour et supprimer des objets Category.

        // L'interface ICategoryRepository agit comme un contrat pour toutes les classes qui souhaitent implémenter
        // un repository pour la gestion des catégories dans l'application Money Tracker.
    }
}
