using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PlayerMatchmakingAPI.Services;  // Ajoutez cette directive pour accéder à MatchmakingService
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchmakingController : ControllerBase
    {
        private readonly MatchmakingService _matchmakingService;

        public MatchmakingController(MatchmakingService matchmakingService)
        {
            _matchmakingService = matchmakingService;
        }

        [HttpPost("register")]
        public IActionResult RegisterServer([FromBody] ServerInfo serverInfo)
        {
            var serverIp = _matchmakingService.RegisterServer(serverInfo.ServerIp);
            return Ok(new { ServerIp = serverIp });
        }

        [HttpPost("join")]
        [Authorize]
        public IActionResult JoinMatchmaking([FromBody] PlayerRequest playerRequest)
        {
            var serverIp = _matchmakingService.GetAvailableServer();
            if (serverIp == null)
                return NotFound("No servers available");

            return Ok(new { ServerIp = serverIp });
        }
    }
}
