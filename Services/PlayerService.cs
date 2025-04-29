using System.Collections.Generic;
using System.Linq;
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Services
{
    public class PlayerService
    {
        private readonly List<Player> players = new List<Player>
        {
            new Player { Id = 1, Username = "player1", Password = "password111", GamePlayed= 15, GamesWon = 10, CubesCleared = 50, Coins = 200},
            new Player { Id = 2, Username = "player2", Password = "password222", GamePlayed= 25, GamesWon = 20, CubesCleared = 70, Coins = 50 },
            new Player { Id = 3, Username = "player3", Password = "password333", GamePlayed= 15, GamesWon = 10, CubesCleared = 50, Coins = 200 },
            new Player { Id = 4, Username = "player4", Password = "password444", GamePlayed= 25, GamesWon = 20, CubesCleared = 70, Coins = 50 },
            new Player { Id = 5, Username = "player5", Password = "password555", GamePlayed= 15, GamesWon = 10, CubesCleared = 50, Coins = 200 },
            new Player { Id = 6, Username = "player6", Password = "password666", GamePlayed= 25, GamesWon = 20, CubesCleared = 70, Coins = 50 },
            new Player { Id = 7, Username = "player7", Password = "password777", GamePlayed= 15, GamesWon = 10, CubesCleared = 50, Coins = 200 },
            new Player { Id = 8, Username = "player8", Password = "password888", GamePlayed= 25, GamesWon = 20, CubesCleared = 70, Coins = 50 }
        };


       
        public Player GetPlayerById(int playerId)
        {
            return players.FirstOrDefault(p => p.Id == playerId);
        }

        
        public Player GetPlayerByUsername(string username)
{
    Console.WriteLine($"Searching for player with username: {username}");
    var player = players.FirstOrDefault(p => p.Username == username);

    if (player == null)
    {
        Console.WriteLine("Player not found");
    }
    else
    {
        Console.WriteLine($"Player found: {player.Username}");
    }

    return player;
}

        
        public Player Authenticate(string username, string password)
        {
            return players.FirstOrDefault(p => p.Username == username && p.Password == password);
        }

        
    }
}
