using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaManagementSystem.Core.EntityServices;
using PizzaManagementSystem.Core.HelperClasses;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.Web.Mappings.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaManagementSystem.Web.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IMapper _mapper;
        private OrderService _orderService;
        public OrderController(IMapper mapper, OrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult SaveNewOrder([FromBody] OrderDTO orderDTO)
        {
            Order order = _mapper.Map<Order>(orderDTO);
            OrderResponse result = _orderService.SaveNewOrder(order);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetTodaysPendingOrders()
        {
            List<Order> pendingOrders = _orderService.GetTodaysPendingOrders();
            List<OrderDTO> pendingOrdersDTOs = _mapper.Map<List<OrderDTO>>(pendingOrders);
            return Ok(pendingOrdersDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult MarkOrderAsDone([FromRoute] int id)
        {
            _orderService.MarkOrderAsDone(id);
            return Ok();
        }
    }
}
