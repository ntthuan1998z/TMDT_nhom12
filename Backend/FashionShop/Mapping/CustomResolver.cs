using AutoMapper;
using FashionShop.DTOs;
using FashionShop.models;

namespace FashionShop.Mapping
{
    public class CustomResolver : IValueResolver<Product, indexProductDTO, string>
    {
        public string Resolve(Product source, indexProductDTO destination, string destMember, ResolutionContext context)
        {
            return source.ListImages[0].Name;
        }
    }
}