﻿using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        // Déclare une instance du service utilisateur
        private readonly IUserService _UserService;

        // Constructeur pour injecter le service utilisateur
        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        // Récuperation de l'id de l'utilisateur via les informations du token
        private int CurrentUserId => User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                                                       .Select(c => int.Parse(c.Value))
                                                       .SingleOrDefault(-1);

    
        // Route GET pour obtenir tous les utilisateurs
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserDTO>))]
        public IActionResult GetAll()
        {
            // Récupère tous les utilisateurs et les convertit en DTO
            IEnumerable<UserDTO> result = _UserService.GetAll().Select(u => u.ToDTO());

            // Renvoie une réponse HTTP 200 (OK) avec la liste des utilisateurs 
            return Ok(result);
        }

        // Route GET pour obtenir un utilisateur par son ID
        [HttpGet("{userId}")]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(UserDTO))]
        public IActionResult GetById([FromRoute] int userId)
        {
            // Récupère l'utilisateur par ID et le convertit en DTO
            UserDTO? userDTO = _UserService.GetById(userId)?.ToDTO();
            if (userDTO is null)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si aucun utilisateur n'est trouvé.
                return NotFound("User not found");
            }
            // Renvoie une réponse HTTP 200 (OK) avec les détails de l'utilisateur
            return Ok(userDTO);
        }

        // Route POST pour créer un nouvel utilisateur
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UserDTO))]
        public IActionResult Create([FromBody] UserDataDTO user)
        {
            // Crée un utilisateur et le convertit en DTO
            UserDTO result = _UserService.Create(user.ToModel()).ToDTO();

            // Renvoie une réponse HTTP 201 (Created) avec les détails de l'utilisateur créé.
            return CreatedAtAction(nameof(GetById), new { userId = result.Id }, result);
        }

        // Route PUT pour mettre à jour un utilisateur
        [HttpPut("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult Update([FromRoute] int userId, [FromBody] UserDataDTO user)
        {
            bool updated;
            try
            {
                // Tente de mettre à jour l'utilisateur
                updated = _UserService.Update(userId, user.ToModel());
            }
            catch (Exception ex)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si l'utilisateur n'est pas trouvé
                return NotFound(ex.Message);
            }

            // Renvoie une réponse HTTP 204 (No Content) si la mise à jour a réussi, sinon 404 (Not Found).
            return updated ? NoContent() : NotFound("User not found");
        }

        // Route DELETE pour supprimer un utilisateur
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
                // Tente de supprimer l'utilisateur
                deleted = _UserService.Delete(userId);
            }
            catch (NotFoundException ex)
            {
                // Renvoie une réponse HTTP 404 (Not Found) si l'utilisateur n'est pas trouvé.
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Renvoie une réponse HTTP 400 (Bad Request)
                return BadRequest(ex.Message); 
            }

            // Renvoie une réponse HTTP 204 (No Content) si la suppression a réussi, sinon 404 (Not Found).
            return deleted ? NoContent() : NotFound("User not found"); 
        }
    }
}
