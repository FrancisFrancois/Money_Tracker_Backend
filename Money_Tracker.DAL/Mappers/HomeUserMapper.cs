using Money_Tracker.DAL.Entities;
using System.Data;


namespace Money_Tracker.DAL.Mappers
{
    // Classe HomeUserMapper : Contient des méthodes pour mapper des enregistrements de base de données vers des objets
    public class HomeUserMapper
    {
        // Méthode pour mapper un enregistrement de base de données (IDataRecord) vers un objet HomeUser.
        public static HomeUser MapperHS(IDataRecord record)
        {
            // Crée et renvoie un nouvel objet HomeUser avec les données extraites de l'enregistrement IDataRecord.
            return new HomeUser
            {
                // Extraction et affectation de l'identifiant de l'utilisateur.
                User_Id = (int)record["User_Id"],

                // Extraction et affectation de l'identifiant de la maison
                Home_Id = (int)record["Home_Id"]
            };
        }
    }
}

