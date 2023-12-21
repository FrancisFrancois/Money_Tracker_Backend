using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    // Classe UserMapper :Fournit des méthodes statiques pour mapper entre les modèles de la BLL et les entités de la DAL.
    public static class UserMapper
    {
        // Convertit une entité User (DAL) en un modèle User (BLL).
        public static Models.User ToModel(this Entities.User entity)
        {
            return new Models.User
            {
                Id = entity.Id, // Mappe l'identifiant de l'entité vers le modèle.
                Lastname = entity.Lastname, // Mappe le nom de famille de l'entité vers le modèle.
                Firstname = entity.Firstname, // Mappe le prénom de l'entité vers le modèle.
                Pseudo = entity.Pseudo, // Mappe le pseudo de l'entité vers le modèle.
                Email = entity.Email, // Mappe l'email de l'entité vers le modèle.
                Password = entity.Password, // Mappe le mot de passe de l'entité vers le modèle.
                Roles = entity.Roles, // Mappe les rôles de l'entité vers le modèle.
            };
        }

        // Convertit un modèle User (BLL) en une entité User (DAL).
        public static Entities.User ToEntity(this Models.User model)
        {
            return new Entities.User
            {
                Id = model.Id, // Mappe l'identifiant du modèle vers l'entité.
                Lastname = model.Lastname, // Mappe le nom de famille du modèle vers l'entité.
                Firstname = model.Firstname, // Mappe le prénom du modèle vers l'entité.
                Pseudo = model.Pseudo, // Mappe le pseudo du modèle vers l'entité.
                Email = model.Email, // Mappe l'email du modèle vers l'entité.
                Password = model.Password, // Mappe le mot de passe du modèle vers l'entité.
                Roles = model.Roles, // Mappe les rôles du modèle vers l'entité.
            };
        }
    }
}
