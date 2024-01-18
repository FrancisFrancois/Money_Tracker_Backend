using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Models;
using Money_Tracker.BLL.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Money_Tracker.API.Controllers
{
    // Définit le chemin de base pour accéder au contrôleur AuthController
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Déclaration de l'instance du service utilisateur et du JWT
        private readonly IUserService _UserService;
        private readonly JwtOptions _JwtOptions;

        // Constructeur pour injecter les dépendances
        public AuthController(IUserService userService, JwtOptions jwtOptions)
        {
            _UserService = userService;
            _JwtOptions = jwtOptions;
        }

        // Route POST pour l'enregistrement d'un nouvel utilisateurtr
        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            // Vérifie si l'email ou le pseudo existe déjà
            if (_UserService.IsEmailOrPseudoExists(registerDTO.Email, registerDTO.Pseudo))
            {
                // Renvoie une réponse HTTP 400 (Bad Request) si l'email ou le pseudo est déjà utilisé
                return BadRequest("Email or Pseudo already in use."); 
            }

            // Crée l'utilisateur et le convertit en DTO
            UserDTO createdUser = _UserService.Register(registerDTO.ToModel()).ToDTO();
            if (createdUser is null)
            {
                // Renvoie une réponse HTTP 400 (Bad Request) si la création de l'utilisateur échoue
                return BadRequest("Failed to create user.");
            }

            // Renvoie une réponse HTTP 201 (Created) avec les détails de l'utilisateur créé.
            return CreatedAtAction("Register", new { userId = createdUser.Id }, createdUser);
        }

        // Route POST pour la connexion d'un utilisateur et renvoie un JWT
        [HttpPost("Login")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            // Vérifie si le DTO de connexion est valide
            if (loginDto is null)
            {
                return BadRequest("Invalid request");
            }

            // Valide les informations de connexion de l'utilisateur
            bool isValidUser = _UserService.ValidateLogin(loginDto.PseudoOrEmail, loginDto.Password);

            // Retourne une erreur si les informations ne sont pas valides
            if (!isValidUser)
            {
                // Renvoie une réponse HTTP 401 (Unauthorized), demande de connexion échoué.
                return Unauthorized("Invalid credentials");  
      
            }

            // Récupérer l'ID de l'utilisateur
            var userId = _UserService.GetUserId(loginDto.PseudoOrEmail);
            if (!userId.HasValue)
            {
                return Unauthorized("Invalid credentials");
            }

            // Récupération des informations de l'utilisateur
            UserDTO? userDTO = _UserService.GetById(userId.Value)?.ToDTO();
            if (userDTO == null)
            {
                return NotFound("User not found");
            }


            // Récupérer le rôle de l'utilisateur
            string role = _UserService.GetUserRole(loginDto.PseudoOrEmail);

            // Génère un jeton JWT pour l'utilisateur
            string token = GenerateJwtToken(loginDto.PseudoOrEmail, role);
            DateTime expiration = DateTime.Now.AddSeconds(_JwtOptions.Expiration);

            // Retourne le jeton et la date d'expiration
            return Ok(new
            {
                accessToken = token,
                Expiration = expiration,
                User = userDTO

            });
        }

        // Méthode privée pour générer un jeton JWT
        private string GenerateJwtToken(string userPseudoOrEmail, string role)
        {
            // Obtient l'ID de l'utilisateur en fonction de son pseudo ou de son email
            var userId = _UserService.GetUserId(userPseudoOrEmail);

            // Si l'ID de l'utilisateur n'existe pas, lance une exception
            if (!userId.HasValue)
            {
                throw new Exception("User not found");
            }

            // Crée les revendications (claims) pour le jeton
            List<Claim> claims = new List<Claim>
            {
                // Revendication pour le nom de l'utilisateur
                new Claim(ClaimTypes.Name, userPseudoOrEmail),

                // Revendication pour le rôle de l'utilisateur
                new Claim(ClaimTypes.Role, role),

                // Revendication pour l'identifiant de l'utilisateur
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };

            // Crée une clé symétrique pour signer le jeton à l'aide de la clé de signature JWT
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.SigningKey));

            // Crée des informations d'authentification pour signer le jeton
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crée le jeton JWT en spécifiant diverses informations
            JwtSecurityToken Token = new JwtSecurityToken
            (
                issuer: _JwtOptions.Issuer,                                  // Émetteur du jeton
                audience: _JwtOptions.Audience,                              // Public cible du jeton
                claims: claims,                                              // Revendications incluses dans le jeton
                expires: DateTime.Now.AddSeconds(_JwtOptions.Expiration),    // Date d'expiration du jeton
                signingCredentials: creds                                    // Informations d'authentification pour la signature
            );

            // Retourne le jeton JWT sous forme de chaîne
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }


    }
}
