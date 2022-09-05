using Microsoft.EntityFrameworkCore;
using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaManagementSystem.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Metodo per recuperare gli ordini non ancora completati del giorno corrente.
        /// </summary>
        /// <returns>Restituisce una lista di oggetti di tipo Order.</returns>
        public List<Order> GetTodaysPendingOrders()
        {
            return ((DatabaseContext)_context).Orders.Where(e => e.OrderDateTime.Date == DateTime.Today.Date && e.State == OrderStateEnum.PENDING)
                                              .Include(e => e.Details).ThenInclude(c => c.MenuItem)
                                              .OrderBy(e => e.OrderDateTime)
                                              .ToList();
        }
    }
}
