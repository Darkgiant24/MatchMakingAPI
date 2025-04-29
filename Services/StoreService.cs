using System;
using System.Collections.Generic;
using System.Linq;
using PlayerMatchmakingAPI.Models;

namespace PlayerMatchmakingAPI.Services
{
    public class StoreService
    {
        private readonly List<Cosmetic> _availableCosmetics;
        private readonly PlayerService _playerService;  

        public StoreService(PlayerService playerService)
        {
            _playerService = playerService;

            
            _availableCosmetics = new List<Cosmetic>
            {
                new Cosmetic { Id = 1, Name = "Cosmetic 1", Description = "A small cosmetic", Price = 10},
                new Cosmetic { Id = 2, Name = "Cosmetic 2", Description = "A cosmetic", Price = 50},
                new Cosmetic { Id = 3, Name = "Cosmetic 3", Description = "A huge cosmetic.", Price = 150}
            };
        }

        
        public List<Cosmetic> GetAvailableCosmetics()
        {
            return _availableCosmetics;
        }

        
        public string BuyCosmetic(int playerId, int cosmeticId)
        {
            var player = _playerService.GetPlayerById(playerId);  

            if (player == null)
            {
                return "Player not found.";
            }

            var cosmetic = _availableCosmetics.FirstOrDefault(c => c.Id == cosmeticId);

            if (cosmetic == null)
            {
                return "Cosmetic not found.";
            }

            
            if (player.Coins >= cosmetic.Price)
            {
                
                player.Coins -= cosmetic.Price;
                player.Cosmetics.Add(cosmetic.Name);

        
                
                return $"You have successfully purchased {cosmetic.Name}.";
            }
            else
            {
                return "Not enough coins to purchase this cosmetic.";
            }
        }
    }
}
