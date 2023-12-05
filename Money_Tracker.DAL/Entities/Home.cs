using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    /// <summary>
    /// Représente un domicile.
    /// </summary>
    public class Home
    {
        /// <summary>
        /// Obtient ou définit l'identifiant du domicile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur associé au domicile.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom du domicile.
        /// </summary>
        public string Name_Home { get; set; } = string.Empty;
    }

}
