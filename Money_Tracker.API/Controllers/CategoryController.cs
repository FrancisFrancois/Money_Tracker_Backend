using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;

namespace Money_Tracker.API.Controllers
{
    // Déclare le contrôleur des catégories avec les routes API correspondantes
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // Référence au service de catégorie pour les opérations de gestion des catégories
        private readonly ICategoryService _CategoryService;

        // Constructeur pour injecter le service de catégorie
        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        // Route GET pour obtenir toutes les catégories
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]
        public IActionResult GetAll()
        {
            // Récupère toutes les catégories et les convertit en DTO pour la réponse
            IEnumerable<CategoryDTO> result = _CategoryService.GetAll().Select(c => c.ToDTO());
            return Ok(result);  // Renvoie une réponse HTTP 200 (OK) avec la liste des catégories.
        }

        // Route GET pour obtenir une catégorie par son ID
        [HttpGet("{categoryId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]
        public IActionResult GetById([FromRoute] int categoryId)
        {
            // Récupère la catégorie spécifiée et la convertit en DTO
            CategoryDTO? categoryDTO = _CategoryService.GetById(categoryId)?.ToDTO();
            if (categoryDTO is null)
            {
                return NotFound("Category not found");  // Renvoie une réponse HTTP 404 si la catégorie n'est pas trouvée.
            }
            return Ok(categoryDTO);  // Renvoie une réponse HTTP 200 avec les détails de la catégorie.
        }

        // Route POST pour insérer une nouvelle catégorie
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryDTO))]
        public IActionResult Insert([FromBody] CategoryDataDTO category)
        {
            // Crée une nouvelle catégorie et la convertit en DTO
            CategoryDTO result = _CategoryService.Create(category.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { CategoryId = result.Id }, result);  // Renvoie une réponse HTTP 201 (Created) avec la catégorie créée.
        }

        // Route PUT pour mettre à jour une catégorie
        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int categoryId, [FromBody] CategoryDataDTO category)
        {
            bool updated;
            try
            {
                // Tente de mettre à jour la catégorie
                updated = _CategoryService.Update(categoryId, category.ToModel());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);  // Renvoie une réponse HTTP 404 si la catégorie n'est pas trouvée.
            }
            return updated ? NoContent() : NotFound("Category not found");  // Renvoie une réponse HTTP 204 si la mise à jour a réussi, sinon 404.
        }

        // Route DELETE pour supprimer une catégorie
        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromRoute] int categoryId)
        {
            bool deleted;
            try
            {
                // Tente de supprimer la catégorie
                deleted = _CategoryService.Delete(categoryId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);  // Renvoie une réponse HTTP 404 si la catégorie n'est pas trouvée.
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  // Renvoie une réponse HTTP 400 en cas d'autres exceptions.
            }

            return deleted ? NoContent() : NotFound("Category not found");  // Renvoie une réponse HTTP 204 si la suppression a réussi, sinon 404.
        }
    }
}
