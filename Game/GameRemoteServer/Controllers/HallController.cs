using GameRemoteServer.Entities;
using GameRemoteServer.Models;
using GameRemoteServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameRemoteServer.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallService hallService;
        private readonly IUserService userService;

        public HallController(IHallService hallService, IUserService userService)
        {
            this.hallService = hallService;
            this.userService = userService;
        }

        /// <summary>
        /// Get a list of active open lobbies by page.
        /// <code>GET: api/Hall/{page?}</code>
        /// </summary>
        [HttpGet("{page:int?}")]
        public IActionResult Index([FromRoute] int page = 1)
        {
            if(page < 1) return NotFound();
            var response = hallService.GetLobbies(page);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewLobby([FromBody] ReqCreateLobbyDTO request)
        {
            long hostId = long.Parse(User.Claims.First(c => c.Type == "Id").Value);
            User host = await userService.GetUserById(hostId);

            var response = hallService.CreateLobby(request, host);

            var newAddress = Request.Path + $"/{response.Id}";
            return Created(newAddress, response);
        }

        /// <summary>
        /// Get an open lobby by its Id.
        /// <code>GET: api/Hall/Lobby?{id}</code>
        /// </summary>
        [HttpGet("Lobby")]
        public IActionResult GetLobbyById([FromQuery(Name = "Id")][Required] long id)
        {
            var lobby = hallService.GetLobby(id);
            if(lobby != null) return Ok(lobby);
            else return NotFound();
        }
    }
}