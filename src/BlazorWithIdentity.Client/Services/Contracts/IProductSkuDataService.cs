using BlazorWithIdentity.Shared.DTO.ProductSku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface IProductSkuDataService
    {
        Task CreateProductSku(SaveProductSkuDTO saveProductSkuDTO);

        Task UpdateProductSku(int id, SaveProductSkuDTO saveProductSkuDTO);

        Task<ProductSkuDTO[]> GetProductSkus();

        Task<ProductSkuDTO> GetProductSkuById(int id);

        Task DeleteProductSku(int id);
    }
}
