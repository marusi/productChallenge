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
using ChamaSubscription.Infrastructure.DTO.CompositeProduct;

namespace ChamaSubAPI.Controllers
{
    [Route("/api/compositeproducts")]
    public class CompositeProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICompositeProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public CompositeProductController(ChamaSubscriptionDbContext context, IMapper mapper, ICompositeProductRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<CompositeProductDTO>> GetCompositeProducts()
        {
            var compositeProducts = await context.CompositeProducts
                .Include(p => p.CombinedProducts)
                .ThenInclude(ps => ps.ProductSku)
                .ToListAsync();
          

            return mapper.Map<List<CompositeProduct>, List<CompositeProductDTO>>(compositeProducts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompositeProduct([FromBody] SaveCompositeProductDTO productResource)
        {
           
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<SaveCompositeProductDTO, CompositeProduct>(productResource);

          //  product.TotalPrice += productSku.Price;

            repository.Add(product);
            await unitOfWork.CompleteAsync();



            product = await repository.GetCompositeProduct(product.Id);

            var result = mapper.Map<CompositeProduct, CompositeProductDTO>(product);
            return Ok(result);
        }
    }
}
