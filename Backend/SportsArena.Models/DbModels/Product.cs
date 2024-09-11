﻿

using SportsArena.Enums.Enum;

namespace SportsArena.Models.DbModels
{
    public class Product
    {
        
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public int StockQuantity { get; set; }
        public int UserId { get; set; }
        public String Category { get; set; } 
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


    }
}




