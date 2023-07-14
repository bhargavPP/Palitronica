using Microsoft.EntityFrameworkCore;

namespace PalitronicaDemo.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        public MyDbContext()
        {

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = "123",
                    CustomerName = "Dunder Mifflin Paper Company",
                    country = "US",
                    state = "PA",
                    zip = "18504",
                    city = "Scranton",
                    street = "1725 Slough Avenue"
                    
                },
                new Customer
                {
                    CustomerId = "456",
                    CustomerName = "Test2",
                    country = "US",
                    state = "FL",
                    zip = "234565",
                    city = "Scranton",
                    street = "1725 Slough Avenue"                 
                   
                }

                );

            modelBuilder.Entity<Item>().HasData(
                new Item { ItemId = "1", Name = "Item 1", Quantity = 5,CustomerId="123" },
                new Item { ItemId = "2", Name = "Item 2", Quantity = 2, CustomerId = "456" },
                new Item { ItemId = "3", Name = "Item 3", Quantity = 3, CustomerId = "123" },
                new Item { ItemId = "4", Name = "Item 4", Quantity = 6, CustomerId = "456" }
                );
        }

        public void Configure(IApplicationBuilder app, MyDbContext dbContext)
        {
            // Other middleware configurations

            // Apply database migrations and seed data
            dbContext.Database.Migrate();

            // Other middleware configurations

        }


    }
}
