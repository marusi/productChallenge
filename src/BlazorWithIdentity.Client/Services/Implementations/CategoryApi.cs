using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared;
using BlazorWithIdentity.Shared.DTO.Category;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Implementations
{
    public class CategoryApi : ICategoryDataService
    {
        private readonly HttpClient _httpClient;

        public CategoryApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string CategoriesUrl = "/api/categories";

        public async Task CreateCategory(SaveProductCategoryDTO saveProductCategoryDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductCategoryDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(CategoriesUrl, saveProductCategoryDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<ProductCategoryDTO[]> GetCategories()
        {
            var result = await _httpClient.GetFromJsonAsync<ProductCategoryDTO[]>(CategoriesUrl);
            return result;
        }

        public async Task<ProductCategoryDTO> GetCategoryById(int id)
        {
          
            var result = await _httpClient.GetFromJsonAsync<ProductCategoryDTO>($"{CategoriesUrl}/{id}");
            return result;
        }

        public async Task DeleteCategory(int id)
        {
            await _httpClient.DeleteAsync($"{CategoriesUrl}/{id}");

        }

        public async Task UpdateCategory(int id, ProductCategoryDTO saveProductCategoryDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveProductCategoryDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{CategoriesUrl}/{id}", saveProductCategoryDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
