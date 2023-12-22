using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs
{
    // Classe HomeDTO : Utilisée pour représenter les informations des dépenses dans les réponses API
    public class HomeDTO
    {
        public int Id { get; set; } // Identifiant unique du domicile
        public int User_Id { get; set; } // Identifiant de l'utilisateur principal associé au domicile
        public string Name_Home { get; set; } = string.Empty; // Nom du domicile
    }

    // Classe HomeFullDTO : Version étendue de HomeDTO incluant une liste des utilisateurs associés au domicile
    public class HomeFullDTO
    {
        public int Id { get; set; } // Identifiant unique du domicile
        public int User_Id { get; set; } // Identifiant de l'utilisateur principal associé au domicile
        public string Name_Home { get; set; } = string.Empty; // Nom du domicile
        public IEnumerable<HomeUserDTO> Users { get; set; } // Liste des utilisateurs associés à ce domicile
    }

    // Classe HomeUserDTO : Utilisée pour représenter la relation entre les utilisateurs et les domiciles
    public class HomeUserDTO
    {
        public int User_Id { get; set; } // Identifiant de l'utilisateur
        public int Home_Id { get; set; } // Identifiant du domicile associé à l'utilisateur
    }

    // Classe HomeDataDTO : Utilisée pour capturer les données lors de la création ou de la mise à jour d'un domicile
    public class HomeDataDTO
    {
        [Required] 
        public int User_Id { get; set; } // Identifiant de l'utilisateur principal associé au domicile
        [Required] 
        public string Name_Home { get; set; } = string.Empty; // Nom du domicile
    }
}
