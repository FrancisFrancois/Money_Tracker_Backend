using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.Tools.Interfaces
{
    // Cette interface représente un service générique pour les opérations CRUD (Create, Read, Update, Delete).
    // - TId : Le type de l'identifiant utilisé pour les entités.
    // - TModel : Le type du modèle/entité.
    public interface ICrudService<TId, TModel> where TModel : class
    {
        // Méthode pour récupérer toutes les entités de type TModel.
        //  IEnumerable<TModel> GetAll(int userId);

        // Méthode pour récupérer une entité spécifique de type TModel par son identifiant.
        // - id : L'identifiant de l'entité à récupérer.
        // Renvoie l'entité de type TModel si elle est trouvée ; sinon, retourne null.
        TModel? GetById(TId id);

        // Méthode pour créer une nouvelle entité de type TModel.
        // - entity : L'entité à créer.
        // Renvoie l'entité de type TModel créée.
        TModel Create(TModel entity);

        // Méthode pour mettre à jour une entité existante de type TModel.
        // - id : L'identifiant de l'entité à mettre à jour.
        // - entity : Les données de l'entité mise à jour.
        // Renvoie true si l'opération de mise à jour a réussi ; sinon, false.
        bool Update(TId id, TModel entity);

        // Méthode pour supprimer une entité de type TModel par son identifiant.
        // - id : L'identifiant de l'entité à supprimer.
        // Renvoie true si l'opération de suppression a réussi ; sinon, false.
        bool Delete(TId id);
    }
}
