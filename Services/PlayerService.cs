using System.Collections.Generic;
using System.Linq;
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Services
{
    public class PlayerService
    {
        private readonly List<Player> players = new List<Player>
        {
            new Player { Id = 1, Username = "player1", Password = "password111", GamesWon = 10, CubesCleared = 50 },
            new Player { Id = 2, Username = "player2", Password = "password222", GamesWon = 20, CubesCleared = 70 },
            new Player { Id = 3, Username = "player3", Password = "password333", GamesWon = 20, CubesCleared = 70 },
            new Player { Id = 4, Username = "player4", Password = "password444", GamesWon = 10, CubesCleared = 50 },
            
        };

        
        public Player? GetPlayerByUsername(string username)
    {
        return players.FirstOrDefault(p => p.Username == username);
    }

        
        public Player? Authenticate(string username, string password)
        {
            return players.FirstOrDefault(p => p.Username == username && p.Password == password);
        }
    }
}
