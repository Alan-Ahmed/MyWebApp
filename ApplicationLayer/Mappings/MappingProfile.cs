using ApplicationLayer.DTOs;
using AutoMapper;
using MyWebApp.DomainLayer.Entities;

namespace MyWebApp.ApplicationLayer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
