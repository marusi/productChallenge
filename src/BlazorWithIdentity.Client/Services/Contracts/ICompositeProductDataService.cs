using BlazorWithIdentity.Shared.DTO.CompositeProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface ICompositeProductDataService
    {
        Task CreateCompositeProduct(SaveCompositeProductDTO saveCompositeProductDTO);

        Task UpdateCompositeProduct(int id, SaveCompositeProductDTO saveCompositeProductDTO);

        Task<CompositeProductDTO[]> GetCompositeProducts();

        Task<CompositeProductDTO> GetCompositeProductById(int id);

        Task DeleteCompositeProduct(int id);
    }
}
