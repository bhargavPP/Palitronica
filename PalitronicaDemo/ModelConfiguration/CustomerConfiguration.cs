using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PalitronicaDemo.Model;

namespace PalitronicaDemo.ModelConfiguration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.CustomerId);

            builder.HasMany(x => x.Items)
                .WithOne(a => a.Customer)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
