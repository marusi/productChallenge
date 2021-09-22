using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using ChamaSubscription.Domain.Services;
using ChamaSubscription.Infrastructure;
using ChamaSubscription.Infrastructure.DTO;
using ChamaSubscription.Domain.Models;
using ChamaSubscription.Infrastructure.DTO.Product;
using ChamaSubscription.Infrastructure.DTO.Option;
using ChamaSubscription.Infrastructure.DTO.SkuValue;
using ChamaSubscription.Infrastructure.DTO.ProductSku;

namespace ChamaSubAPI.Controllers
{
    [Route("/api/productAttributes")]
    public class ProductSkuController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductSkuRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;

        public ProductSkuController(ChamaSubscriptionDbContext context, IMapper mapper, IProductSkuRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
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

    }
}
