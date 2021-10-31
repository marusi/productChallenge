
using Blazor.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        // converting classes to tables code first migration
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products {  get; set; }

        public DbSet<CompositeProduct> CompositeProducts { get; set; }

        public DbSet<Option> Options { get; set; }
        public DbSet<OptionValue> OptionValues { get; set; }
        public DbSet<SkuValue> SkuValues { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //many to many relationship
            builder.Entity<CombinedProduct>().HasKey(ps =>
               new { ps.CompositeProductId, ps.ProductSkuId });

        }

    }
}
