using BlazorWithIdentity.Shared.DTO.SkuValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface ISkuValueDataService
    {
        Task CreateSkuValue(SaveSkuValueDTO saveSkuValueDTO);

        Task UpdateSkuValue(int id, SaveSkuValueDTO saveSkuValueDTO);

        Task<SkuValueDTO[]> GetSkuValues();

        Task<SkuValueDTO> GetSkuValueById(int id);

        Task DeleteSkuValue(int id);
    }
}
