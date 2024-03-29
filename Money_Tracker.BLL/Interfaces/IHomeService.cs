﻿using Money_Tracker.BLL.Models;
using Money_Tracker.Tools.Interfaces;

namespace Money_Tracker.BLL.Interfaces
{
    // Interface IHomeService : Définit les opérations pour la gestion des maisons
    // Hérite de l'interface générique ICrudService pour fournir des opérations CRUD standard
    public interface IHomeService : ICrudService<int, Home>
    {
        IEnumerable<Home> GetAll();
        // Méthode pour ajouter un utilisateur à un domicile.
        HomeUser AddUserToHome(HomeUser homeUser);

        // Méthode pour supprimer un utilisateur d'un domicile
        bool RemoveUserFromHome(int homeId, int userId);
    }
}
