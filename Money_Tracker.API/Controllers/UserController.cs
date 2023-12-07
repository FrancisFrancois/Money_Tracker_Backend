using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.Interfaces;

namespace Money_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]

        public IActionResult GetAll()
        {
            IEnumerable<UserDTO> result = _UserService.GetAll().Select(u => u.ToDTO());
            return Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(UserDTO))]

        public IActionResult GetById([FromRoute] int userId)
        {
            UserDTO? userDTO = _UserService.GetById(userId)?.ToDTO();
            if (userDTO is null)
            {
                return NotFound("User not found");
            }

            return Ok(userDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDTO))]

        public IActionResult Insert([FromBody] UserDataDTO user)
        {
            UserDTO result = _UserService.Insert(user.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { userId = result.Id }, result);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]

        public IActionResult Update([FromRoute] int userId, [FromBody] UserDataDTO user)
        {
            bool updated;
            try
            {
                updated = _UserService.Update(userId, user.ToModel());
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

            return updated ? NoContent() : NotFound("User not found");
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        [ProducesResponseType(400)]

        public IActionResult Delete([FromRoute] int userId)
        {
            bool deleted;
            try
            {
                deleted = _UserService.Delete(userId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return deleted ? NoContent() : NotFound("User not found");
        }
    }
}
