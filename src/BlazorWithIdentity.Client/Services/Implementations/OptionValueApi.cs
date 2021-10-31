using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.OptionValue;
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
    public class OptionValueApi : IOptionValueDataService
    {
        private readonly HttpClient _httpClient;

        public OptionValueApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string OptionValuesUrl = "/api/optionvalues";

        public async Task CreateOptionValue(SaveOptionValueDTO saveOptionValueDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveOptionValueDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(OptionValuesUrl, saveOptionValueDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<OptionValueDTO[]> GetOptionValues()
        {
            var result = await _httpClient.GetFromJsonAsync<OptionValueDTO[]>(OptionValuesUrl);
            return result;
        }

        public async Task<OptionValueDTO> GetOptionValueById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<OptionValueDTO>($"{OptionValuesUrl}/{id}");
            return result;
        }

        public async Task DeleteOptionValue(int id)
        {
            await _httpClient.DeleteAsync($"{OptionValuesUrl}/{id}");

        }

        public async Task UpdateOptionValue(int id, SaveOptionValueDTO saveOptionValueDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveOptionValueDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{OptionValuesUrl}/{id}", saveOptionValueDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
