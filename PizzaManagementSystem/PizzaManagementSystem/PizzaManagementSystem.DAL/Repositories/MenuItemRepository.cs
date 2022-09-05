using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;

namespace PizzaManagementSystem.DAL.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>
    {
        public MenuItemRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
