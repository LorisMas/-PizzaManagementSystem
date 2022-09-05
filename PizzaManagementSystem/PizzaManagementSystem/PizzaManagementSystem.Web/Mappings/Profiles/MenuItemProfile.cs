using AutoMapper;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.Web.Mappings.DTOs;

namespace PizzaManagementSystem.Web.Mappings.Profiles
{
    public class MenuItemProfile : Profile
    {
        public MenuItemProfile()
        {
            CreateMap<MenuItem, MenuItemDTO>().ReverseMap();
        }
    }
}
