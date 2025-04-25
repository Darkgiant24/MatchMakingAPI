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
                    server.Value.Add(player);
                    return server.Key; 
                }
            }

            
            return "No available servers.";
        }

        
        public Dictionary<string, List<Player>> GetServers()
        {
            return _servers;
        }
    }
}
