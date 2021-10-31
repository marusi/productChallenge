using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Blazor.Domain.Services;
using Blazor.Infrastructure;

using Blazor.Domain.Models;
using BlazorWithIdentity.Shared.DTO.SkuValue;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Server.Controllers
{
    [Route("/api/skuvalues")]
    public class SkuValueController : Controller
    {
        private readonly IMapper mapper;
        private readonly ISkuValueRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public SkuValueController(ApplicationDbContext context, IMapper mapper, ISkuValueRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<SkuValueDTO>> GetSkuValues()
        {
            var skuValues = await context.SkuValues.
                Include(p => p.OptionValue)
                 
                 .ThenInclude(b => b.Option)
                 .ThenInclude(c => c.Product).ThenInclude(d => d.ProductCategory).
                ToListAsync();

            return mapper.Map<List<SkuValue>, List<SkuValueDTO>>(skuValues);
        }

        [HttpPost]
        [Authorize]
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


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSkuValue(int id)
        {
            var skuValue = await repository.GetSkuValue(id);

            if (skuValue == null)
                return NotFound();
            var skuValueDto = mapper.Map<SkuValue, SkuValueDTO>(skuValue);

            return Ok(skuValueDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSkuValue(int id, [FromBody] SaveSkuValueDTO saveSkuValueDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skuValue = await repository.GetSkuValue(id);

            if (skuValue == null)
                return NotFound();

            mapper.Map<SaveSkuValueDTO, SkuValue>(saveSkuValueDTO, skuValue);


            await unitOfWork.CompleteAsync();

            skuValue = await repository.GetSkuValue(skuValue.Id);
            var result = mapper.Map<SkuValue, SaveSkuValueDTO>(skuValue);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSkuValue(int id)
        {
            var skuValue = await repository.GetSkuValue(id, includeRelated: false);

            if (skuValue == null)
                return NotFound();

            repository.Remove(skuValue);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}