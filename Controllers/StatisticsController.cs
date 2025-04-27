using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerMatchmakingAPI.Services;  
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public StatisticsController(PlayerService playerService)
        {
            _playerService = playerService;
        }


        [HttpGet("statistics")]
        [Authorize]
        public IActionResult GetStatistics()
        {
            
            var username = User.Identity?.Name;  
            
        
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User is not authenticated");
            }

            var player = _playerService.GetPlayerByUsername(username);

            if (player == null)
            {
                return NotFound("Player not found");
            }

            return Ok(new
            {
                GamesWon = player.GamesWon,
                CubesCleared = player.CubesCleared,
                Coins = player.Coins,
                Cosmetics = player.Cosmetics
            });
        }

        [HttpGet("achievements")]
        [Authorize]
        public IActionResult GetAchievements()
        {
            var username = User.Identity?.Name;  
            
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("User is not authenticated");
            }
            var player = _playerService.GetPlayerByUsername(username);

            if (player == null)
                return NotFound("Player not found");

            var achievements = new[]
            {
                new Achievement { Name = "Won 10 games", Unlocked = player.GamesWon >= 10 },
                new Achievement { Name = "Cleared 50 cubes", Unlocked = player.CubesCleared >= 50 },
                new Achievement { Name = "Test", Unlocked= player.Username=="player1"}
            };

            return Ok(achievements);
        }
    }
}
