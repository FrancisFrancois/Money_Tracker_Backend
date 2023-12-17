using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Money_Tracker.API.DTOs;
using Money_Tracker.API.Mappers;
using Money_Tracker.BLL.CustomExceptions;
using Money_Tracker.BLL.Interfaces;
using System.Globalization;

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

        private bool TryParseFrenchDate(string dateString, out DateTime date)
        {
            return DateTime.TryParseExact(dateString, "dd/MM/yyyy", new CultureInfo("fr-FR"), DateTimeStyles.None, out date);
        }


        [HttpGet("ExpensesByDay")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByDay([FromQuery] string dateString)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByDay(date).Select(e => e.ToDTO());
            return Ok(result);
        }


        [HttpGet("ExpensesByWeek")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        public IActionResult GetExpensesByWeek([FromQuery] DateTime date)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByWeek(date).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("ExpensesByMonth")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        public IActionResult GetExpensesByMonth([FromQuery] DateTime date)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByMonth(date).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("ExpensesByYear")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        public IActionResult GetExpensesByYear([FromQuery] DateTime date)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByYear(date).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("TotalExpenseByDay")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByDay([FromQuery] DateTime date)
        {
            double total = _ExpenseService.GetTotalExpensesByDay(date);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByWeek")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByWeek([FromQuery] DateTime date)
        {
            double total = _ExpenseService.GetTotalExpensesByWeek(date);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByMonth")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByMonth([FromQuery] DateTime date)
        {
            double total = _ExpenseService.GetTotalExpensesByMonth(date);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByYear")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByYear([FromQuery] DateTime date)
        {
            double total = _ExpenseService.GetTotalExpensesByYear(date);
            return Ok(total);
        }

        [HttpGet("ExpensesByCategoryByDay")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByCategoryByDay([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByDay(date, categoryId).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("ExpensesByCategoryByWeek")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByCategoryByWeek([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByWeek(date, categoryId).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("ExpensesByCategoryByMonth")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByCategoryByMonth([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByMonth(date, categoryId).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("ExpensesByCategoryByYear")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByCategoryByYear([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByYear(date, categoryId).Select(e => e.ToDTO());
            return Ok(result);
        }

        [HttpGet("TotalExpenseByCategoryByDay")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByCategoryByDay([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            double total = _ExpenseService.GetTotalExpensesByCategoryByDay(date, categoryId);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByCategoryByWeek")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByCategoryByWeek([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            double total = _ExpenseService.GetTotalExpensesByCategoryByWeek(date, categoryId);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByCategoryByMonth")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByCategoryByMonth([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            double total = _ExpenseService.GetTotalExpensesByCategoryByMonth(date, categoryId);
            return Ok(total);
        }

        [HttpGet("TotalExpenseByCategoryByYear")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByCategoryByYear([FromQuery] DateTime date, [FromQuery] int categoryId)
        {
            double total = _ExpenseService.GetTotalExpensesByCategoryByYear(date, categoryId);
            return Ok(total);
        }
    }
}

