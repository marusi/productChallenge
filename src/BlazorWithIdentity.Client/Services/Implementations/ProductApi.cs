using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.Product;
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
    public class ProductApi : IProductDataService
    {
        private readonly HttpClient _httpClient;

        public ProductApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string ProductsUrl = "/api/products";

        public async Task CreateProduct(SaveProductDTO saveProductDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(ProductsUrl, saveProductDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<ProductDTO[]> GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<ProductDTO[]>(ProductsUrl);
            return result;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<ProductDTO>($"{ProductsUrl}/{id}");
            return result;
        }

        public async Task DeleteProduct(int id)
        {
            await _httpClient.DeleteAsync($"{ProductsUrl}/{id}");

        }

        public async Task UpdateProduct(int id, SaveProductDTO saveProductDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{ProductsUrl}/{id}", saveProductDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
