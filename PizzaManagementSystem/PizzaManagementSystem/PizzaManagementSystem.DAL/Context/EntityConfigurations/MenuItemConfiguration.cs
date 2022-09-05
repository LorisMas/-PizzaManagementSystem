using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaManagementSystem.DAL.Models;

namespace PizzaManagementSystem.DAL.Context.EntityConfigurations
{
    class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
        }
    }
}
