using AutoMapper;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.Web.Mappings.DTOs;

namespace PizzaManagementSystem.Web.Mappings.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }
}
