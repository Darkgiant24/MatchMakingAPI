namespace PlayerMatchmakingAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        
        public required string Username { get; set; }
        public required string Password { get; set; }

        public int GamesWon { get; set; }
        public int CubesCleared { get; set; }
    }
}
