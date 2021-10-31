using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using BlazorWithIdentity.Shared.DTO.ProductSku;
using Blazor.Domain.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Controllers
{
    [Route("/api/productattributes")]
    public class ProductSkuController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductSkuRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;

        public ProductSkuController(ApplicationDbContext context, IMapper mapper, IProductSkuRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ProductSkuDTO>> GetProductSku()
        {
            var productSkus = await context.ProductSkus.
                 Include(p => p.SkuValue)
                 .ThenInclude(a => a.OptionValue)
                 .ThenInclude(b => b.Option)
                 .ThenInclude(c => c.Product).ThenInclude(d => d.ProductCategory)
                   .ToListAsync();

            return mapper.Map<List<ProductSku>, List<ProductSkuDTO>>(productSkus);

            



        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProductSku([FromBody] SaveProductSkuDTO productSkuResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productSku = mapper.Map<SaveProductSkuDTO, ProductSku>(productSkuResource);

            //   skuValue.LastUpdate = DateTime.Now;

            repository.Add(productSku);
            await unitOfWork.CompleteAsync();



            productSku = await repository.GetProductSku(productSku.Id);

            var result = mapper.Map<ProductSku, ProductSkuDTO>(productSku);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductSku(int id)
        {
            var productSku = await repository.GetProductSku(id);

            if (productSku == null)
                return NotFound();
            var productSkuDto = mapper.Map<ProductSku, ProductSkuDTO>(productSku);

            return Ok(productSkuDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateProductSku(int id, [FromBody] SaveProductSkuDTO saveProductSkuDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productSku = await repository.GetProductSku(id);

            if (productSku == null)
                return NotFound();

            mapper.Map<SaveProductSkuDTO, ProductSku>(saveProductSkuDTO, productSku);


            await unitOfWork.CompleteAsync();

            productSku = await repository.GetProductSku(productSku.Id);
            var result = mapper.Map<ProductSku, SaveProductSkuDTO>(productSku);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProductSku(int id)
        {
            var productSku = await repository.GetProductSku(id, includeRelated: false);

            if (productSku == null)
                return NotFound();

            repository.Remove(productSku);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}
