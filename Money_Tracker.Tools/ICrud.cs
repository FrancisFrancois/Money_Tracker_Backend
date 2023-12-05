using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.Tools
{
    /// <summary>
    /// Interface générique pour les opérations CRUD (Create, Read, Update, Delete).
    /// </summary>
    /// <typeparam name="TId">Le type de l'identifiant utilisé pour les entités.</typeparam>
    /// <typeparam name="TEntity">Le type de l'entité.</typeparam>
    public interface ICrud<TId, TEntity> where TEntity : class
    {
        /// <summary>
        /// Récupère toutes les entités de type TEntity.
        /// </summary>
        /// <returns>Une collection énumérable des entités de type TEntity.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Récupère une entité spécifique de type TEntity par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à récupérer.</param>
        /// <returns>L'entité de type TEntity correspondante à l'identifiant fourni.</returns>
        TEntity GetById(TId id);

        /// <summary>
        /// Crée une nouvelle entité de type TEntity.
        /// </summary>
        /// <param name="entity">L'entité à créer.</param>
        /// <returns>L'entité de type TEntity créée.</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Met à jour une entité existante de type TEntity.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à mettre à jour.</param>
        /// <param name="entity">Les données de l'entité mise à jour.</param>
        /// <returns>True si l'opération de mise à jour a réussi ; sinon, false.</returns>
        bool Update(TId id, TEntity entity);

        /// <summary>
        /// Supprime une entité de type TEntity par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de l'entité à supprimer.</param>
        /// <returns>True si l'opération de suppression a réussi ; sinon, false.</returns>
        bool Delete(TId id);
    }

}
