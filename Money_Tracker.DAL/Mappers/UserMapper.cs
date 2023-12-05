using Money_Tracker.DAL.Entities;
using System.Data;


namespace Money_Tracker.DAL.Mappers
{
    public class UserMapper
    {
        public static User Mapper(IDataRecord record)
        {
            return new User
            {
                Id = (int)record["Id"],
                Lastname = (string)record["Name"],
                Firstname = (string)record["Firstname"],
                Pseudo = (string)record["Pseudo"],
                Email = (string)record["Email"],
                Password = (string)record["Hash_Password"],
                Roles = (string)record["Roles"]
            };
        }
    }
}
