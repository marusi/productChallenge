using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.CompositeProduct;
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
    public class CompositeProductApi : ICompositeProductDataService
    {
        private readonly HttpClient _httpClient;

        public CompositeProductApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string CompositeProductsUrl = "/api/compositeproducts";

        public async Task CreateCompositeProduct(SaveCompositeProductDTO saveCompositeProductDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveCompositeProductDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(CompositeProductsUrl, saveCompositeProductDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<CompositeProductDTO[]> GetCompositeProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<CompositeProductDTO[]>(CompositeProductsUrl);
            return result;
        }

        public async Task<CompositeProductDTO> GetCompositeProductById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<CompositeProductDTO>($"{CompositeProductsUrl}/{id}");
            return result;
        }

        public async Task DeleteCompositeProduct(int id)
        {
            await _httpClient.DeleteAsync($"{CompositeProductsUrl}/{id}");

        }

        public async Task UpdateCompositeProduct(int id, SaveCompositeProductDTO saveCompositeProductDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveCompositeProductDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{CompositeProductsUrl}/{id}", saveCompositeProductDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
