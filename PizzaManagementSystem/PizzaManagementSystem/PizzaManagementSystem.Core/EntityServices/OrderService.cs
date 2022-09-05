using PizzaManagementSystem.Core.HelperClasses;
using PizzaManagementSystem.DAL.Context;
using PizzaManagementSystem.DAL.Models;
using PizzaManagementSystem.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaManagementSystem.Core.EntityServices
{
    public class OrderService : GenericEntityService<OrderRepository, Order>
    {
        private OrderDetailService _orderDetailService;
        private MenuItemService _menuItemService;
        public OrderService(DatabaseContext context, OrderDetailService orderDetailService, MenuItemService menuItemService) : base(context)
        {
            _orderDetailService = orderDetailService;
            _menuItemService = menuItemService;
        }

        /// <summary>
        /// Imposta la data di ricezione dell'ordine e lo stato prima di salvare il record sul DB.
        /// </summary>
        /// <param name="order">Oggetto da salvare.</param>
        /// <returns>Restituisce un oggetto contenente l'ID dell'ordine appena salvato, la sua posizione 
        /// tra gli ordini creati lo stesso giorno ordinati per data crescente</returns>
        public OrderResponse SaveNewOrder(Order order)
        {
            // Imposto data e ora di ricezione e lo stato  e lo salvo.
            order.OrderDateTime = DateTime.Now;
            order.State = OrderStateEnum.PENDING;
            order.ID = Save(order);

            //Recupero tutti gli ordini creati nello stesso giorno di quello appena salvato e li ordino per OrderDateTime
            List<Order> todaysOrder = GetBy(e => e.OrderDateTime.Date == DateTime.Today.Date && e.State == OrderStateEnum.PENDING).OrderBy(e => e.OrderDateTime).ToList();
            //Trovo l'indice del nuovo ordine all'interno dell'array per sapere quanti ordini devono essere evasi prima di quello appena salvato
            int lastOrderPosition = todaysOrder.FindIndex(e => e.ID == order.ID);

            // Calcolo il prezzo totale dell'ordine;
            double totalPrice = CalculateOrderTotalPrice(order);

            OrderResponse result = new OrderResponse();
            result.OrderID = order.ID;
            result.OrderTotalPrice = totalPrice;
            result.PendingOrders = lastOrderPosition;

            return result;
        }

        /// <summary>
        /// Metodo per recuperare gli ordini non ancora completati del giorno corrente.
        /// </summary>
        /// <returns>Restituisce una lista di oggetti di tipo Order.</returns>
        public List<Order> GetTodaysPendingOrders()
        {
            return _repository.GetTodaysPendingOrders();
        }


        /// <summary>
        /// Marca l'ordine corrispondente all'ID passato in input come completato.
        /// </summary>
        /// <param name="id">ID dell'ordine da segnare come completato</param>
        public void MarkOrderAsDone(int id)
        {
            Order order = Get(id);
            order.State = OrderStateEnum.DONE;
            base.Save(order);
        }

        /// <summary>
        /// Prima di salvare controlla se nell'ordine è stato selezionato almeno un piatto del menu e nel caso manca genera un eccezione.
        /// Se durante il salvataggio dell'Order e le relative OrderDetails viene generato un errore allora elimino l'ordine appena salvato.
        /// </summary>
        public override int Save(Order entity, bool saveChanges = true)
        {
            if (entity.Details == null || !entity.Details.Any())
                throw new Exception("Please include at least one menu item in the order.");

            try
            {
                entity.ID = base.Save(entity, saveChanges);

                foreach (OrderDetail orderDetail in entity.Details)
                    orderDetail.FK_Order = entity.ID;

                _orderDetailService.Save(entity.Details);

                return entity.ID;
            }
            catch (Exception ex)
            {
                if (entity.ID != 0)
                    Delete(entity);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo per calcolare il prezzo totale dell'ordine. Recupera tutti i MenuItem e cicla le OrderDetail aggiunte
        /// per aggiungere al totale il prodotto tra la quantità selezionata e il prezzo del MenuItem scelto.
        /// </summary>
        /// <param name="order">Oggetto di tipo Order di cui si vuole calcolare il totale</param>
        /// <returns>Restituisce un double che rappresenta il prezzo totale da pagare per l'ordine</returns>
        private double CalculateOrderTotalPrice(Order order)
        {
            double totalPrice = 0;

            List<MenuItem> menuItems = _menuItemService.GetAll();

            if (order.Details != null && order.Details.Any())
                foreach (OrderDetail orderDetail in order.Details)
                    totalPrice = totalPrice + (orderDetail.Quantity * menuItems.First(e => e.ID == orderDetail.FK_MenuItem).Price);

            return totalPrice;
        }
    }
}
