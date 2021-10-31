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

using BlazorWithIdentity.Shared.DTO.OptionValue;
using Microsoft.AspNetCore.Authorization;

namespace ChamaSubAPI.Controllers
{
    [Route("/api/optionvalues")]
    public class OptionValueController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOptionValueRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public OptionValueController(ApplicationDbContext context, IMapper mapper, IOptionValueRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<OptionValueDTO>> GetOptionValues()
        {
            var optionValues = await context.OptionValues.Include(p => p.Option).ThenInclude(o => o.Product).ThenInclude(o => o.ProductCategory).ToListAsync();

          

            return mapper.Map<List<OptionValue>, List<OptionValueDTO>>(optionValues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOptionValue(int id)
        {
            var optionValue = await repository.GetOptionValue(id);

            if (optionValue == null)
                return NotFound();
            var optionValueDto = mapper.Map<OptionValue, OptionValueDTO>(optionValue);

            return Ok(optionValueDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOptionValue([FromBody] SaveOptionValueDTO optionValueResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var optionValue = mapper.Map<SaveOptionValueDTO, OptionValue>(optionValueResource);

         //   optionValue.LastUpdate = DateTime.Now;

            repository.Add(optionValue);
            await unitOfWork.CompleteAsync();



            optionValue = await repository.GetOptionValue(optionValue.Id);

            var result = mapper.Map<OptionValue, OptionValueDTO>(optionValue);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOptionValue(int id, [FromBody] SaveOptionValueDTO saveOptionValueDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var optionValue = await repository.GetOptionValue(id);

            if (optionValue == null)
                return NotFound();

            mapper.Map<SaveOptionValueDTO, OptionValue>(saveOptionValueDTO, optionValue);


            await unitOfWork.CompleteAsync();

            optionValue = await repository.GetOptionValue(optionValue.Id);
            var result = mapper.Map<OptionValue, SaveOptionValueDTO>(optionValue);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOptionValue(int id)
        {
            var optionValue = await repository.GetOptionValue(id, includeRelated: false);

            if (optionValue == null)
                return NotFound();

            repository.Remove(optionValue);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

    }
}