using AutoMapper;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Models;


namespace SimpleEcommerce.Data.Mappings.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BrandModel, BrandDto>();
            CreateMap<BrandDto, BrandModel>();
            CreateMap<CategoryModel, CategoryDto>();
            CreateMap<CategoryDto, CategoryModel>();
            CreateMap<ProductModel, ProductDto>();
            CreateMap<ProductDto, ProductModel>();
        }
    }
}