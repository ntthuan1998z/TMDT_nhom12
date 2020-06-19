using System.ComponentModel.DataAnnotations.Schema;
using FashionShop.models;
using Newtonsoft.Json;

namespace FashionShop.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int Default { get; set; }
        public int Highlight { get; set; }

        public int ProductForeignKey { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductForeignKey")]
        public Product Product { get; set; }
    }
}