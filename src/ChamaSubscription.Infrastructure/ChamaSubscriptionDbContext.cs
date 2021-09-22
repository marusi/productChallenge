using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using Microsoft.EntityFrameworkCore.Design;
using ChamaSubscription.Domain.Models;

namespace ChamaSubscription.Infrastructure
{
   public class ChamaSubscriptionDbContext : DbContext
    {
        // converting classes to tables code first migration
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products {  get; set; }
        public DbSet<Option> Options {  get; set; }
        public DbSet<OptionValue> OptionValues {  get; set; }
        public DbSet<SkuValue> SkuValues {  get; set; }
        public DbSet<ProductSku> ProductSkus {  get; set; }

        public ChamaSubscriptionDbContext(DbContextOptions<ChamaSubscriptionDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




        }

    }
}
