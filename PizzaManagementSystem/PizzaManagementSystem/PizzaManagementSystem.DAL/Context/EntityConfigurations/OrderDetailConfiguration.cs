using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaManagementSystem.DAL.Models;

namespace PizzaManagementSystem.DAL.Context.EntityConfigurations
{
    class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(e => e.MenuItem).WithMany().HasForeignKey(e => e.FK_MenuItem).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
