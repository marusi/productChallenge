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
using ChamaSubscription.Infrastructure.DTO.Product;

namespace ChamaSubAPI.Controllers
{
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public ProductController(ChamaSubscriptionDbContext context, IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var standardProducts = await context.Products.
                Include(p => p.ProductCategory).
                ToListAsync();

            return mapper.Map<List<Product>, List<ProductDTO>>(standardProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] SaveProductSkuDTO productResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<SaveProductSkuDTO, Product>(productResource);

          //  product.LastUpdate = DateTime.Now;

            repository.Add(product);
            await unitOfWork.CompleteAsync();



            product = await repository.GetProduct(product.Id);

            var result = mapper.Map<Product, ProductDTO>(product);
            return Ok(result);
        }

    }
}
