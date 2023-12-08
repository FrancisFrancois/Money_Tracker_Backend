using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
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
    }
}
