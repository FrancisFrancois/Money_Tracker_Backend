namespace Money_Tracker.API.DTOs
{
    /// <summary>
    /// Classe représentant un objet de transfert de données (DTO) pour un utilisateur.
    /// </summary>
    public class UserDTO
    {

        /// <summary>
        /// ID de l'utilisateur.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Prénom de l'utilisateur.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Pseudo de l'utilisateur.
        /// </summary>
        public string Pseudo { get; set; } = string.Empty;

        /// <summary>
        /// Email de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;

    }

    /// <summary>
    /// Classe représentant un objet de transfert de données (DTO) pour les données d'utilisateur.
    /// </summary>
    public class UserDataDTO
    {
        /// <summary>
        /// ID de l'utilisateur.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom de l'utilisateur.
        /// </summary>
        public string Lastname { get; set; } = string.Empty;

        /// <summary>
        /// Prénom de l'utilisateur.
        /// </summary>
        public string Firstname { get; set; } = string.Empty;

        /// <summary>
        /// Pseudo de l'utilisateur.
        /// </summary>
        public string Pseudo { get; set; } = string.Empty;

        /// <summary>
        /// Email de l'utilisateur.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Mot de passe de l'utilisateur.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Rôles de l'utilisateur.
        /// </summary>
        public string Roles { get; set; } = string.Empty;
    }
}