using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    // Classe User : Représente un utilisateur 
    public class User
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur

        public string Lastname { get; set; } = string.Empty; // Nom de famille de l'utilisateur.

        public string Firstname { get; set; } = string.Empty; // Prénom de l'utilisateur.

        public string Pseudo { get; set; } = string.Empty; // Pseudo de l'utilisateur

        public string Email { get; set; } = string.Empty; // Adresse e-mail de l'utilisateur

        public string Password { get; set; } = string.Empty; // Mot de passe de l'utilisateur

        public string Roles { get; set; } = string.Empty; // Rôles de l'utilisateur. Utilisé pour définir les permissions et accès dans l'application.
    }
}

