using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.BLL.Models
{
    // Classe HomeUser : Représente une relation entre un utilisateur (User) et une maison (Home).
    public class HomeUser
    {
        public User? User { get; set; } // Référence à l'objet utilisateur associé à cette relation maison-utilisateur.
                                        // Permet de relier directement un utilisateur à une maison.

        public int Home_Id { get; set; } // Identifiant de la maison associée à cette relation.
                                         // Utilisé pour relier cette relation à une maison spécifique.
    }
}
