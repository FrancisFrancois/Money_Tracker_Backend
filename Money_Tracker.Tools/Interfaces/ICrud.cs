using System;
using System.Collections.Generic;

namespace Money_Tracker.Tools.Interfaces
{
    // Interface générique pour les opérations CRUD (Create, Read, Update, Delete).
    // - TId : Le type de l'identifiant utilisé pour les entités.
    // - TEntity : Le type de l'entité.
    public interface ICrud<TId, TEntity> where TEntity : class
    {
        // Méthode pour récupérer toutes les entités de type TEntity.
        IEnumerable<TEntity> GetAll();

        // Méthode pour récupérer une entité spécifique de type TEntity par son identifiant.
        // - id : L'identifiant de l'entité à récupérer.
        // Renvoie l'entité de type TEntity correspondante à l'identifiant fourni, ou null si elle n'existe pas.
        TEntity? GetById(TId id);

        // Méthode pour créer une nouvelle entité de type TEntity.
        // - entity : L'entité à créer.
        // Renvoie l'entité de type TEntity créée.
        TEntity Create(TEntity entity);

        // Méthode pour mettre à jour une entité existante de type TEntity.
        // - id : L'identifiant de l'entité à mettre à jour.
        // - entity : Les données de l'entité mise à jour.
        // Renvoie true si l'opération de mise à jour a réussi ; sinon, false.
        bool Update(TId id, TEntity entity);

        // Méthode pour supprimer une entité de type TEntity par son identifiant.
        // - id : L'identifiant de l'entité à supprimer.
        // Renvoie true si l'opération de suppression a réussi ; sinon, false.
        bool Delete(TId id);
    }
}
