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

namespace ChamaSubAPI.Controllers
{
    [Route("/api/skuvalues")]
    public class SkuValueController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISkuValueRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public SkuValueController(ChamaSubscriptionDbContext context, IMapper mapper, ISkuValueRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<SkuValueDTO>> GetSkuValues()
        {
            var skuValues = await context.SkuValues.ToListAsync();

            return mapper.Map<List<SkuValue>, List<SkuValueDTO>>(skuValues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSkuValue([FromBody] SaveSkuValueDTO skuValueResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skuValue = mapper.Map<SaveSkuValueDTO, SkuValue>(skuValueResource);

         //   skuValue.LastUpdate = DateTime.Now;

            repository.Add(skuValue);
            await unitOfWork.CompleteAsync();



            skuValue = await repository.GetSkuValue(skuValue.Id);

            var result = mapper.Map<SkuValue, SkuValueDTO>(skuValue);
            return Ok(result);
        }

    }
}