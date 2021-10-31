using BlazorWithIdentity.Shared.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Client.Services.Contracts
{
    public interface ICategoryDataService
    {
       // ProductCategoryDTO SavedCategory { get; set; }
        Task CreateCategory(SaveProductCategoryDTO saveProductCategoryDTO);

        Task UpdateCategory(int id, ProductCategoryDTO saveProductCategoryDTO); 

        Task<ProductCategoryDTO[]> GetCategories();

        Task<ProductCategoryDTO> GetCategoryById(int id);

         Task DeleteCategory(int id);
    }
}
