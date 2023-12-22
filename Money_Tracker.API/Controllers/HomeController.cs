using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using System.Security.Claims;

namespace Money_Tracker.API.Controllers
{
    // Définit le contrôleur UserController avec les routes API
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // Déclare une instance du service home
        private readonly IHomeService _HomeService;

        // Constructeur pour injecter le service utilisateur
        public HomeController(IHomeService homeService)
        {
            _HomeService = homeService;
        }

        // Route GET pour obtenir tous les maisons
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HomeDTO>))]
        public IActionResult GetAll()
        {
            // Récupère tous les utilisateurs et les convertit en DTO
            IEnumerable<HomeDTO> result = _HomeService.GetAll().Select(h => h.ToDTO());

            // Renvoie une réponse HTTP 200 (OK) avec la liste des utilisateurs 
            return Ok(result);
        }

        // Route GET pour obtenir une maison par son ID
        [HttpGet("{homeId}")]
        [ProducesResponseType(200, Type = typeof(HomeFullDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetById([FromRoute] int homeId)
        {
            // Récupère l'utilisateur par ID et le convertit en DTO
            HomeFullDTO? result = _HomeService.GetById(homeId)?.ToFullDTO();

            if (result is null)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si aucune maison n'est trouvé.
                return NotFound("Track not found");
            }
            // Renvoie une réponse HTTP 200 (OK) avec les détails de la maison
            return Ok(result);
        }

        // Route POST pour créer une nouvelle maison
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(HomeDTO))]
        [ProducesResponseType(401)]

        public IActionResult Create([FromBody] HomeDataDTO home) 
        {
            // Vérifiez si l'utilisateur actuel a le rôle de manager
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (currentUserRole != "Manager")
            {
                return Unauthorized(new { Message = "Accès refusé. Seuls les managers peuvent créer des maisons." });
            }
            // Crée une maison et le convertit en DTO
            HomeDTO result = _HomeService.Create(home.ToModel()).ToDTO();

            // Renvoie une réponse HTTP 201 (Created) avec les détails de la maison
            return CreatedAtAction(nameof(GetById), new {homeId = result.Id}, result);
        }

        // Route PUT pour mettre à jour une maison
        [HttpPut("{homeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int homeId, [FromBody] HomeDataDTO home)
        {
            bool updated;
            try
            {
                // Tente de mettre à jour la maison
                updated = _HomeService.Update(homeId, home.ToModel());

            }
            catch (NotFoundException ex)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si la maison n'est pas trouvé
                return NotFound(ex.Message);
            }

            // Renvoie une réponse HTTP 204 (No Content) si la mise à jour a réussi, sinon 404 (Not Found).
            return updated ? NoContent() : NotFound("Home Not Found");
        }

        // Route DELETE pour supprimer une maison
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
                // Tente de supprimer la maison
                deleted = _HomeService.Delete(homeId);
            }
            catch (NotFoundException ex)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si la maison n'est pas trouvée
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Renvoie une sélection HTTP 400 (Bad Request)
                return BadRequest(ex.Message);
            }

            // Renvoie une sélection HTTP 204 (No Content) si la suppression a réussi, sinon 404 (Not Found).
            return deleted ? NoContent() : NotFound("Home not found");
        }
    }
}
