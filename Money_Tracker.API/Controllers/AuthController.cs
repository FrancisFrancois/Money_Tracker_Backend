using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Models;
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
        // Déclaration des services nécessaires
        private readonly IUserService _UserService;
        private readonly JwtOptions _JwtOptions;

        // Constructeur pour injecter les dépendances
        public AuthController(IUserService userService, JwtOptions jwtOptions)
        {
            _UserService = userService;
            _JwtOptions = jwtOptions;
        }

        // Endpoint pour l'enregistrement d'un nouvel utilisateur
        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            // Vérifie si l'email ou le pseudo existe déjà
            if (_UserService.IsEmailOrPseudoExists(registerDTO.Email, registerDTO.Pseudo))
            {
                return BadRequest("Email or Pseudo already in use.");
            }

            // Crée l'utilisateur et le convertit en DTO
            UserDTO createdUser = _UserService.Create(registerDTO.ToModel()).ToDTO();
            if (createdUser is null)
            {
                return BadRequest("Failed to create user.");
            }

            // Retourne les informations de l'utilisateur créé
            return CreatedAtAction("Register", new { userId = createdUser.Id }, createdUser);
        }

        // Endpoint pour la connexion d'un utilisateur
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

            // Valide les informations de connexion
            bool isValidUser = _UserService.ValidateLogin(loginDto.PseudoOrEmail, loginDto.Password);

            // Retourne une erreur si les informations ne sont pas valides
            if (!isValidUser)
            {
                return Unauthorized("Invalid credentials");
            }

            // Génère un jeton JWT pour l'utilisateur
            string token = GenerateJwtToken(loginDto.PseudoOrEmail);
            DateTime expiration = DateTime.Now.AddSeconds(_JwtOptions.Expiration);

            // Retourne le jeton et la date d'expiration
            return Ok(new
            {
                accessToken = token,
                Expiration = expiration
            });
        }

        // Méthode privée pour générer un jeton JWT
        private string GenerateJwtToken(string userNameOrEmail)
        {
            // Crée les revendications (claims) pour le jeton
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userNameOrEmail)
            };

            // Crée une clé symétrique pour signer le jeton
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.SigningKey));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crée le jeton JWT
            JwtSecurityToken Token = new JwtSecurityToken
                (
                issuer: _JwtOptions.Issuer,
                audience: _JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(_JwtOptions.Expiration),
                signingCredentials: creds
                );

            // Retourne le jeton JWT sous forme de chaîne
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
