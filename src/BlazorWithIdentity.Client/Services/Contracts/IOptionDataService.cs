using BlazorWithIdentity.Shared.DTO.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface IOptionDataService
    {
        // ProductCategoryDTO SavedCategory { get; set; }
        Task CreateOption(SaveOptionDTO saveOptionDTO);

        Task UpdateOption(int id, SaveOptionDTO saveOptionDTO);

        Task<OptionDTO[]> GetOptions();

        Task<OptionDTO> GetOptionById(int id);

        Task DeleteOption(int id);
    }
}
