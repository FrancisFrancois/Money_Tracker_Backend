using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.Tools.Interfaces
{
    /// <summary>
    /// Représente une interface de service générique CRUD (Create, Read, Update, Delete).
    /// </summary>
    /// <typeparam name="TId">Le type d'identifiant utilisé pour les entités.</typeparam>
    /// <typeparam name="TModel">Le type du modèle/entité.</typeparam>
    public interface ICrudService<TId, TModel> where TModel : class
    {
        /// <summary>
        /// Récupère toutes les entités de type TModel.
        /// </summary>
        /// <returns>Une collection énumérable des entités de type TModel.</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Récupère une entité spécifique de type TModel par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à récupérer.</param>
        /// <returns>L'entité de type TModel si elle est trouvée ; sinon, retourne null.</returns>
        TModel? GetById(TId id);

        /// <summary>
        /// Crée une nouvelle entité de type TModel.
        /// </summary>
        /// <param name="entity">L'entité à créer.</param>
        /// <returns>L'entité de type TModel créée.</returns>
        TModel Create(TModel entity);

        /// <summary>
        /// Met à jour une entité existante de type TModel.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à mettre à jour.</param>
        /// <param name="entity">Les données de l'entité mise à jour.</param>
        /// <returns>True si l'opération de mise à jour a réussi ; sinon, false.</returns>
        bool Update(TId id, TModel entity);

        /// <summary>
        /// Supprime une entité de type TModel par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à supprimer.</param>
        /// <returns>True si l'opération de suppression a réussi ; sinon, false.</returns>
        bool Delete(TId id);
    }

}
