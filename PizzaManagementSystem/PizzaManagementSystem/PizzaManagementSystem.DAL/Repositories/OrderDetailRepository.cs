using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;

namespace PizzaManagementSystem.DAL.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
