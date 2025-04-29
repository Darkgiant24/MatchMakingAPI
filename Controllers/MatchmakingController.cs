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

       
        [HttpPost("register")]
        public IActionResult RegisterServer([FromBody] ServerInfo serverInfo)
        {
            var serverIp = _matchmakingService.RegisterServer(serverInfo.ServerIp);
            Console.WriteLine("Server registered: " + serverIp);
            return Ok(new { serverIp });

        }

    
        [HttpPost("join")]
        [Authorize]  
        public IActionResult JoinServer()
        {
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

            
            var serverIp = _matchmakingService.JoinServer(player);

            if (serverIp == "No available servers.")
            {
                return BadRequest(new { message = "No available servers." });
            }

            return Ok(new { serverIp });
        }

        [HttpPost("leave")]
        [Authorize]
        public IActionResult LeaveServer()
        {
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

            var result = _matchmakingService.LeaveServer(player);
            if (!result)
            {
                return BadRequest(new { message = "Player is not in any server." });
            }

            return Ok(new { message = "Player successfully left the server." });
        }

        [HttpGet("server/{serverIp}/players")]
        [Authorize]
        public IActionResult GetPlayersInServer(string serverIp)
        {
            var players = _matchmakingService.GetPlayersInServer(serverIp);

            if (players == null || !players.Any())
            {
                return NotFound(new { message = "No players found in the server." });
            }

            return Ok(players);
        }

        
        [HttpGet("servers")]
        public IActionResult GetServers()
        {
            var servers = _matchmakingService.GetServers();
            return Ok(servers);
        }

        [HttpGet("player/server")]
        [Authorize]
        public IActionResult GetServerForPlayer()
        {
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

            var serverIp = _matchmakingService.GetServerForPlayer(player);
            if (serverIp == null)
            {
                return NotFound(new { message = "Player is not in any server." });
            }

            return Ok(new { serverIp });
        }
    }
}
