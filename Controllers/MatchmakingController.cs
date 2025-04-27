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
            return Ok(new { serverIp });
        }

    
        [HttpPost("join")]
        [Authorize]  
        public IActionResult JoinServer()
        {
            var username = User.Identity.Name;  
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
    }
}
