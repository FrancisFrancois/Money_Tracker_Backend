using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DbConnection _DbConnection;

        public HomeRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        public IEnumerable<Home> GetAll()
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Home]";
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return HomeMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            }
        }

        public Home? GetById(int id)
        {
            Home? result = null;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Home] WHERE [Home_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = HomeMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }

        public Home Create(Home home)
        {
            Home result;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM [Home] WHERE [Name_Home] = @name_home";
                command.addParamWithValue("name_home", home.Name_Home);
                _DbConnection.Open();

                int homeCount = (int)command.ExecuteScalar();

                if (homeCount > 0)
                {
                    throw new Exception("Le nom de maison existe déjà");
                }
                else
                {
                    command.CommandText = "INSERT INTO [Home] ([Name_Home] " +
                                          "OUTPUT INSERTED.* " +
                                          "VALUES (@name_home,)";
                    command.addParamWithValue("name_home", home.Name_Home);
       

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = HomeMapper.Mapper(reader);
                        }
                        else
                        {
                            throw new Exception("Erreur lors de l'ajout de la maison");
                        }
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }

        public bool Update(int id, Home home)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText =
                    "UPDATE [Home]" +
                    " SET [Name_Home] = @name_home," +
                    " WHERE [Home_Id] = @id";
                command.addParamWithValue("name_home", home.Name_Home);
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowUpdated = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowUpdated == 1;
            }
        }
    
        public bool Delete(int id)
        {
        using (DbCommand command = _DbConnection.CreateCommand())
        {
            command.CommandText = "DELETE FROM [Home] WHERE [Home_Id] = @id";
            command.addParamWithValue("id", id);
            _DbConnection.Open();
            int nbRowDeleted = command.ExecuteNonQuery();
            _DbConnection.Close();
            return nbRowDeleted == 1;
        };
    }

        public IEnumerable<HomeUser> GetUsers(int userId)
        {
            using (DbCommand command = _DbConnection.CreateCommand()) 
            {
                command.CommandText = "SELECT * FROM [Home_User] WHERE [Home_id] = @id";
                command.addParamWithValue("id", userId);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return HomeUserMapper.MapperHS(reader);
                    }
                };
                _DbConnection.Close();
            }
        }
    }
}
