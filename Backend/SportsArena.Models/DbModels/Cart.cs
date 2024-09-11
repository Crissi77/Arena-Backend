

using SportsArena.Models.DTOModels;

namespace SportsArena.Models.DbModels
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
      
    }
}
