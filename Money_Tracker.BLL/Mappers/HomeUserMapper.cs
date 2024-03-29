﻿using Models = Money_Tracker.BLL.Models;
using Entities = Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Mappers
{
    // Classe HomeUserMapper : Fournit des méthodes statiques pour mapper entre les modèles de la BLL et les HomeUser de la DAL.
    public static class HomeUserMapper
    {
        // Convertit une entité HomeUser (DAL) en un modèle HomeUser (BLL).
        public static Models.HomeUser ToModel(this Entities.HomeUser entity)
        {
            return new Models.HomeUser
            {
                User = new Models.User { Id = entity.User_Id }, // Crée un modèle User avec l'identifiant fourni.
                Home_Id = entity.Home_Id, // Mappe l'identifiant du domicile de l'entité vers le modèle.
            };
        }

        // Convertit un modèle HomeUser (BLL) en une entité HomeUser (DAL).
        public static Entities.HomeUser ToEntity(this Models.HomeUser model)
        {
            return new Entities.HomeUser
            {
                User_Id = model.User?.Id ?? 0, // Utilise l'identifiant de l'utilisateur, ou 0 si User est null.
                Home_Id = model.Home_Id // Mappe l'identifiant du domicile du modèle vers l'entité.
            };
        }
    }
}
