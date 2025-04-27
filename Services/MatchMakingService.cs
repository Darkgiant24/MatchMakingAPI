using System;
using System.Collections.Generic;
using System.Linq;
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Services
{
    public class MatchmakingService
    {
        private readonly Dictionary<string, List<Player>> _servers = new Dictionary<string, List<Player>>();
        private const int MaxPlayersPerServer = 2;
        private const int MaxSkillLevelDifference = 2; 

       
        public string RegisterServer(string serverIp)
        {
            if (!_servers.ContainsKey(serverIp))
            {
                _servers[serverIp] = new List<Player>(); 
            }

            return serverIp;
        }

       
        public string JoinServer(Player player)
        {
            
            foreach (var server in _servers)
            {
                
                if (server.Value.Count < MaxPlayersPerServer)
                {
                    
                    var existingPlayer = server.Value.FirstOrDefault();
                    if (existingPlayer != null)
                    {
                        // A changer, ptet avec skilllevel a save sur le player?
                        if (Math.Abs(existingPlayer.GamesWon - player.GamesWon) > MaxSkillLevelDifference)
                        {
                            continue;
                        }
                    }

                    
                    server.Value.Add(player);
                    return server.Key; 
                }
            }

            
            return CreateAndJoinNewServer(player);
        }

        private string CreateAndJoinNewServer(Player player)
        {
           
            var newServerIp = "server" + Guid.NewGuid().ToString(); 

            
            _servers[newServerIp] = new List<Player> { player };

            return newServerIp; 
        }

        
        public Dictionary<string, List<Player>> GetServers()
        {
            return _servers;
        }
    }
}
