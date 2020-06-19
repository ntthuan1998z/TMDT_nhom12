using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FashionShop.Models;

namespace FashionShop.DTOs
{
    public class ProductDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }

        public decimal Price { get; set; }      

         public List<ProductImage> ListImages { get; set; }

         public string Description { get; set; }  
    }
}