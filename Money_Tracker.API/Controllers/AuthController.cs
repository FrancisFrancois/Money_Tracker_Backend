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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly JwtOptions _JwtOptions;

        public AuthController(IUserService userService, JwtOptions jwtOptions)
        {
            _UserService = userService;
            _JwtOptions = jwtOptions;
        }

        [HttpPost("Register")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            if (_UserService.IsEmailOrPseudoExists(registerDTO.Email, registerDTO.Pseudo))
            {
                return BadRequest("Email or Pseudo already in use.");
            }

            var createdUser = _UserService.Create(registerDTO.ToModel());
            if (createdUser is null)
            {
                return BadRequest("Failed to create user.");
            }

            var resultDTO = createdUser.ToDTO();
            return CreatedAtAction("Register", new { userId = resultDTO.Id }, resultDTO);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto is null)
            {
                return BadRequest("Invalid request");
            }

            bool isValidUser = _UserService.ValidateLogin(loginDto.PseudoOrEmail, loginDto.Password);

            if (!isValidUser)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = GenerateJwtToken(loginDto.PseudoOrEmail);
            var expiration = DateTime.Now.AddSeconds(_JwtOptions.Expiration);

            return Ok(new
            {
                accessToken = token,
                Expiration = expiration
            });
        }

        private string GenerateJwtToken(string userNameOrEmail)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userNameOrEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.SigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken Token = new JwtSecurityToken
                (

                issuer: _JwtOptions.Issuer,
                audience: _JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddSeconds(_JwtOptions.Expiration),
                signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
    


