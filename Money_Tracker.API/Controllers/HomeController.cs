using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;

namespace Money_Tracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _HomeService;

        public HomeController(IHomeService homeService)
        {
            _HomeService = homeService;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HomeDTO>))]
        public IActionResult GetAll()
        {
            IEnumerable<HomeDTO> result = _HomeService.GetAll().Select(h => h.ToDTO());
            return Ok(result);
        }


        [HttpGet("{homeId}")]
        [ProducesResponseType(200, Type = typeof(HomeFullDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetById([FromRoute] int homeId)
        {
            HomeFullDTO? result = _HomeService.GetById(homeId)?.ToFullDTO();

            if (result is null)
            {
                return NotFound("Track not found");
            }

            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(HomeDTO))]

        public IActionResult Create([FromBody] HomeDataDTO home) 
        {
            HomeDTO result = _HomeService.Create(home.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new {homeId = result.Id}, result);
        }

        [HttpPut("{homeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int homeId, [FromBody] HomeDataDTO home)
        {
            bool updated;
            try
            {
                updated = _HomeService.Update(homeId, home.ToModel());

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return updated ? NoContent() : NotFound("Home Not Found");
        }


        [AllowAnonymous]
        [HttpDelete("{homeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromRoute] int homeId)
        {
            bool deleted;
            try
            {
                deleted = _HomeService.Delete(homeId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return deleted ? NoContent() : NotFound("Home not found");
        }
    }
}
