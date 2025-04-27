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
                new Cosmetic { Id = 1, Name = "Golden Skin", Description = "A rare golden skin for your character.", Price = 100},
                new Cosmetic { Id = 2, Name = "Fire Emote", Description = "An emote with fire effects.", Price = 50},
                new Cosmetic { Id = 3, Name = "Rocket Backpack", Description = "A cool rocket-powered backpack.", Price = 150}
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

                
                // pour l'instnat il a juste dépensé son fric, mais il est pas conservé
                
                return $"You have successfully purchased {cosmetic.Name}.";
            }
            else
            {
                return "Not enough coins to purchase this cosmetic.";
            }
        }
    }
}
