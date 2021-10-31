using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blazor.Domain.Models;
using BlazorWithIdentity.Shared.DTO;
using BlazorWithIdentity.Shared.DTO.Category;
using BlazorWithIdentity.Shared.DTO.CompositeProduct;
using BlazorWithIdentity.Shared.DTO.Option;
using BlazorWithIdentity.Shared.DTO.OptionValue;
using BlazorWithIdentity.Shared.DTO.Product;
using BlazorWithIdentity.Shared.DTO.ProductSku;
using BlazorWithIdentity.Shared.DTO.SkuValue;

namespace BlazorWithIdentity.Server.Mapping
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

            CreateMap<SkuValue, SaveSkuValueDTO>();
            CreateMap<SkuValue, SkuValueDTO>()
              .ForMember(s => s.OptionValue, opt => opt.MapFrom(p => p.OptionValue))
               .ForMember(s => s.SkuValueName, opt => opt.MapFrom(o => o.SkuValueName));

            CreateMap<ProductSku, SaveProductSkuDTO>();
            CreateMap<ProductSku, ProductSkuDTO>()
              .ForMember(s => s.SkuValue, opt => opt.MapFrom(p => p.SkuValue))
               .ForMember(s => s.Price, opt => opt.MapFrom(o => o.Price));

            CreateMap<CompositeProduct, SaveCompositeProductDTO>()
                 .ForMember(pr => pr.CombinedProducts, opt => opt.MapFrom(p => p.CombinedProducts.Select(ps => ps.ProductSkuId)))
                .ForMember(pr => pr.TotalPrice, opt => opt.MapFrom(p => p.TotalPrice));
               
            CreateMap<CompositeProduct, CompositeProductDTO>()
                
                .ForMember(pr => pr.TotalPrice, opt => opt.MapFrom(p => p.TotalPrice))
               
                .ForMember(pr => pr.CombinedProducts, opt => opt.MapFrom(p => p.CombinedProducts.Select(ps => new ProductSkuDTO
                { 
                    Id = ps.ProductSku.Id, Price = ps.ProductSku.Price, SkuValue = new SkuValueDTO 
                        { Id = ps.ProductSku.SkuValue.Id, SkuValueName = ps.ProductSku.SkuValue.SkuValueName, OptionValue = new OptionValueDTO 
                           { Id = ps.ProductSku.SkuValue.OptionValue.Id, OptionValueName = ps.ProductSku.SkuValue.OptionValue.OptionValueName, Option = new OptionDTO 
                              { Id = ps.ProductSku.SkuValue.OptionValue.Option.Id, OptionName = ps.ProductSku.SkuValue.OptionValue.Option.OptionName, Product = new ProductDTO 
                                    { Id = ps.ProductSku.SkuValue.OptionValue.Option.Product.Id, ProductName = ps.ProductSku.SkuValue.OptionValue.Option.Product.ProductName, ProductCategory = new KeyValuePairDTO 
                                      { Id = ps.ProductSku.SkuValue.OptionValue.Option.Product.ProductCategory.Id, Name =  ps.ProductSku.SkuValue.OptionValue.Option.Product.ProductCategory.Name } 
                } } } } })));

         


            //API DTO to Domain
            CreateMap<SaveProductCategoryDTO, ProductCategory>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.Name, opt => opt.MapFrom(pr => pr.Name));

            CreateMap<SaveProductDTO, Product>()
           .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.ProductName, opt => opt.MapFrom(pr => pr.ProductName));
            //.ForMember(p => p.ProductCategoryId, opt => opt.MapFrom(pr => pr.ProductCategoryId));

            CreateMap<SaveOptionDTO, Option>()
           .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<SaveOptionValueDTO, OptionValue>()
           .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<SaveSkuValueDTO, SkuValue>()
            .ForMember(p => p.Id, opt => opt.Ignore());

            CreateMap<SaveProductSkuDTO, ProductSku>()
            .ForMember(p => p.Id, opt => opt.Ignore());

            
            CreateMap<SaveCompositeProductDTO, CompositeProduct>()
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
