using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.DAL.Repositories;

namespace PizzaManagementSystem.Core.EntityServices
{
    public class OrderDetailService : GenericEntityService<OrderDetailRepository, OrderDetail>
    {
        public OrderDetailService(DatabaseContext context) : base(context)
        {
        }
    }
}
