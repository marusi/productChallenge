using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ChamaSubscription.Domain.Services;
using ChamaSubscription.Infrastructure;
using ChamaSubscription.Infrastructure.DTO;
using ChamaSubscription.Domain.Models;
using ChamaSubscription.Infrastructure.DTO.Category;

namespace ChamaSubAPI.Controllers
{
    [Route("/api/categories")]
    public class ProductCategoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductCategoryRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public ProductCategoryController(ChamaSubscriptionDbContext context, IMapper mapper, IProductCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<ProductCategoryDTO>> GetCategories()
        {
            var categories = await context.ProductCategories.ToListAsync();

            return mapper.Map<List<ProductCategory>, List<ProductCategoryDTO>>(categories);
        }

        [HttpPost]
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
    }
}
