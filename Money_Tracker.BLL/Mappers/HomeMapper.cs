using Entities = Money_Tracker.DAL.Entities;


namespace Money_Tracker.BLL.Mappers
{
    // Classe HomeMapper : Fournit des méthodes statiques pour mapper entre les modèles de la BLL et les entités de la DAL.
    public static class HomeMapper
    {
        // Convertit une entité Home (DAL) en un modèle Home (BLL).
        public static Models.Home ToModel(this Entities.Home entity)
        {
            return new Models.Home
            {
                Id = entity.Id, // Mappe l'identifiant de l'entité vers le modèle.
                User_Id = entity.User_Id, // Mappe l'identifiant de l'utilisateur associé à la maison.
                Name_Home = entity.Name_Home, // Mappe le nom de la maison de l'entité vers le modèle.
            };
        }

        // Convertit un modèle Home (BLL) en une entité Home (DAL).
        public static Entities.Home ToEntity(this Models.Home model)
        {
            return new Entities.Home
            {
                Id = model.Id, // Mappe l'identifiant du modèle vers l'entité.
                User_Id = model.User_Id, // Mappe l'identifiant de l'utilisateur associé à la maison.
                Name_Home = model.Name_Home, // Mappe le nom de la maison du modèle vers l'entité.
            };
        }
    }
}
