using AutoMapper;
using ChamaSubscription.Domain.Models;
using ChamaSubscription.Infrastructure;
using ChamaSubscription.Infrastructure.DTO;
using ChamaSubscription.Infrastructure.DTO.Category;
using ChamaSubscription.Infrastructure.DTO.Option;
using ChamaSubscription.Infrastructure.DTO.Product;


namespace ChamaSubAPI.Mapping
{
   public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API DTO
            CreateMap<ProductCategory, SaveProductCategoryDTO>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<ProductCategory, KeyValuePairDTO>();
            CreateMap<Product, SaveProductDTO>();
            CreateMap<Product, ProductDTO>()
               .ForMember(pr => pr.ProductCategory, opt => opt.MapFrom(p => p.ProductCategory))
                .ForMember(pr => pr.ProductName, opt => opt.MapFrom(p => p.ProductName));
            CreateMap<Option, SaveOptionDTO>();
            CreateMap<Option, OptionDTO>()
               .ForMember(pr => pr.Product, opt => opt.MapFrom(p => p.Product))
                .ForMember(pr => pr.OptionName, opt => opt.MapFrom(p => p.OptionName));
            CreateMap<OptionValue, SaveOptionValueDTO>();
            CreateMap<OptionValue, OptionValueDTO>()
              // .ForMember(pr => pr.Product, opt => opt.MapFrom(p => p.Product))
               .ForMember(pr => pr.Option, opt => opt.MapFrom(p => p.Option.Product))
                .ForMember(pr => pr.OptionValueName, opt => opt.MapFrom(p => p.OptionValueName));

            //API DTO to Domain
            CreateMap<SaveProductCategoryDTO, ProductCategory>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveProductDTO, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveOptionDTO, Option>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveOptionValueDTO, OptionValue>()
           .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
