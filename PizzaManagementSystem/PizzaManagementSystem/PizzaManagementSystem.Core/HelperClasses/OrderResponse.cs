namespace PizzaManagementSystem.Core.HelperClasses
{
    public class OrderResponse
    {
        public int OrderID { get; set; }
        public double OrderTotalPrice { get; set; }
        public int PendingOrders { get; set; }
    }
}
