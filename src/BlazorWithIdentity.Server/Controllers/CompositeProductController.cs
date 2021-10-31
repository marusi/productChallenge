using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using BlazorWithIdentity.Shared.DTO.CompositeProduct;
using Blazor.Domain.Models;
using BlazorWithIdentity.Shared.DTO.ProductSku;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Server.Controllers
{
    [Route("/api/compositeproducts")]
    public class CompositeProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICompositeProductRepository repository;
        private readonly IProductSkuRepository skuRepository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public CompositeProductController(ApplicationDbContext context, IMapper mapper, ICompositeProductRepository repository, IUnitOfWork unitOfWork, IProductSkuRepository skuRepository)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.skuRepository = skuRepository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<CompositeProductDTO>> GetCompositeProducts()
        {
            var compositeProducts = await context.CompositeProducts
                 .Include(p => p.CombinedProducts)
                 .ThenInclude(ps => ps.CompositeProduct)
                 .Include(ps => ps.CombinedProducts)
                 .ThenInclude(ps => ps.ProductSku)
                 .ThenInclude(ps => ps.SkuValue)
                 .ThenInclude(ps => ps.OptionValue)
                 .ThenInclude(ps => ps.Option)
                 .ThenInclude(ps => ps.Product)
                 .ThenInclude(ps => ps.ProductCategory)
                .ToListAsync();


            return mapper.Map<List<CompositeProduct>, List<CompositeProductDTO>>(compositeProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompositeProduct(int id)
        {
            var product = await repository.GetCompositeProduct(id);

            if (product == null)
                return NotFound();
            var productDto = mapper.Map<CompositeProduct, CompositeProductDTO>(product);

            return Ok(productDto);
        }




        [HttpPost]
        public async Task<IActionResult> CreateCompositeProduct([FromBody] SaveCompositeProductDTO productResource)
        {
           
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<SaveCompositeProductDTO, CompositeProduct>(productResource);

            // logic for getting the totalPrice
            var productSku = await context.ProductSkus.ToListAsync();
            product.TotalPrice += productSku.Sum(p => p.Price);

            repository.Add(product);
            await unitOfWork.CompleteAsync();



            product = await repository.GetCompositeProduct(product.Id);

            var result = mapper.Map<CompositeProduct, CompositeProductDTO>(product);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCompositeProduct(int id)
        {
            var compositeProduct = await repository.GetCompositeProduct(id, includeRelated: false);

            if (compositeProduct == null)
                return NotFound();

            repository.Remove(compositeProduct);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
    }
}
