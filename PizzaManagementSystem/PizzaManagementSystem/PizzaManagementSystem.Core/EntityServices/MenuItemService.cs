using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaManagementSystem.Core.EntityServices
{
    public class MenuItemService : GenericEntityService<MenuItemRepository, MenuItem>
    {
        private DatabaseContext _context;
        public MenuItemService(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo per creare dei dati di default se la tabella dei menu items è vuota.
        /// </summary>
        public void CreateDefaultMenu()
        {
            MenuItem menuItem = _context.MenuItems.FirstOrDefault();
            if (menuItem == null)
            {
                List<MenuItem> menuItems = new List<MenuItem>()
                {
                    new MenuItem() { Name ="Margherita", Price = 5 },
                    new MenuItem() { Name ="Ortolana", Price = 6 },
                    new MenuItem() { Name ="Diavola", Price = 6.5 },
                    new MenuItem() { Name ="Bufalina", Price = 7 }
                };

                Save(menuItems);
            }
        }
    }
}
