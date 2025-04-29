namespace PlayerMatchmakingAPI.Models
{
    public class Cosmetic
    {
        public int Id { get; set; }  
        public required string Name { get; set; } 
        public required string Description { get; set; }  
        public required int Price { get; set; }  
       
    }
}
