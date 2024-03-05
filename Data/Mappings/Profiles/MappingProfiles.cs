using AutoMapper;
using SimpleEcommerce.Dto;
using SimpleEcommerce.Dto.Request;
using SimpleEcommerce.Dto.Response;
using SimpleEcommerce.Models;


namespace SimpleEcommerce.Data.Mappings.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // request
            CreateMap<BrandModel, BrandReqDto>();
            CreateMap<BrandReqDto, BrandModel>();

            CreateMap<CategoryModel, CategoryReqDto>();
            CreateMap<CategoryReqDto, CategoryModel>();
            
            CreateMap<ProductModel, ProductReqDto>();
            CreateMap<ProductReqDto, ProductModel>();

            CreateMap<ProductModel, ProductReqDto>();
            CreateMap<ProductReqDto, ProductModel>();
            
            CreateMap<SkuModel, SkuReqDto>();
            CreateMap<SkuReqDto, SkuModel>();

            CreateMap<SkuModel, SkuByProdReqDto>();
            CreateMap<SkuByProdReqDto, SkuModel>();

            //response
            CreateMap<BrandModel, BrandResDto>();
            CreateMap<BrandResDto, BrandModel>();

            CreateMap<CategoryModel, CategoryResDto>();
            CreateMap<CategoryResDto, CategoryModel>();
            
            CreateMap<ProductModel, ProductResDto>();
            CreateMap<ProductResDto, ProductModel>();

            CreateMap<ProductModel, ProductResDto>();
            CreateMap<ProductResDto, ProductModel>();
            
            CreateMap<SkuModel, SkuResDto>();
            CreateMap<SkuResDto, SkuModel>();
        }
    }
}