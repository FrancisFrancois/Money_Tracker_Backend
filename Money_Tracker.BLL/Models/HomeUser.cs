using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    /// <summary>
    /// Représente une relation entre un utilisateur (User) et une maison (Home).
    /// </summary>
    public class HomeUser
    {
        /// <summary>
        /// Obtient ou définit l'utilisateur associé à cette relation maison-utilisateur.
        /// </summary>
        public User? User { get; set; }

        /// <summary>
        /// Obtient ou définit la maison associée à cette relation maison-utilisateur.
        /// </summary>
        public Home? Home { get; set; }
    }
}