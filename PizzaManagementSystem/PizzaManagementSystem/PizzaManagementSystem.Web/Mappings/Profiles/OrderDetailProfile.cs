using AutoMapper;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.Web.Mappings.DTOs;

namespace PizzaManagementSystem.Web.Mappings.Profiles
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDTO>()
                .ReverseMap()
                .ForMember(model => model.FK_MenuItem, config => config.MapFrom((dto, model) => { return dto.MenuItem != null ? dto.MenuItem.ID : dto.FK_MenuItem; }));
        }
    }
}
