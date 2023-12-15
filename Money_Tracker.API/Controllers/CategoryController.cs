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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _CategoryService;

        public CategoryController(ICategoryService CategoryService)
        {
            _CategoryService = CategoryService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDTO>))]

        public IActionResult GetAll()
        {
            IEnumerable<CategoryDTO> result = _CategoryService.GetAll().Select(c => c.ToDTO());
            return Ok(result);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(CategoryDTO))]

        public IActionResult GetById([FromRoute] int categoryId)
        {
            CategoryDTO? categoryDTO = _CategoryService.GetById(categoryId)?.ToDTO();
            if (categoryDTO is null)
            {
                return NotFound("Category not found");
            }
            return Ok(categoryDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryDTO))]

        public IActionResult Insert([FromBody] CategoryDataDTO category)
        {
            CategoryDTO result = _CategoryService.Create(category.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { CategoryId = result.Id }, result);
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]

        public IActionResult Update([FromRoute] int categoryId, [FromBody] CategoryDataDTO category)
        {
            bool updated;
            try
            {
                updated = _CategoryService.Update(categoryId, category.ToModel());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return updated ? NoContent() : NotFound("Category not found");

        }

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
                deleted = _CategoryService.Delete(categoryId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return deleted ? NoContent() : NotFound("Category not found");
        }
    }
}
