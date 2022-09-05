using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaManagementSystem.DAL.Models;

namespace PizzaManagementSystem.DAL.Context.EntityConfigurations
{
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(e => e.CustomerName).IsRequired().HasMaxLength(100);
            builder.HasMany(e => e.Details).WithOne(e => e.Order).HasForeignKey(e => e.FK_Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
