using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Services
{
    public class MatchmakingService
    {
        private readonly Dictionary<string, List<Player>> _servers = new Dictionary<string, List<Player>>();
        
        
        private const int MaxPlayersPerServer = 4;
        private const int MaxSkillLevelDifference = 2; 

        

        public string RegisterServer(string serverIp)
        {
            if (!_servers.ContainsKey(serverIp))
            {
                _servers[serverIp] = new List<Player>(); 
            }
            Console.WriteLine("Server registered: " + serverIp);

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
                        if (Math.Abs(existingPlayer.GamesWon - player.GamesWon) > MaxSkillLevelDifference)
                        {
                            continue;
                        }
                    }

                    server.Value.Add(player);
                    Console.WriteLine("Server registered: " + server);
                    return server.Key; // Le joueur a rejoint un serveur
                }
            }

            // Aucun serveur disponible, ajouter le joueur à la file d'attente
        
            return "No server available";
        }


        public List<Player> GetPlayersInServer(string serverIp)
        {
            if (!_servers.ContainsKey(serverIp))
            {
                return null; 
            }

            return _servers[serverIp]; 
        }

        public bool LeaveServer(Player player)
        {
            foreach (var server in _servers)
            {
                if (server.Value.Contains(player))
                {
                    server.Value.Remove(player);
                    return true; 
                }
            }

            return false; 
        }

        public Dictionary<string, List<Player>> GetServers()
        {
            return _servers;
        }

        

        
        public string GetServerForPlayer(Player player)
        {
            foreach (var server in _servers)
            {
                if (server.Value.Contains(player))
                {
                    return server.Key; // Retourne l'IP du serveur où se trouve le joueur
                }
            }

            return null; // Le joueur n'est dans aucun serveur
        }
    }
}
