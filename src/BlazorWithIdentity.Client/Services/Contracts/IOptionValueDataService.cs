using BlazorWithIdentity.Shared.DTO.OptionValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface IOptionValueDataService
    {
        Task CreateOptionValue(SaveOptionValueDTO saveOptionValueDTO);

        Task UpdateOptionValue(int id, SaveOptionValueDTO saveOptionValueDTO);

        Task<OptionValueDTO[]> GetOptionValues();

        Task<OptionValueDTO> GetOptionValueById(int id);

        Task DeleteOptionValue(int id);
    }
}
