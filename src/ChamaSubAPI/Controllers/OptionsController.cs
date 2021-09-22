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

namespace ChamaSubAPI.Controllers
{
    [Route("/api/options")]
    public class OptionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOptionRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public OptionController(ChamaSubscriptionDbContext context, IMapper mapper, IOptionRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<OptionDTO>> GetOptions()
        {
            var options = await context.Options.ToListAsync();

            return mapper.Map<List<Option>, List<OptionDTO>>(options);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOption([FromBody] SaveOptionDTO optionResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var option = mapper.Map<SaveOptionDTO, Option>(optionResource);

            option.LastUpdate = DateTime.Now;

            repository.Add(option);
            await unitOfWork.CompleteAsync();



            option = await repository.GetOption(option.Id);

            var result = mapper.Map<Option, OptionDTO>(option);
            return Ok(result);
        }

    }
}