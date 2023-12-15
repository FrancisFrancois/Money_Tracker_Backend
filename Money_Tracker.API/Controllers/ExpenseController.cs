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
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _ExpenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _ExpenseService = expenseService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        public IActionResult GetAll()
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetAll().Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("{expenseId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(ExpenseDTO))]

        public IActionResult GetById([FromRoute] int expenseId)
        {
            ExpenseDTO? expenseDTO = _ExpenseService.GetById(expenseId)?.ToDTO();
            if (expenseDTO is null)
            {
                return NotFound("Expense not found");
            }
            return Ok(expenseDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ExpenseDTO))]

        public IActionResult Insert([FromBody] ExpenseDataDTO expense)
        {
            ExpenseDTO result = _ExpenseService.Create(expense.ToModel()).ToDTO();
            return CreatedAtAction(nameof(GetById), new { expenseId = result.Id }, result);
        }


        [HttpPut("{expenseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]

        public IActionResult Update([FromRoute] int expenseId, [FromBody] ExpenseDataDTO expense)
        {
            bool updated;
            try
            {
                updated = _ExpenseService.Update(expenseId, expense.ToModel());
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            return updated ? NoContent() : NotFound("Expense not found");
        }

        [HttpDelete("{expenseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(409, Type = typeof(string))]
        [ProducesResponseType(400)]

        public IActionResult Delete([FromRoute] int expenseId)
        {
            bool deleted;
            try
            {
                deleted = _ExpenseService.Delete(expenseId);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return deleted ? NoContent() : NotFound("Expense not found");
        }
    }
}
