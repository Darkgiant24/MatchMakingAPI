namespace PlayerMatchmakingAPI.Models
{
    public class ServerInfo
    {
        public required string ServerIp { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
