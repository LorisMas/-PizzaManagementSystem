using PizzaManagementSystem.DAL.Interfaces;

namespace PizzaManagementSystem.DAL.Models
{
    public class MenuItem : IBaseEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
