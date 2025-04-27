using Microsoft.AspNetCore.Mvc;
using PlayerMatchmakingAPI.Services;
using PlayerMatchmakingAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace PlayerMatchmakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreService _storeService;
        private readonly PlayerService _playerService;

        public StoreController(StoreService storeService, PlayerService playerService)
        {
            _storeService = storeService;
            _playerService = playerService;
        }

        
        [HttpGet("cosmetics")]
        public IActionResult GetAvailableCosmetics()
        {
            var cosmetics = _storeService.GetAvailableCosmetics();
            return Ok(cosmetics);
        }

        
        [HttpPost("buy")]
        [Authorize] 
        public IActionResult BuyCosmetic([FromBody] PurchaseRequest purchaseRequest)
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

            
            var result = _storeService.BuyCosmetic(player.Id, purchaseRequest.CosmeticId);



            if (result.StartsWith("You have successfully"))
            {
                return Ok(new { message = result });
            }
            else
            {
                return BadRequest(new { message = result });
            }
        }
    }

    
    public class PurchaseRequest
    {
        public int CosmeticId { get; set; }
    }
}
