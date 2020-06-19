using FashionShop.Models;
using Newtonsoft.Json;

namespace FashionShop.DTOs
{
    public class indexProductDTO
    {   
        // public indexProductDTO(string name, decimal price, string image) {
        //     Name = name;
        //     Price = price;
        //     Image = image;
        // }
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int TypeId { get; set; } 

        public string Image { get; set; }
    }
}