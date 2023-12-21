using Money_Tracker.DAL.Entities;

namespace Money_Tracker.BLL.Models
{
    // Classe Home : Représente une maison 
    public class Home
    {
        public int Id { get; set; } // Identifiant unique de la maison. 

        public int User_Id { get; set; } // Identifiant de l'utilisateur principal associé à la maison.

        public string Name_Home { get; set; } = string.Empty; // Nom de la maison

        public IEnumerable<HomeUser> Users { get; set; } // Collection d'utilisateurs associés à la maison.
                                                         
    }
}
