using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;  // Ajoutez cette directive pour accéder à Claim
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using PlayerMatchmakingAPI.Services;
using PlayerMatchmakingAPI.Models;  // Assurez-vous d'importer le modèle LoginRequest

namespace PlayerMatchmakingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public AuthController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var player = _playerService.Authenticate(loginRequest.Username, loginRequest.Password);

            if (player == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateJwtToken(player.Username);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Augmenter la taille de la clé à 16 caractères (128 bits)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_128_bits_secret_key_here_1234"));  // 128 bits = 16 caractères minimum
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "server",
                audience: "client",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
