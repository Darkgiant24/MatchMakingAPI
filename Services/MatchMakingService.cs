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
        private const int MaxSkillLevelDifference = 2; // Différence maximale de skillLevel autorisée

        // Enregistrer un serveur
        public string RegisterServer(string serverIp)
        {
            if (!_servers.ContainsKey(serverIp))
            {
                _servers[serverIp] = new List<Player>(); // Initialise une liste vide pour les joueurs
            }

            return serverIp;
        }

        // Ajouter un joueur à un serveur
        public string JoinServer(Player player)
        {
            // Chercher un serveur où il y a de la place
            foreach (var server in _servers)
            {
                // Si le serveur a moins de 2 joueurs
                if (server.Value.Count < MaxPlayersPerServer)
                {
                    // Vérifier si le serveur a déjà un joueur
                    var existingPlayer = server.Value.FirstOrDefault();
                    if (existingPlayer != null)
                    {
                        // Vérifier la différence de skillLevel entre les joueurs
                        if (Math.Abs(existingPlayer.GamesWon - player.GamesWon) > MaxSkillLevelDifference)
                        {
                            // Si la différence de niveau est trop grande, essayez un autre serveur
                            continue;
                        }
                    }

                    // Si on est ici, c'est qu'on peut ajouter le joueur au serveur
                    server.Value.Add(player);
                    return server.Key; // Retourne l'IP du serveur où le joueur a été ajouté
                }
            }

            // Si aucun serveur n'a de place, on retourne une erreur ou un message spécifique
            return "No available servers.";
        }

        // Récupérer les serveurs et leurs joueurs
        public Dictionary<string, List<Player>> GetServers()
        {
            return _servers;
        }
    }
}
