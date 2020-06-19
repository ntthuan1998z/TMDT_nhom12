using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FashionShop.Models;

namespace FashionShop.models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int TypeId { get; set; }    

        public List<ProductImage> ListImages { get; set; }

        public string Description { get; set; }

    }
}