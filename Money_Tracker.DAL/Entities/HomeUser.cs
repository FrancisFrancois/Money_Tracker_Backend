using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Money_Tracker.DAL.Entities
{
    namespace Money_Tracker.DAL.Entities
    {
        // Classe HomeUser : Représente une relation entre un utilisateur et un domicile dans la base de données.
        public class HomeUser
        {
            public int User_Id { get; set; } // Identifiant de l'utilisateur. 
                                             // Ce champ fait référence à l'identifiant d'un utilisateur spécifique dans la table des utilisateurs.

            public int Home_Id { get; set; } // Identifiant de la maison. 
                                             // Ce champ fait référence à l'identifiant d'une maison spécifique dans la table des maisons.
        }
    }
}