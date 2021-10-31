using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.ProductSku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Implementations
{
    public class ProductSkuApi : IProductSkuDataService
    {
        private readonly HttpClient _httpClient;

        public ProductSkuApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string ProductSkuUrl = "/api/productattributes";

        public async Task CreateProductSku(SaveProductSkuDTO saveProductSkuDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductSkuDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(ProductSkuUrl, saveProductSkuDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<ProductSkuDTO[]> GetProductSkus()
        {
            var result = await _httpClient.GetFromJsonAsync<ProductSkuDTO[]>(ProductSkuUrl);
            return result;
        }

        public async Task<ProductSkuDTO> GetProductSkuById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<ProductSkuDTO>($"{ProductSkuUrl}/{id}");
            return result;
        }

        public async Task DeleteProductSku(int id)
        {
            await _httpClient.DeleteAsync($"{ProductSkuUrl}/{id}");

        }

        public async Task UpdateProductSku(int id, SaveProductSkuDTO saveProductSkuDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductSkuDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{ProductSkuUrl}/{id}", saveProductSkuDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
