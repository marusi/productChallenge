using ChamaSubscription.Domain.Services;
using ChamaSubscription.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace ChamaSubscription.Infrastructure.DI
{
    public class Loader
    {
        public static void Register(IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration cfg)
        {
            var conn = cfg.GetConnectionString("Default");
           
            services.AddDbContext<ChamaSubscriptionDbContext>(options => options.UseSqlServer(conn));

           

            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        }
    }
}
