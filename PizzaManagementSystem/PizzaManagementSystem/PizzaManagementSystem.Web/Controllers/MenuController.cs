using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaManagementSystem.Core.EntityServices;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.Web.Mappings.DTOs;
using System.Collections.Generic;

namespace PizzaManagementSystem.Web.Controllers
{
    [Route("api/[controller]/{action}")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IMapper _mapper;
        private MenuItemService _menuItemService;
        public MenuController(IMapper mapper, MenuItemService menuItemService)
        {
            _mapper = mapper;
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<MenuItem> menuItems = _menuItemService.GetAll();
            List<MenuItemDTO> menuItemDTOs = _mapper.Map<List<MenuItemDTO>>(menuItems);
            return Ok(menuItemDTOs);
        }
    }
}
