namespace PizzaManagementSystem.Web.Mappings.DTOs
{
    public class OrderDetailDTO
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int FK_Order { get; set; }
        public int FK_MenuItem { get; set; }
        public MenuItemDTO MenuItem { get; set; }
    }
}
