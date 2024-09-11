

using SportsArena.Enums.Enum;

namespace SportsArena.Models.DTOModels
{
    public class productDto
    {
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; } 
      


    }
}

