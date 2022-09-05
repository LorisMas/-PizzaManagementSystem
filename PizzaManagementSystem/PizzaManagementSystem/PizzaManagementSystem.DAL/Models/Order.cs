using PizzaManagementSystem.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace PizzaManagementSystem.DAL.Models
{
    public class Order : IBaseEntity
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDateTime { get; set; }
        public OrderStateEnum State { get; set; }
        public List<OrderDetail> Details { get; set; }
    }

    public enum OrderStateEnum
    {
        PENDING = 1,
        DONE = 2
    }
}
