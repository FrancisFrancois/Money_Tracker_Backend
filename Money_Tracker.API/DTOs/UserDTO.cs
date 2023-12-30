using System.ComponentModel.DataAnnotations;

namespace Money_Tracker.API.DTOs

{
    // Classe UserDTO : Utilisée pour représenter les données des users dans les réponses API
    public class UserDTO
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur
        public string Lastname { get; set; } = string.Empty; // Nom de famille de l'utilisateur
        public string Firstname { get; set; } = string.Empty; // Prénom de l'utilisateur
        public string Pseudo { get; set; } = string.Empty; // Pseudo de l'utilisateur
        public string Email { get; set; } = string.Empty; // Adresse email de l'utilisateur
        public string Roles { get; set; } = string.Empty; // Rôles de l'utilisateur
    }

    // Classe UserDataDTO :  Utilisée pour capturer les données de la dépense lors des requêtes de création ou de mise à jour 
    public class UserDataDTO
    {
        public int Id { get; set; } // Identifiant unique de l'utilisateur 
        public string Lastname { get; set; } = string.Empty; // Nom de famille de l'utilisateur
        public string Firstname { get; set; } = string.Empty; // Prénom de l'utilisateur
        public string Pseudo { get; set; } = string.Empty; // Pseudo de l'utilisateur
        public string Email { get; set; } = string.Empty; // Adresse email de l'utilisateur
        public string Password { get; set; } = string.Empty; // Mot de passe de l'utilisateur
        public string Roles { get; set; } = string.Empty; // Rôles de l'utilisateur 
    }

    // Classe LoginDTO : Uttilisée pour les données de connexion de l'utilisateur
    public class LoginDTO
    {
        [Required]
        public string PseudoOrEmail { get; set; } = string.Empty; // Pseudo ou email pour la connexion
        [Required]
        public string Password { get; set; } = string.Empty; // Mot de passe pour la connexion
    }

    // Classe RegisterDTO : Utilisée pour les données d'enregistrement de l'utilisateur
    public class RegisterDTO
    {   
        [Required]
        public string Lastname { get; set; } = string.Empty; // Nom de famille de l'utilisateur
        [Required]
        public string Firstname { get; set; } = string.Empty; // Prénom de l'utilisateur
        [Required]
        public string Pseudo { get; set; } = string.Empty; // Pseudo de l'utilisateur
        [Required]
        public string Email { get; set; } = string.Empty; // Adresse email de l'utilisateur
        [Required]
        public string Password { get; set; } = string.Empty; // Mot de passe de l'utilisateur
        
    }
}
