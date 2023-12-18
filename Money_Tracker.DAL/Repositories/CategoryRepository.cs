using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbConnection _DbConnection;

        public CategoryRepository(DbConnection dbconnection)
        {
            _DbConnection = dbconnection;
        }

        public IEnumerable<Category> GetAll()
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Category]";
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        yield return CategoryMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            };
        }

        public Category? GetById(int id)
        {
            Category? result = null;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [Category] WHERE [Category_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = CategoryMapper.Mapper(reader);
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }

        public Category Create(Category category)
        {
            Category result;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "INSERT INTO [Category] ([Category_Name]) " +
                                      " OUTPUT INSERTED.* " +
                                      "VALUES (@category_name)";
                command.addParamWithValue("category_name", category.Category_Name);
                _DbConnection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("Erreur lors de l'ajout de la catégories");
                    }
                    result = CategoryMapper.Mapper(reader);
                };
                _DbConnection.Close();
            };
            return result;
        }

        public bool Update(int id, Category category)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText =
                    "UPDATE [Category]" +
                    " SET [Category_Name] = @category_name" +
                    " WHERE [Category_Id] = @id";
                command.addParamWithValue("category_name", category.Category_Name);
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
                command.CommandText = "DELETE FROM [Category] WHERE [Category_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowDeleted = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowDeleted == 1;
            };
        }
    }
}