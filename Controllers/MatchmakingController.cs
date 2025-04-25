using Microsoft.AspNetCore.Mvc;
using PlayerMatchmakingAPI.Services;
using PlayerMatchmakingAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlayerMatchmakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchmakingController : ControllerBase
    {
        private readonly MatchmakingService _matchmakingService;
        private readonly PlayerService _playerService;

        public MatchmakingController(MatchmakingService matchmakingService, PlayerService playerService)
        {
            _matchmakingService = matchmakingService;
            _playerService = playerService;
        }

        // Endpoint pour enregistrer un serveur
        [HttpPost("register")]
        public IActionResult RegisterServer([FromBody] ServerInfo serverInfo)
        {
            var serverIp = _matchmakingService.RegisterServer(serverInfo.ServerIp);
            return Ok(new { serverIp });
        }

        // Endpoint pour rejoindre un serveur
        [HttpPost("join")]
        [Authorize]  // Assurez-vous que l'utilisateur est authentifié avec JWT
        public IActionResult JoinServer([FromBody] PlayerRequest playerRequest)
        {
            // Récupérer l'utilisateur authentifié à partir du service PlayerService
            var username = User.Identity?.Name; 
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User is not authenticated");
            }
            var player = _playerService.GetPlayerByUsername(username);

            if (player == null)
            {
                return Unauthorized(new { message = "Player not found." });
            }

            // Le joueur rejoint un serveur
            var serverIp = _matchmakingService.JoinServer(player);

            if (serverIp == "No available servers.")
            {
                return BadRequest(new { message = "No available servers." });
            }

            return Ok(new { serverIp });
        }
    }
}
