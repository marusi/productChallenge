using BlazorWithIdentity.Client.Services.Contracts;
using BlazorWithIdentity.Shared.DTO.Option;
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
    public class OptionApi : IOptionDataService
    {
        private readonly HttpClient _httpClient;

        public OptionApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        string OptionsUrl = "/api/options";

        public async Task CreateOption(SaveOptionDTO saveOptionDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveOptionDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsJsonAsync(OptionsUrl, saveOptionDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }

        public async Task<OptionDTO[]> GetOptions()
        {
            var result = await _httpClient.GetFromJsonAsync<OptionDTO[]>(OptionsUrl);
            return result;
        }

        public async Task<OptionDTO> GetOptionById(int id)
        {

            var result = await _httpClient.GetFromJsonAsync<OptionDTO>($"{OptionsUrl}/{id}");
            return result;
        }

        public async Task DeleteOption(int id)
        {
            await _httpClient.DeleteAsync($"{OptionsUrl}/{id}");

        }

        public async Task UpdateOption(int id, SaveOptionDTO saveOptionDTO)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(saveOptionDTO), Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsJsonAsync($"{OptionsUrl}/{id}", saveOptionDTO);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
        }
    }
}
