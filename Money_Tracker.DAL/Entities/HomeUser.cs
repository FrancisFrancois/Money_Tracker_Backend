using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    /// <summary>
    /// Représente la liaison entre un utilisateur et un domicile.
    /// </summary>
    public class HomeUser
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur lié à un domicile.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant du domicile lié à un utilisateur.
        /// </summary>
        public int Home_Id { get; set; }
    }

}
