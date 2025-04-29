namespace PlayerMatchmakingAPI.Models
{
    public class Achievement
    {
        public required string Name { get; set; }
        public required string Description { get; set; }   
        public bool Unlocked { get; set; }
    }
}
