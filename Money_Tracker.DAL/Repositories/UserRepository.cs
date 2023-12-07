﻿using Money_Tracker.DAL.Entities;
using Money_Tracker.DAL.Interfaces;
using Money_Tracker.DAL.Mappers;
using Money_Tracker.Tools.Utils;
using System.Data.Common;
using System;
using System.Data;

namespace Money_Tracker.DAL.Repositories
{
    /// <summary>
    /// Représente un repository pour la manipulation des utilisateurs en base de données.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection _DbConnection;

        public UserRepository(DbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }

        /// <summary>
        /// Récupère tous les utilisateurs de la base de données.
        /// </summary>
        public IEnumerable<User> GetAll()
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [User]";
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

        /// <summary>
        /// Récupère un utilisateur par son ID.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à récupérer.</param>
        /// <returns>L'utilisateur correspondant à l'ID donné.</returns>
        public User? GetById(int id)
        {
            User? result = null;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM [User] WHERE [User_Id] = @Id";
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

        /// <summary>
        /// Crée un nouvel utilisateur dans la base de données.
        /// </summary>
        /// <param name="user">Utilisateur à créer.</param>
        /// <returns>L'utilisateur créé.</returns>
        public User Create(User user)
        {
            User result;
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE [Email] = @email";
                command.addParamWithValue("email", user.Email);
                _DbConnection.Open();

                int emailCount = (int)command.ExecuteScalar();

                command.CommandText = "SELECT COUNT(*) FROM [User] WHERE [Pseudo] = @pseudo";
                command.addParamWithValue("pseudo", user.Pseudo);

                int pseudoCount = (int)command.ExecuteScalar();

                if (emailCount > 0 && pseudoCount > 0)
                {
                    throw new Exception("L'email et le pseudo existent déjà");
                }
                else if (emailCount > 0)
                {
                    throw new Exception("L'email existe déjà");
                }
                else if (pseudoCount > 0)
                {
                    throw new Exception("Le pseudo existe déjà");
                }
                else
                {
                    command.CommandText = "INSERT INTO [User] ([Name],[Firstname],[Pseudo],[Email],[Hash_Password],[Roles]) " +
                                          "OUTPUT INSERTED.* " +
                                          "VALUES (@name, @firstname, @pseudo, @email, @hash_password, @roles)";
                    command.addParamWithValue("name", user.Name);
                    command.addParamWithValue("firstname", user.Firstname);
                    command.addParamWithValue("pseudo", user.Pseudo);
                    command.addParamWithValue("email", user.Email);
                    command.addParamWithValue("hash_password", user.Password);
                    command.addParamWithValue("roles", user.Roles);

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = UserMapper.Mapper(reader);
                        }
                        else
                        {
                            throw new Exception("Erreur lors de l'ajout de l'utilisateur");
                        }
                    }
                };
                _DbConnection.Close();
            };
            return result;
        }

        /// <summary>
        /// Met à jour les informations d'un utilisateur dans la base de données.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à mettre à jour.</param>
        /// <param name="user">Nouvelles informations de l'utilisateur.</param>
        /// <returns>Booléen indiquant si la mise à jour a réussi.</returns>
        public bool Update(int id, User user)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "UPDATE [User] " +
                                      " SET [Name] = @name, " +
                                      "    [Firstname] = @firstname," +
                                      "    [Pseudo] = @pseudo, " +
                                      "    [Email] = @email, " +
                                      "    [Hash_Password] = @hash_password, " +
                                      "    [Roles] = @roles " +
                                      " WHERE [User_Id] = @id";
                command.addParamWithValue("name", user.Name);
                command.addParamWithValue("firstname", user.Firstname);
                command.addParamWithValue("pseudo", user.Pseudo);
                command.addParamWithValue("email", user.Email);
                command.addParamWithValue("hash_password", user.Password);
                command.addParamWithValue("roles", user.Roles);
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowUpdated = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowUpdated == 1;
            }

        }

        /// <summary>
        /// Supprime un utilisateur de la base de données.
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer.</param>
        /// <returns>Booléen indiquant si la suppression a réussi.</returns>
        public bool Delete(int id)
        {
            using (DbCommand command = _DbConnection.CreateCommand())
            {
                command.CommandText = "DELETE FROM [User] WHERE [User_Id] = @id";
                command.addParamWithValue("id", id);
                _DbConnection.Open();
                int nbRowDeleted = command.ExecuteNonQuery();
                _DbConnection.Close();
                return nbRowDeleted == 1;
            }
        }



    }
}
