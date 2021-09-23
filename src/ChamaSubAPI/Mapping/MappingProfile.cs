using AutoMapper;
using ChamaSubscription.Domain.Models;
using ChamaSubscription.Infrastructure;
using ChamaSubscription.Infrastructure.DTO;
using ChamaSubscription.Infrastructure.DTO.Category;
using ChamaSubscription.Infrastructure.DTO.CompositeProduct;
using ChamaSubscription.Infrastructure.DTO.Option;
using ChamaSubscription.Infrastructure.DTO.Product;
using ChamaSubscription.Infrastructure.DTO.ProductSku;
using ChamaSubscription.Infrastructure.DTO.SkuValue;
using System.Linq;

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
               .ForMember(pr => pr.Option, opt => opt.MapFrom(p => p.Option))
                .ForMember(pr => pr.OptionValueName, opt => opt.MapFrom(p => p.OptionValueName));
            CreateMap<SkuValue, SkuValueDTO>()
               .ForMember(s => s.OptionValue, opt => opt.MapFrom(p => p.OptionValue))
                .ForMember(s => s.SkuValueName, opt => opt.MapFrom(o => o.SkuValueName));
            CreateMap<ProductSku, ProductSkuDTO>()
              .ForMember(s => s.SkuValue, opt => opt.MapFrom(p => p.SkuValue))
               .ForMember(s => s.Price, opt => opt.MapFrom(o => o.Price));
            CreateMap<CompositeProduct, SaveCompositeProductDTO>()
                 .ForMember(pr => pr.CombinedProducts, opt => opt.MapFrom(p => p.CombinedProducts.Select(ps => ps.ProductSkuId)))
                .ForMember(pr => pr.TotalPrice, opt => opt.MapFrom(p => p.TotalPrice));
            CreateMap<CompositeProduct, CompositeProductDTO>()
                .ForMember(pr => pr.TotalPrice, opt => opt.MapFrom(p => p.TotalPrice))
                .ForMember(pr => pr.CombinedProducts, opt => opt.MapFrom(p => p.CombinedProducts.Select(ps => new ProductSkuDTO
                { Id = ps.ProductSku.Id, Price = ps.ProductSku.Price })));


            //API DTO to Domain
            CreateMap<SaveProductCategoryDTO, ProductCategory>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveProductDTO, Product>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveOptionDTO, Option>()
            .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveOptionValueDTO, OptionValue>()
           .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveSkuValueDTO, SkuValue>()
          .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveProductSkuDTO, ProductSku>()
        .ForMember(p => p.Id, opt => opt.Ignore());
            CreateMap<SaveCompositeProductDTO,CompositeProduct>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.TotalPrice, opt => opt.MapFrom(pr => pr.TotalPrice))
                .ForMember(p => p.CombinedProducts, opt => opt.Ignore())
               .AfterMap((ps, p) => {
                   // Remove unselected Products
                   var removedProducts = p.CombinedProducts.Where(s => !ps.CombinedProducts.Contains(s.ProductSkuId));
                   foreach (var s in removedProducts)
                       p.CombinedProducts.Remove(s);

                   // Add new Products
                   var addedProducts = ps.CombinedProducts.Where(id => !p.CombinedProducts.Any(s => s.ProductSkuId == id)).Select(id => new CombinedProduct { ProductSkuId = id });
                   foreach (var s in addedProducts)
                       p.CombinedProducts.Add(s);
               });

        }
    }
}
