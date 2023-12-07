using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System.Data.Common;

namespace Money_Tracker.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection _DbConnection;

        public UserRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        public IEnumerable<User> GetAll()
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Users]";
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return UserMapper.Mapper(reader);
                    } 
                };
                _DbConnection.Close();
            }
        }

        public User? GetById(int id)
        {
            User? result = null;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Users] WHERE [User_Id] = @Id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = UserMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }

        public User Create(User user)
        {
            User user;
        }





        public bool Update(int id, User entity)
        {
            throw new NotImplementedException();

        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }



    }
}
