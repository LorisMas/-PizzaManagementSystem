using System;
using System.Collections.Generic;

namespace PizzaManagementSystem.Web.Mappings.DTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDateTime { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
    }
}
