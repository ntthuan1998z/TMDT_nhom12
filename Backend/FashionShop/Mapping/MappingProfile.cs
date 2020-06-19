using AutoMapper;
using FashionShop.DTOs;
using FashionShop.models;


namespace FashionShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, indexProductDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(p => (p.ListImages[0].Name)));
        }   
    }
}