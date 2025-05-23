using Microsoft.EntityFrameworkCore;
using PlayerMatchmakingAPI.Models;  

namespace PlayerMatchmakingAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }  
    }
}
