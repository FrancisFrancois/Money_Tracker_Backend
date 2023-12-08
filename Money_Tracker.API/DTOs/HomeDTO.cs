namespace Money_Tracker.API.DTOs
{
    /// <summary>
    /// Représente un modèle simplifié pour une maison (Home).
    /// </summary>
    public class HomeDTO
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la maison.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur associé à la maison.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la maison.
        /// </summary>
        public string Name_Home { get; set; } = string.Empty;
    }

    /// <summary>
    /// Représente un modèle détaillé pour une maison (Home) avec ses utilisateurs associés.
    /// </summary>
    public class HomeFullDTO
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la maison.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur associé à la maison.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la maison.
        /// </summary>
        public string Name_Home { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit une collection de modèles d'utilisateurs associés à la maison.
        /// </summary>
        public IEnumerable<HomeUserDTO> Users { get; set; }
    }

    /// <summary>
    /// Représente un modèle simplifié pour la relation entre un utilisateur et une maison.
    /// </summary>
    public class HomeUserDTO
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur associé à la maison.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de la maison.
        /// </summary>
        public int Home_Id { get; set; }
    }
}