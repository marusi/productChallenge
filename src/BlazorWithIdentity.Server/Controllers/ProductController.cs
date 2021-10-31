using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using Blazor.Domain.Models;
using BlazorWithIdentity.Shared.DTO.Product;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Server.Controllers
{
    [Route("/api/products")]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public ProductController(ApplicationDbContext context, IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
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
        public async Task<IActionResult> CreateProduct([FromBody] SaveProductDTO productResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<SaveProductDTO, Product>(productResource);

          //  product.LastUpdate = DateTime.Now;

            repository.Add(product);
            await unitOfWork.CompleteAsync();



            product = await repository.GetProduct(product.Id);

            var result = mapper.Map<Product, ProductDTO>(product);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await repository.GetProduct(id);

            if (product == null)
                return NotFound();
            var productDto = mapper.Map<Product, ProductDTO>(product);

            return Ok(productDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] SaveProductDTO saveProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await repository.GetProduct(id);

            if (product == null)
                return NotFound();

            mapper.Map<SaveProductDTO, Product>(saveProductDTO, product);


            await unitOfWork.CompleteAsync();

            product = await repository.GetProduct(product.Id);
            var result = mapper.Map<Product, SaveProductDTO>(product);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await repository.GetProduct(id, includeRelated: false);

            if (product == null)
                return NotFound();

            repository.Remove(product);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }


    }
}
