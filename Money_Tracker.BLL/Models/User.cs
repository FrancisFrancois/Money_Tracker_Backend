using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    /// <summary>
    /// Représente un utilisateur.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de famille de l'utilisateur.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le prénom de l'utilisateur.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le pseudo de l'utilisateur.
        /// </summary>
        public string Pseudo { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit l'adresse e-mail de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le mot de passe de l'utilisateur.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit les rôles de l'utilisateur.
        /// </summary>
        public string Roles { get; set; } = string.Empty;
    }

}
