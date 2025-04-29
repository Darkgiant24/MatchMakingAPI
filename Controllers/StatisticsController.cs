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
    foreach (var header in Request.Headers)
    {
        Console.WriteLine($"{header.Key}: {header.Value}");
    }

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
        GamesPlayed = player.GamePlayed,
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
                new Achievement { Name = "First Step", Description= "Play 1 game", Unlocked = player.GamePlayed >= 1 },
                new Achievement { Name = "Casual Player", Description= "Play 10 game", Unlocked = player.GamePlayed >= 10 },
                new Achievement { Name = "Pro Player", Description= "Play 50 game", Unlocked = player.GamePlayed >= 50 },
                new Achievement { Name = "First Victory", Description= "Win 1 game", Unlocked = player.GamesWon >= 1 },
                new Achievement { Name = "Champion", Description= "Win 10 game", Unlocked = player.GamesWon >= 1 },
                new Achievement { Name = "Master of the Game", Description= "Win 50 game", Unlocked = player.GamesWon >= 1 },
                new Achievement { Name = "Cube Breaker",  Description= "Clear 50 cubes", Unlocked = player.CubesCleared >= 50 },
                new Achievement { Name = "Cube Master",  Description= "Clear 200 cubes", Unlocked = player.CubesCleared >= 200 },
                new Achievement { Name = "Cube Overlord",  Description= "Clear 1000 cubes", Unlocked = player.CubesCleared >= 1000 },
                new Achievement { Name = "Shoper's Delight",  Description= "Buy a cosmetic", Unlocked = player.Cosmetics.Count >= 1 },
                new Achievement { Name = "Loser",  Description= "Have a low winrate", Unlocked= player.GamesWon/player.GamePlayed<0.2}
            };

            return Ok(achievements);
        }

        [HttpPut("statistics/update")]
        [Authorize]
        public IActionResult UpdateStatistics([FromBody] UpdateStatisticsRequest request)
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

            player.CubesCleared += request.CubesCleared;
            player.GamesWon += request.GamesWon;
            player.Coins += request.Coins;
            player.GamePlayed += 1;

            return Ok(new { message = "Statistics updated successfully." });
        }
    }
}
