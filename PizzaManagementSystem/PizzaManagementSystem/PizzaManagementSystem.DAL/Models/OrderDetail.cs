using PizzaManagementSystem.DAL.Interfaces;

namespace PizzaManagementSystem.DAL.Models
{
    public class OrderDetail : IBaseEntity
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int FK_Order { get; set; }
        public Order Order { get; set; }
        public int FK_MenuItem { get; set; }
        public MenuItem MenuItem { get; set; }
    }
}
