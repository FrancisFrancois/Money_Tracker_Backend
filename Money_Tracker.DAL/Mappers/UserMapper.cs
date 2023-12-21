using Money_Tracker.DAL.Entities;
using System.Data;

namespace Money_Tracker.DAL.Mappers
{
    // Classe UserMapper : Contient des méthodes pour mapper des enregistrements de base de données vers des objets
    public class UserMapper
    {
        // Méthode pour mapper un enregistrement de base de données (IDataRecord) vers un objet User.
        public static User Mapper(IDataRecord record)
        {
            // Crée et renvoie un nouvel objet User basé sur les données extraites de l'enregistrement.
            return new User
            {
                // Extraction et affectation de l'identifiant de l'utilisateur.
                Id = (int)record["User_Id"],

                // Extraction et affectation du nom de famille de l'utilisateur.
                Lastname = (string)record["Name"],

                // Extraction et affectation du prénom de l'utilisateur.
                Firstname = (string)record["Firstname"],

                // Extraction et affectation du pseudo de l'utilisateur.
                Pseudo = (string)record["Pseudo"],

                // Extraction et affectation de l'adresse e-mail de l'utilisateur.
                Email = (string)record["Email"],

                // Extraction et affectation du mot de passe (hashé) de l'utilisateur.
                Password = (string)record["Hash_Password"],

                // Extraction et affectation des rôles de l'utilisateur.
                Roles = (string)record["Roles"]
            };
        }
    }
}
