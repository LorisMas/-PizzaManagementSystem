using Autofac;
using PizzaManagementSystem.Core.EntityServices;
using PizzaManagementSystem.Core.HelperServices;
using PizzaManagementSystem.DAL.Context;
using System;

namespace PizzaManagementSystem.Core.IoC
{
    public class ApplicationContainerModule : Module
    {
        private readonly string _connectionString;
        public ApplicationContainerModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            //Se non è stata passata la stringa di connessione genero un'eccezione, necessaria per registrare
            //il DatabaseContext usato da tutte le entità.
            if (string.IsNullOrEmpty(_connectionString))
                throw new ApplicationException("Connection string not defined.");

            //Configuro il DatabaseContext in modo da restituire la stessa istanza durante tutto lo scope della chiamata.
            builder.Register<DatabaseContext>((c) =>
            {
                return new DatabaseContext(_connectionString);
            }).InstancePerLifetimeScope();

            //Configuro il servizio in cui vengono memorizzate le impostazioni di configurazione dell'ambiente.
            //Questo servizio deve essere uguale per tutte le chiamate eseguite al server.
            builder.RegisterType<ConfigurationService>().SingleInstance();

            //Configuro i servizi delle entità.
            builder.RegisterType<MenuItemService>().AsSelf();
            builder.RegisterType<OrderService>().AsSelf();
            builder.RegisterType<OrderDetailService>().AsSelf();
        }
    }
}
