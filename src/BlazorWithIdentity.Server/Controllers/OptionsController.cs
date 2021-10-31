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

using BlazorWithIdentity.Shared.DTO.Option;
using Microsoft.AspNetCore.Authorization;

namespace BlazorWithIdentity.Controllers
{
    [Route("/api/options")]
    public class OptionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOptionRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ApplicationDbContext context;



        public OptionController(ApplicationDbContext context, IMapper mapper, IOptionRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<OptionDTO>> GetOptions()
        {
            var options = await context.Options.Include(o => o.Product).ThenInclude(p => p.ProductCategory).ToListAsync();

            return mapper.Map<List<Option>, List<OptionDTO>>(options);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOption(int id)
        {
            var option = await repository.GetOption(id);

            if (option == null)
                return NotFound();
            var optionDto = mapper.Map<Option, OptionDTO>(option);

            return Ok(optionDto);
        }

       

        [HttpPost]
        public async Task<IActionResult> CreateOption([FromBody] SaveOptionDTO optionResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var option = mapper.Map<SaveOptionDTO, Option>(optionResource);

           // option.LastUpdate = DateTime.Now;

            repository.Add(option);
            await unitOfWork.CompleteAsync();



            option = await repository.GetOption(option.Id);

            var result = mapper.Map<Option, OptionDTO>(option);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateOption(int id, [FromBody] SaveOptionDTO saveOptionDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var option = await repository.GetOption(id);

            if (option == null)
                return NotFound();

            mapper.Map<SaveOptionDTO, Option>(saveOptionDTO, option);


            await unitOfWork.CompleteAsync();

            option = await repository.GetOption(option.Id);
            var result = mapper.Map<Option, SaveOptionDTO>(option);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOption(int id)
        {
            var option = await repository.GetOption(id, includeRelated: false);

            if (option == null)
                return NotFound();

            repository.Remove(option);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }


    }
}