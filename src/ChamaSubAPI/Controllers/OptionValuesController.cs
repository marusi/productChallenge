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
    [Route("/api/optionvalues")]
    public class OptionValueController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOptionValueRepository repository;
        private readonly IUnitOfWork unitOfWork;

        private readonly ChamaSubscriptionDbContext context;



        public OptionValueController(ChamaSubscriptionDbContext context, IMapper mapper, IOptionValueRepository repository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;

        }

        [HttpGet]
        public async Task<IEnumerable<OptionValueDTO>> GetOptionValues()
        {
            var optionValues = await context.OptionValues.ToListAsync();

            return mapper.Map<List<OptionValue>, List<OptionValueDTO>>(optionValues);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOptionValue([FromBody] SaveOptionValueDTO optionValueResource)
        {
            // error handling
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var optionValue = mapper.Map<SaveOptionValueDTO, OptionValue>(optionValueResource);

            optionValue.LastUpdate = DateTime.Now;

            repository.Add(optionValue);
            await unitOfWork.CompleteAsync();



            optionValue = await repository.GetOptionValue(optionValue.Id);

            var result = mapper.Map<OptionValue, OptionValueDTO>(optionValue);
            return Ok(result);
        }

    }
}