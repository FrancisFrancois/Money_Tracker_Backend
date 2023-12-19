using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.Interfaces;
using Money_Tracker.BLL.Services;

namespace Money_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _UserService;

        public AuthController(IUserService userService)
        {
            _UserService = userService;
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

        [HttpPost("Login")]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto is null)
            {
                return BadRequest("Invalid request");
            }

            bool isValidUser = _UserService.ValidateLogin(loginDto.PseudoOrEmail, loginDto.Password);

            if (!isValidUser)
            {
                return Unauthorized("Invalid Credential");
            }

            return Ok("Login successful");
        }
    }
}
