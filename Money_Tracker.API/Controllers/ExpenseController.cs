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
        // Référence au service de gestion des dépenses
        private readonly IExpenseService _ExpenseService;

        // Constructeur pour injecter le service des dépenses
        public ExpenseController(IExpenseService expenseService)
        {
            _ExpenseService = expenseService;
        }

        // Route GET pour obtenir toutes les dépenses
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetAll()
        {
            // Récupère toutes les dépenses et les convertit en DTO
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetAll().Select(e => e.ToDTO());

            // Renvoie une réponse HTTP 200 (OK) avec la liste des maisons
            return Ok(result);
        }

        // Route GET pour obtenir une dépense par son ID
        [HttpGet("{expenseId}")]
        [ProducesResponseType(200, Type = typeof(ExpenseDTO))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult GetById([FromRoute] int expenseId)
        {
            // Récupere une dépense par son ID et la convertit en DTO
            ExpenseDTO? expenseDTO = _ExpenseService.GetById(expenseId)?.ToDTO();
            if (expenseDTO is null)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si aucune dépense n'est trouvé.
                return NotFound("Expense not found");
            }
            // Renvoie une réponse HTTP 200 (OK) avec les détails de la dépense
            return Ok(expenseDTO);
        }

        // Route POST pour créer un nouvelle dépense
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ExpenseDTO))]
        public IActionResult Insert([FromBody] ExpenseDataDTO expense)
        {
            // Crée une nouvelle dépense et la convertit en DTO
            ExpenseDTO result = _ExpenseService.Create(expense.ToModel()).ToDTO();

            // Renvoie une réponse HTTP 201 (Created) avec les détails de la dépense créé.
            return CreatedAtAction(nameof(GetById), new { expenseId = result.Id }, result);
        }

        // Route PUT pour mettre à jour un utilisateur
        [HttpPut("{expenseId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int expenseId, [FromBody] ExpenseDataDTO expense)
        {
            bool updated;
            try
            {
                // Tente de mettre à jour la dépense
                updated = _ExpenseService.Update(expenseId, expense.ToModel()); 
            }
            catch (NotFoundException ex)
            {
                // Retourne une réponse HTTP 404 (Not Found) si la dépense n'est pas trouvée
                return NotFound(ex.Message);
            }

            // Renvoie une réponse HTTP 204 (No Content) si la suppression a réussi, sinon 404 (Not Found).
            return updated ? NoContent() : NotFound("Expense not found"); 
        }

        // Route DELETE pour supprimer un utilisateur
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
                // Tente de supprimer la dépense
                deleted = _ExpenseService.Delete(expenseId);
            }
            catch (NotFoundException ex)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si la dépense n'est pas trouvée
                return NotFound(ex.Message); 
            }
            catch (Exception ex)
            {
                // Renvoie une réponse HTTP 400 (Bad Request)
                return BadRequest(ex.Message); 
            }

            // Renvoie une réponse HTTP 204 (No Content) si la suppression a réussi, sinon 404 (Not Found).
            return deleted ? NoContent() : NotFound("Expense not found");
        }

        // Méthode privée pour analyser une date en format français
        private bool TryParseFrenchDate(string dateString, out DateTime date)
        {
            // Tente de convertir une chaîne de caractères en date en utilisant le format français 'dd/MM/yyyy'
            // 'dateString' est la chaîne de caractères à convertir
            // 'date' est la variable de sortie qui contiendra la date convertie si la conversion réussit
            // 'new CultureInfo("fr-FR")' spécifie le contexte culturel pour l'interprétation de la date, ici le format français
            // 'DateTimeStyles.None' indique qu'aucun style spécifique n'est utilisé pour analyser la chaîne de caractères
            // La méthode renvoie 'true' si la conversion est réussie, sinon 'false'
            return DateTime.TryParseExact(dateString, "dd/MM/yyyy", new CultureInfo("fr-FR"), DateTimeStyles.None, out date);
        }

        #region Unused
        //[HttpGet("ExpensesByDay")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        //public IActionResult GetExpensesByDay([FromQuery] string dateString)
        //{
        //    if (!TryParseFrenchDate(dateString, out DateTime date))
        //    {
        //        return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
        //    }

        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByDay(date).Select(e => e.ToDTO());
        //    return Ok(result);
        //}


        //[HttpGet("ExpensesByWeek")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        //public IActionResult GetExpensesByWeek([FromQuery] DateTime date)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByWeek(date).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("ExpensesByMonth")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        //public IActionResult GetExpensesByMonth([FromQuery] DateTime date)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByMonth(date).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("ExpensesByYear")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]

        //public IActionResult GetExpensesByYear([FromQuery] DateTime date)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByYear(date).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("TotalExpenseByDay")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByDay([FromQuery] DateTime date)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByDay(date);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByWeek")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByWeek([FromQuery] DateTime date)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByWeek(date);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByMonth")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByMonth([FromQuery] DateTime date)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByMonth(date);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByYear")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByYear([FromQuery] DateTime date)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByYear(date);
        //    return Ok(total);
        //}

        //[HttpGet("ExpensesByCategoryByDay")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        //public IActionResult GetExpensesByCategoryByDay([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByDay(date, categoryId).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("ExpensesByCategoryByWeek")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        //public IActionResult GetExpensesByCategoryByWeek([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByWeek(date, categoryId).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("ExpensesByCategoryByMonth")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        //public IActionResult GetExpensesByCategoryByMonth([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByMonth(date, categoryId).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("ExpensesByCategoryByYear")]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        //public IActionResult GetExpensesByCategoryByYear([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByCategoryByYear(date, categoryId).Select(e => e.ToDTO());
        //    return Ok(result);
        //}

        //[HttpGet("TotalExpenseByCategoryByDay")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByCategoryByDay([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByCategoryByDay(date, categoryId);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByCategoryByWeek")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByCategoryByWeek([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByCategoryByWeek(date, categoryId);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByCategoryByMonth")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByCategoryByMonth([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByCategoryByMonth(date, categoryId);
        //    return Ok(total);
        //}

        //[HttpGet("TotalExpenseByCategoryByYear")]
        //[ProducesResponseType(200, Type = typeof(double))]
        //[ProducesResponseType(400)]
        //public IActionResult GetTotalExpensesByCategoryByYear([FromQuery] DateTime date, [FromQuery] int categoryId)
        //{
        //    double total = _ExpenseService.GetTotalExpensesByCategoryByYear(date, categoryId);
        //    return Ok(total);
        //}
        #endregion

        // Route GET pour obtenir les dépenses par jour
        [HttpGet("ExpensesByDay")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByDay([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            // Vérifie si la date est au bon format
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                // Renvoie une erreur si le format de la date est invalide
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Récupère les dépenses pour le jour spécifié et les convertit en DTO, , filtrées éventuellement par maison, utilisateur ou catégorie
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByDay(date, homeId, userId, categoryId).Select(e => e.ToDTO());

            // Vérifie si des dépenses ont été trouvées
            if (!result.Any())
            {
                // Renvoie une réponse HTTP 404 (Not Found) si aucune dépenses n'est trouvé.
                return NotFound("No expenses found for the specified day.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le détail des dépenses par jour
            return Ok(result);
        }

        // Route GET pour obtenir les dépenses par semaine
        [HttpGet("ExpensesByWeek")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByWeek([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Récupère les dépenses pour la semaine spécifié et les convertit en DTO, , filtrées éventuellement par maison, utilisateur ou catégorie
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByWeek(date, homeId, userId, categoryId).Select(e => e.ToDTO());

            // Vérifie si des dépenses ont été trouvées
            if (!result.Any())
            {
                // Renvoie une réponse HTTP 404 (Not Found) si aucune dépenses n'est trouvé.
                return NotFound("No expenses found for the specified week.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le détail des dépenses par semaine
            return Ok(result);
        }

        // Route GET pour obtenir les dépenses par mois
        [HttpGet("ExpensesByMonth")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByMonth([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Récupère les dépenses pour le mois spécifié et les convertit en DTO, , filtrées éventuellement par maison, utilisateur ou catégorie
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByMonth(date, homeId, userId, categoryId).Select(e => e.ToDTO());

            // Vérifie si des dépenses ont été trouvées
            if (!result.Any())
            {
                // Renvoie une sélection HTTP 404 (Not Found) si aucune dépenses n'est trouvé.
                return NotFound("No expenses found for the specified month.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le détail des dépenses par mois
            return Ok(result);
        }

        // Route GET pour obtenir les dépenses par an
        [HttpGet("ExpensesByYear")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseDTO>))]
        public IActionResult GetExpensesByYear([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Récupère les dépenses pour l'annee spécifié et les convertit en DTO, , filtrées éventuellement par maison, utilisateur ou catégorie
            IEnumerable<ExpenseDTO> result = _ExpenseService.GetExpensesByYear(date, homeId, userId, categoryId).Select(e => e.ToDTO());

            // Vérifie si des dépenses ont été trouvées
            if (!result.Any())
            {
                return NotFound("No expenses found for the specified year.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le détail des dépenses par an
            return Ok(result);
        }

        // Route GET pour obtenir le total des dépenses par jour
        [HttpGet("TotalExpenseByDay")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByDay([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Calcul les dépenses pour la semaine donnée, filtrées éventuellement par maison, utilisateur ou catégorie
            double total = _ExpenseService.GetTotalExpensesByDay(date, homeId, userId, categoryId);

            // Vérifie si des dépenses ont été trouvées
            if (total == 0)
            {
                // Renvoie une sélection HTTP 200 (OK) avec le total des dépenses par jour
                return NotFound("No expenses found for the specified day.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le total des dépenses par jour
            return Ok(total);
        }

        // Route GET pour obtenir le total des dépenses par semaine
        [HttpGet("TotalExpenseByWeek")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByWeek([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Calcul les dépenses pour la semaine donnée, filtrées éventuellement par maison, utilisateur ou catégorie
            double total = _ExpenseService.GetTotalExpensesByWeek(date, homeId, userId, categoryId);

            // Vérifie si le total calculé est de 0, ce qui signifie qu'aucune dépense n'a été trouvée
            if (total == 0)
            {
                // Renvoie une sélection HTTP 200 (OK) avec le total des dépenses par jour
                return NotFound("No expenses found for the specified week.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le total des dépenses par semaine
            return Ok(total);
        }

        // Route GET pour obtenir le total des dépenses par mois
        [HttpGet("TotalExpenseByMonth")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByMonth([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Calcul les dépenses pour le mois donné, filtrées éventuellement par maison, utilisateur ou catégorie
            double total = _ExpenseService.GetTotalExpensesByMonth(date, homeId, userId, categoryId);

            // Vérifie si le total calculé est de 0, ce qui signifie qu'aucune dépense n'a été trouvée
            if (total == 0)
            {
                // Renvoie une sélection HTTP 200 (OK) avec le total des dépenses par jour
                return NotFound("No expenses found for the specified month.");
            }

            // Renvoie une réponse HTTP 200 (OK) avec le total des dépenses par semaine
            return Ok(total);
        }

        // Route GET pour obtenir le total des dépenses par an
        [HttpGet("TotalExpenseByYear")]
        [ProducesResponseType(200, Type = typeof(double))]
        [ProducesResponseType(400)]
        public IActionResult GetTotalExpensesByYear([FromQuery] string dateString, [FromQuery] int? homeId, [FromQuery] int? userId, [FromQuery] int? categoryId)
        {
            if (!TryParseFrenchDate(dateString, out DateTime date))
            {
                return BadRequest("Invalid date format. Please use 'dd/MM/yyyy'.");
            }

            // Calcul les dépenses pour l'annee donnée, filtrées éventuellement par maison, utilisateur ou catégorie
            double total = _ExpenseService.GetTotalExpensesByYear(date, homeId, userId, categoryId);

            // Vérifie si le total calculé est de 0, ce qui signifie qu'aucune dépense n'a été trouvée
            if (total == 0)
            {
                // Renvoie une sélection HTTP 200 (OK) avec le total des dépenses par an
                return NotFound("No expenses found for the specified year.");
            }

            // Renvoie une sélection HTTP 200 (OK) avec le total des dépenses par an
            return Ok(total);
        }
    }
}

