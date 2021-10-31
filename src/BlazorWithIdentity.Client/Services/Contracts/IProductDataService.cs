using BlazorWithIdentity.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface IProductDataService
    {

        Task CreateProduct(SaveProductDTO saveProductDTO);

        Task UpdateProduct(int id, SaveProductDTO saveProductDTO);

        Task<ProductDTO[]> GetProducts();

        Task<ProductDTO> GetProductById(int id);

        Task DeleteProduct(int id);
    }
}
