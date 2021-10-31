using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.SkuValue;
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
    public class SkuValueApi : ISkuValueDataService

    {
        private readonly HttpClient _httpClient;

        public SkuValueApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string SkuValuesUrl = "/api/skuvalues";

        public async Task CreateSkuValue(SaveSkuValueDTO saveSkuValueDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveSkuValueDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(SkuValuesUrl, saveSkuValueDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<SkuValueDTO[]> GetSkuValues()
        {
            var result = await _httpClient.GetFromJsonAsync<SkuValueDTO[]>(SkuValuesUrl);
            return result;
        }

        public async Task<SkuValueDTO> GetSkuValueById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<SkuValueDTO>($"{SkuValuesUrl}/{id}");
            return result;
        }

        public async Task DeleteSkuValue(int id)
        {
            await _httpClient.DeleteAsync($"{SkuValuesUrl}/{id}");

        }

        public async Task UpdateSkuValue(int id, SaveSkuValueDTO saveSkuValueDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveSkuValueDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{SkuValuesUrl}/{id}", saveSkuValueDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
