﻿using Money_Tracker.DAL.Entities;
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
    /// <summary>
    /// Implémentation du repository pour accéder aux données liées aux maisons.
    /// </summary>
    public class HomeRepository : IHomeRepository
    {
        private readonly DbConnection _DbConnection;

        public HomeRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        /// <summary>
        /// Récupère toutes les maisons depuis la base de données.
        /// </summary>
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

        /// <summary>
        /// Récupère une maison par son ID depuis la base de données.
        /// </summary>
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

        /// <summary>
        /// Crée une nouvelle maison dans la base de données.
        /// </summary>
        public Home Create(Home home)
        {
            Home result;
            try
            {
                using (DbCommand command = _DbConnection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO [Home] ([User_Id],[Name_Home]) " +
                                            " OUTPUT INSERTED.* " +
                                            "VALUES (@user_id, @name_home)";
                    command.addParamWithValue("@user_id", home.User_Id);
                    command.addParamWithValue("@name_home", home.Name_Home);

                    _DbConnection.Open();
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
                }
            }
            catch (DbException ex)
            {
                // Gérer l'exception liée à l'accès aux données
                throw new Exception("Erreur d'accès aux données : " + ex.Message);
            }
            finally
            {
                _DbConnection.Close();
            }

            return result;
        }


        /// <summary>
        /// Met à jour une maison dans la base de données.
        /// </summary>
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

        /// <summary>
        /// Supprime une maison de la base de données par son ID.
        /// </summary>
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

        /// <summary>
        /// Récupère les utilisateurs associés à une maison depuis la base de données.
        /// </summary>
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
