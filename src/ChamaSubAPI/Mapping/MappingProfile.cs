using AutoMapper;
using ChamaSubscription.Domain.Models;
using ChamaSubscription.Infrastructure;
using ChamaSubscription.Infrastructure.DTO;
using ChamaSubscription.Infrastructure.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
               .ForMember(pr => pr.ProductCategory, opt => opt.MapFrom(p => p.ProductCategory));

            //API DTO to Domain
            CreateMap<SaveProductCategoryDTO, ProductCategory>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveProductDTO, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore());
        }
    }
}
