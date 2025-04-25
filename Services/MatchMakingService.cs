namespace PlayerMatchmakingAPI.Services
{
    public class MatchmakingService
    {
        private readonly List<string> servers = new List<string>();

        public string RegisterServer(string serverIp)
        {
            servers.Add(serverIp);
            return serverIp;
        }

        public string? GetAvailableServer()
        {
            return servers.FirstOrDefault();
        }
    }
}
