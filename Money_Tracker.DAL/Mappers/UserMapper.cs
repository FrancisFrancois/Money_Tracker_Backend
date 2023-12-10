using Money_Tracker.DAL.Entities;
using System.Data;


namespace Money_Tracker.DAL.Mappers
{
    /// <summary>
    /// Classe responsable du mapping entre IDataRecord et les objets User.
    /// </summary>
    public class UserMapper
    {
        /// <summary>
        /// Mappe un IDataRecord à un objet User.
        /// </summary>
        /// <param name="record">L'IDataRecord à mapper.</param>
        /// <returns>Un objet User.</returns>
        public static User Mapper(IDataRecord record)
        {
            return new User
            {
                // Mappage du champ 'Id' de IDataRecord à la propriété 'Id' de User.
                Id = (int)record["User_Id"],

                // Mappage du champ 'Name' de IDataRecord à la propriété 'Name' de User.
                Lastname = (string)record["Name"],

                // Mappage du champ 'Firstname' de IDataRecord à la propriété 'Firstname' de User.
                Firstname = (string)record["Firstname"],

                // Mappage du champ 'Pseudo' de IDataRecord à la propriété 'Pseudo' de User.
                Pseudo = (string)record["Pseudo"],

                // Mappage du champ 'Email' de IDataRecord à la propriété 'Email' de User.
                Email = (string)record["Email"],

                // Mappage du champ 'Hash_Password' de IDataRecord à la propriété 'Password' de User.
                Password = (string)record["Hash_Password"],

                // Mappage du champ 'Roles' de IDataRecord à la propriété 'Roles' de User.
                Roles = (string)record["Roles"]
            };
        }
    }

}
