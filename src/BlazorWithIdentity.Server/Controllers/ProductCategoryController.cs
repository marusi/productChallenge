using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using BlazorWithIdentity.Shared.DTO.Category;
using Blazor.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Server.Controllers
{
    [Route("/api/categories")]
    public class ProductCategoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductCategoryRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public ProductCategoryController(ApplicationDbContext context, IMapper mapper, IProductCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ProductCategoryDTO>> GetCategories()
        {
            var categories = await context.ProductCategories.ToListAsync();

            return mapper.Map<List<ProductCategory>, List<ProductCategoryDTO>>(categories);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProductCategory([FromBody] SaveProductCategoryDTO productCategoryResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productCategory = mapper.Map<SaveProductCategoryDTO, ProductCategory>(productCategoryResource);

          //  productCategory.LastUpdate = DateTime.Now;

            repository.Add(productCategory);
            await unitOfWork.CompleteAsync();



            productCategory = await repository.GetProductCategory(productCategory.Id);

            var result = mapper.Map<ProductCategory, ProductCategoryDTO>(productCategory);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await repository.GetProductCategory(id);

            if (category == null)
                return NotFound();
            var categoryDto = mapper.Map<ProductCategory, ProductCategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] SaveProductCategoryDTO saveProductCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await repository.GetProductCategory(id);

            if (category == null)
                return NotFound();

            mapper.Map<SaveProductCategoryDTO, ProductCategory>(saveProductCategoryDTO, category);
           

            await unitOfWork.CompleteAsync();

            category = await repository.GetProductCategory(category.Id);
            var result = mapper.Map<ProductCategory, SaveProductCategoryDTO>(category);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await repository.GetProductCategory(id, includeRelated: false);

            if (category == null)
                return NotFound();

            repository.Remove(category);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}
